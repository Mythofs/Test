using UnityEngine.InputSystem;
using UnityEngine;
using Scripts.Managers;
using Scripts.Menu;

namespace Scripts.Player
{
    public class PlayerCancel : MonoBehaviour
    {
        PlayerControl control;
        private void Start()
        {
            control = PlayerInputManager.Instance.Control;
            OnEnable();
        }
        public void OnEnable()
        {
            if(control != null)
                control.Player.Cancel.performed += OnCancel;
        }
        public void OnDisable()
        {
            if (control != null)
                control.Player.Cancel.performed -= OnCancel;
        }
        private void OnCancel(InputAction.CallbackContext context)
        {
            GameManager.GameState state = GameManager.Instance.State;
            if (state == GameManager.GameState.Overworld)
                return;
            if (state == GameManager.GameState.Interact)
                if (PlayerInteract.Instance.InteractScript.CanCancel())
                    PlayerInteract.Instance.InteractScript.Close();
                else
                    return;
            if (state == GameManager.GameState.Menu)
                MenuManager.Instance.Close();
            GameManager.Instance.SetState(GameManager.GameState.Overworld);

        }
    }
}
