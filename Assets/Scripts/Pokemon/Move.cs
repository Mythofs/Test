using UnityEngine;

public class Move
{
    public MoveBase mBase { get; set; }
    public int PP  { get; set; }
    public Move(MoveBase mb)
    {
        mBase = mb;
        PP = mb.PP;
    }
}
