using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataManagerOOP
{
    public List<MutationOOP> listMutations = new();
    public List<EnemyCellOOP> listEnemies = new();
    public List<AbilityOOP> listAbilities = new();
}
