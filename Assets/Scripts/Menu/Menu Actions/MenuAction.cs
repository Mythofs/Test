using Scripts.Managers;
using UnityEngine;

namespace Scripts.Menu
{
    public abstract class MenuAction : ScriptableObject
	{
        public abstract void Execute();
	}
}