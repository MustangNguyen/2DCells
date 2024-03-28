using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Elements
{
    public PrimaryElement element = PrimaryElement.physic;
}
public enum PrimaryElement{
    physic,
    fire,
    ice,
    electric,
    toxin
}
public enum SecondaryElement{
    
}