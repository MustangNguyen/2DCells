using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Elements
{
    public PrimaryElement primaryElement = PrimaryElement.physic;
    public SecondaryElement secondaryElement;
}
public enum PrimaryElement{
    physic,
    fire,
    wet,
    electric,
    toxin,
    earth
}
public enum SecondaryElement{
    ice,

}

public enum Status{
    
}