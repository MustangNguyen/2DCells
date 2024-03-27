using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements : MonoBehaviour
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