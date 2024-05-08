using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Elements
{
    public PrimaryElement primaryElement = PrimaryElement.physic;

    public SecondaryElement secondaryElement = SecondaryElement.none;
}
public enum PrimaryElement{
    none,
    physic,
    fire,
    wet,
    electric,
    toxin,
    earth
}
public enum SecondaryElement{
    none,
    ice,
}

public enum Status{
    
}