using Scripts.Managers;
using UnityEngine;

namespace Scripts.Menu.MenuActions
{
    [CreateAssetMenu(fileName = "OpenInventoryAction", menuName = "MenuActions/Open Inventory")]
    public class OpenInventoryAction : MenuAction
    {
        public override void Execute()
        {
            MenuManager.Instance.Close();
            GameManager.Instance.SetState(GameManager.GameState.Inventory);
        }
    }
}