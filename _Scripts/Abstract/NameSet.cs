using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameSet {

    public int equipSlot;
    public string name;
    public string spriteLocation;

    public NameSet()
    {

    }

    public NameSet(int equipSlot, string name, string spriteLocation)
    {
        this.equipSlot = equipSlot;
        this.name = name;
        this.spriteLocation = spriteLocation;
    }

}
