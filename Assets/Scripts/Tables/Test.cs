using System.Collections.Generic;
using static EnumTypes;

[System.Serializable]
public class Test
{
    public int ID;
    public string Def;
    public float Float;
    public bool Bool;
    public string De2;
    public int name;
    public float fuck;
    public bool bo2;
    public PlayerStateType PlayerStateType;
    public string PlayerSkiils;
    public InGameParamType InGameParamType;
    public static Dictionary<int, Test> table = new Dictionary<int, Test> ();   
}