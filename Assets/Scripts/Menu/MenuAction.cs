using Scripts.Managers;
using UnityEngine;

namespace Scripts.Menu
{
    public abstract class MenuAction : ScriptableObject
	{
        public abstract void Execute();
	}
    [CreateAssetMenu(fileName = "QuitGameAction", menuName = "MenuActions/Quit Game")]
    public class QuitGameAction : MenuAction
    {
        public override void Execute()
        {
            GameManager.Instance.Save();
            Application.Quit();
        }
    }
    [CreateAssetMenu(fileName = "SaveGameAction", menuName = "MenuActions/Save Game")]
    public class SaveGameAction : MenuAction
    {
        public override void Execute()
        {
            GameManager.Instance.Save();
        }
    }
    [CreateAssetMenu(fileName = "OpenInventoryAction", menuName = "MenuActions/Open Inventory")]
    public class OpenInventoryAction : MenuAction
    {
        public override void Execute()
        {
            GameManager.Instance.SetState(GameManager.GameState.Inventory);
        }
    }
}