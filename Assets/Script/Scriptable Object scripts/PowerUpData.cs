using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Power Up Data", menuName = "Scriptable Objects/Power Up Data")]
public class PowerUpData : ScriptableObject
{
    public PowerUpRarity rarity = PowerUpRarity.Common;
    public string id;
    public string powerUpName;
    public string description;
    public Image glyph;
    public PowerUp powerUp;



}
