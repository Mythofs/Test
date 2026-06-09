using Scripts.Managers;
using Scripts.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        private Vector2 offset = new(0f, -0.3f);
        private PlayerControl control;
        private LayerMask interactableObjectsLayer;
        private bool InInteract;
        private SpriteRenderer sr;
        private IInteractable script;
        private void Awake()
        {
            interactableObjectsLayer = LayerMask.GetMask("InteractableObjects");
            InInteract = false;
        }
        private void Start()
        {
            control = PlayerInputManager.Instance.Control;
            OnEnable();
        }
        private void OnEnable()
        {
            if (control == null)
                return;
            control.Player.Interact.performed += OnInteract;
            control.Player.Cancel.performed += OnCancel;
        }
        private void OnDisable()
        {
            if (control == null)
                return;
            control.Player.Interact.performed -= OnInteract;
            control.Player.Cancel.performed -= OnCancel;
        }
        private void OnCancel(InputAction.CallbackContext content)
        {
            if (InInteract && script.CanCancel())
            {
                script.Close();
                InInteract = false;
                GameManager.Instance.SetState(GameManager.GameState.Overworld);
            }
        }
        private void OnInteract(InputAction.CallbackContext context)
        {
            if (InInteract)
            {
                if (script.RepeatedInteract())
                    script.Interact();
                else
                {
                    script.Close();
                    InInteract = false;
                    GameManager.Instance.SetState(GameManager.GameState.Overworld);
                }
                return;
            }
            Collider2D col = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + PlayerMovement.Instance.Facing + offset, 0.2f, interactableObjectsLayer);
            if (col != null)
            {
                sr = col.GetComponent<SpriteRenderer>();
                script = sr.GetComponent<IInteractable>();
                if (script.CanInteract())
                {
                    GameManager.Instance.SetState(GameManager.GameState.Interact);
                    InInteract = true;
                    script.Interact();
                }
            }
            GameManager.Instance.SetState(GameManager.GameState.Overworld);
        }
    }
}