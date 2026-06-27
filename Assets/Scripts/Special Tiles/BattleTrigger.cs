using Scripts.Managers;
using System.Collections;
using UnityEngine;

namespace Scripts.SpecialTiles
{
    public class BattleTrigger : MonoBehaviour, ISpecialTile
    {
        public void Interact()
        {
            GameManager.Instance.Level = 0;
            GameManager.Instance.SetState(GameManager.GameState.Battle);
        }
    }
}