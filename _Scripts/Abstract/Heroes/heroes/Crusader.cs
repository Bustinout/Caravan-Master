using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Crusader : Hero
{

    public Crusader()
    {
        this.Name = "Danath";
        this.Info = "";
        this.className = "Crusader";
        this.spriteLocation = "Sprites/Splash/Crusader";
        this.heroType = 1;
        this.legendaryHero = true;

        this.Stamina = 5;
        this.Power = 25;
        this.Haste = 0;
        this.critChance = 0;
        this.critDamage = 0;
        this.Speed = 0;
        this.attackSpeed = 2;


        this.equipped = new Gear[8];
    }

    public Crusader(string x)
    {
        this.Name = x;
        this.Info = "";
        this.className = "Crusader";
        this.spriteLocation = "Sprites/Splash/Crusader";
        this.heroType = 1;

        this.Stamina = 5;
        this.Power = 20;
        this.Haste = 0;
        this.critChance = 0;
        this.critDamage = 0;
        this.Speed = 0;
        this.attackSpeed = 1.2f;


        this.equipped = new Gear[8];
    }


    public void useAbility(int x, int caravanID) //0 for travel, 1 for combat
    {
        if (x == 0) //travel skill
        {

        }
        else //combat skill
        {

        }
    }

}
