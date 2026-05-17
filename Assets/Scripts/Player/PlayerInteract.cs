using Scripts.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerInventory playerInventory;
        private Vector2 offset = new(0f, -0.3f);
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
                playerMovement.enabled = true;
                playerInventory.enabled = true;
            }
            Collider2D col = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + PlayerMovement.Facing + offset, 0.2f, interactableObjectsLayer);
            if (col != null)
            {
                sr = col.GetComponent<SpriteRenderer>();
                script = sr.GetComponent<Interactable>();
                if (script.CanInteract())
                {
                    playerMovement.enabled = false;
                    playerInventory.enabled = false;
                    InInteract = true;
                    script.Interact();
                }
            }
        }
    }
}