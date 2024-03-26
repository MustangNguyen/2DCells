using System;
using System.Diagnostics.Tracing;
using UnityEngine;

[Serializable]
public class CellProtection
{
    
    public ArmorType armorType = ArmorType.None;
    public int armorPoint = 0;
}
public enum ArmorType{
    None,
    Alloy,
    Bio,
    Shield
}