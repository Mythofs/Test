using UnityEngine;

namespace Scripts.Player
{
    class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance { get; private set; }
        public PlayerControl Control { get; private set; }
        private void Awake()
        {
            Control = new PlayerControl();
            Instance = this;
        }
        private void OnEnable()
        {
            if (Control == null)
                return;
            Control.Enable();
        }
        private void OnDisable()
        {
            if (Control == null)
                return;
            Control.Disable();
        }
        private void OnDestroy()
        {
            if (Control == null)
                return;
            Control.Dispose();
        }
    }
}
