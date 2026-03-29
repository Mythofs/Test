using TMPro;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] HPBar hpbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void setData(Pokemon pokemon)
    {
        nameText.text = pokemon.Base.Name;
        levelText.text = "Lvl " + pokemon.Level;
        hpbar.SetHP((float) pokemon.HP / pokemon.MaxHP);
    }
}
