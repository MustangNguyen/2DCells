using System;
using System.Diagnostics.Tracing;
using UnityEngine;

[Serializable]
public class CellProtection
{
    
    public ArmorType armorType = ArmorType.None;
    public ShieldType shieldType = ShieldType.None;
    public int armorPoint = 0;
    public int shieldPoint = 0;
}
public enum ArmorType{
    None,
    Alloy,
    Bio,
}
public enum ShieldType{
    None,
    Proto,
    Pulse

}