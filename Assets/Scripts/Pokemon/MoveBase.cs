using NUnit.Framework.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "PokemonMove", menuName = "PokemonMove/Create a new Pokemon move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string moveName;
    [TextArea]
    [SerializeField] string desc;
    [SerializeField] type type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string Name => moveName;
    public string Desc => desc;
    public type Type => type;
    public int Power => power;
    public int Accuracy => accuracy;
    public int PP => pp;
}
