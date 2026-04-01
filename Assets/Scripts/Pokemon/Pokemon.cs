using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private PokemonBase Base { get; set; }
    private int Level { get; set; }
    private int HP { get; set; }
    private List<Move> Moves { get; set; }
    public Pokemon(PokemonBase pb, int l)
    {
        Base = pb;
        Level = l;
        HP = MaxHP;
        Moves = new List<Move>();
        foreach(var move in Base.LearnableMoves)
        {
            if(move.Level <= Level)
                Moves.Add(new Move(move.Base));
            if(Moves.Count >= 4)
                break;
        }
    }
    public int Attack => Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5;
    public int Defense => Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5;
    public int SpAttack => Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5;
    public int SpDefense => Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5;
    public int Speed => Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5;
    public int MaxHP => Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10;
}
