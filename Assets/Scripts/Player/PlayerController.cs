using UnityEngine;

namespace Scripts.Player
{
	public class PlayerController: MonoBehaviour
	{
        PlayerMovement movement;
        public static PlayerController Instance;
        private void Awake()
        {
            if (Instance = null)
                Instance = this;
            else
                Destroy(this);
            movement = GetComponent<PlayerMovement>();
        }
        public void Save()
        {
            GameManager.Instance.saveData.playerData.facing = movement.Facing;
            GameManager.Instance.saveData.playerData.position = transform.position;
        }
        public void Load(PlayerData playerData)
        {
            transform.position = playerData.position;
            movement.SetFacing(playerData.facing);
        }
    }
}