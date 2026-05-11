using Scripts.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private PlayerMovement overworldMovement;
        private PlayerControl control;
        private LayerMask interactableObjectsLayer;
        private bool InInteract;
        private SpriteRenderer sr;
        private Interactable script;
        private void Awake()
        {
            control = new PlayerControl();
            interactableObjectsLayer = LayerMask.GetMask("InteractableObjects");
            InInteract = false;
        }
        private void OnEnable()
        {
            control.Enable();
            control.Player.Interact.performed += OnInteract;
        }
        private void OnDisable()
        {
            control.Player.Interact.performed -= OnInteract;
            control.Disable();
        }
        private void OnInteract(InputAction.CallbackContext context)
        {
            if (InInteract)
            {
                script.CloseDialog();
                InInteract = false;
                overworldMovement.enabled = true;
            }
            Collider2D col = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + PlayerMovement.Facing, 0.2f, interactableObjectsLayer);
            if (col != null)
            {
                overworldMovement.enabled = false;
                InInteract = true;
                sr = col.GetComponent<SpriteRenderer>();
                script = sr.GetComponent<Interactable>();
                script.Interact();
            }
        }
    }
}