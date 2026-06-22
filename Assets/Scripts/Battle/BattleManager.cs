using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public BattleManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
