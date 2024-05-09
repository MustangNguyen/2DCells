using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Elements
{
    public PrimaryElement primaryElement = PrimaryElement.Physic;

    public SecondaryElement secondaryElement = SecondaryElement.None;
}
public enum PrimaryElement{
    None,
    Physic,
    Fire,
    Water,
    Electric,
    Toxin,
    Earth
}
public enum SecondaryElement{
    None,
    Ice,
}

public enum Status{
    
}