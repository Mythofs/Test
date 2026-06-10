using Scripts.Managers;
using UnityEngine;

namespace Scripts.Menu.MenuActions
{
    [CreateAssetMenu(fileName = "SaveGameAction", menuName = "MenuActions/Save Game")]
    public class SaveGameAction : MenuAction
    {
        public override void Execute()
        {
            GameManager.Instance.Save();
        }
    }
}