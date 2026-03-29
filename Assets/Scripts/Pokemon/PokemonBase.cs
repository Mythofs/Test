using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create a new Pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string pokemonName;
    [TextArea]
    [SerializeField] string desc;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] type type1;
    [SerializeField] type type2;
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;
    [SerializeField] List<LearnableMove> learnableMoves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string Name => pokemonName;
    public string Desc => desc;
    public Sprite FrontSprite => frontSprite;
    public Sprite BackSprite => backSprite;
    public type Type1 => type1;
    public type Type2 => type2;
    public int MaxHP => maxHP;
    public int Attack => attack;
    public int Defense => defense;
    public int SpAttack => spAttack;
    public int SpDefense => spDefense;
    public int Speed => speed;
    public List<LearnableMove> LearnableMoves => learnableMoves;
}
[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase mBase;
    [SerializeField] int level;
    public MoveBase Base => mBase;
    public int Level => level;
}
public enum type
{
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Dark,
    Steel,
    Fairy
}
