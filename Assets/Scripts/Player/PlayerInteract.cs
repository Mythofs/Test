using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private PlayerControl control;
    private LayerMask interactableObjectsLayer;
    private void Awake()
    {
        control = new PlayerControl();
        interactableObjectsLayer = LayerMask.GetMask("InteractableObjects");
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
        Collider2D col = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + PlayerMovement.facing, 0.2f, interactableObjectsLayer);
        if(col != null)
        {
            SpriteRenderer sr = col.GetComponent<SpriteRenderer>();
            IInteractable script = sr.GetComponent<IInteractable>();
            script.Interact();
        }
    }
}
