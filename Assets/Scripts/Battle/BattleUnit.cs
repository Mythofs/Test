using UnityEngine.UI;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PokemonBase pBase;
    [SerializeField] int level;
    [SerializeField] bool isPlayer;
    public Pokemon Pokemon { get; set; }
    public void setup()
    {
        Pokemon = new Pokemon(pBase, level);
        if(isPlayer)
            GetComponent<Image>().sprite = Pokemon.Base.BackSprite;
        else
            GetComponent<Image>().sprite = Pokemon.Base.FrontSprite;
    }
}
