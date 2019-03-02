using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Affection
{
    //affection values for all partners, access by calling Affection.Name and changing the int value
    //this is simply a struct declaration, the values must be stored in GameControl

    public int zander;
    public int nathaniel;
    public int thistle;
    public int embrey;
    public int luna;
    public int lily;

    public Affection(int Zander, int Nathaniel, int Thistle, int Embrey, int Luna, int Lily)
    {
        zander = Zander;
        nathaniel = Nathaniel;
        thistle = Thistle;
        embrey = Embrey;
        luna = Luna;
        lily = Lily;
    }
}
