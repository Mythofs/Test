using Scripts.Managers;
using UnityEngine;

namespace Scripts.Menu.MenuActions
{
    [CreateAssetMenu(fileName = "QuitGameAction", menuName = "MenuActions/Quit Game")]
    public class QuitGameAction : MenuAction
    {
        public override void Execute()
        {
            GameManager.Instance.Save();
            Application.Quit();
        }
    }
}