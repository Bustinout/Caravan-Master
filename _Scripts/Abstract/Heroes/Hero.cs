using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero
{
    public string Name;
    public string Info;
    public string className;
    public string spriteLocation;

    //public int exp;
    public int rank;

    public int heroType; //1 for bodyguard, 2 for merchant, 3 for traveler
    public bool legendaryHero;
    public int currentCaravan;
    public int slotInCaravan;

    public int Stamina; //grants HP
    public int Power;
    public int Haste; // % value
    public int critChance; // % value
    public int critDamage; // % value
    public int Speed;
    public int luck;

    public float attackSpeed;


    public bool coroutineActive;

    public Gear[] equipped;
    public bool gearLocked; //prevents gear from being unequipped when unequipping all

    public Hero()
    {

    }

    public Hero(string x = "Slayer")
    {
        this.Name = "Kul";
        this.Info = "";
        this.className = "Slayer";
        this.spriteLocation = "Sprites/Splash/Crusader";
        this.heroType = 1;
        this.legendaryHero = true;

        this.Stamina = 5;
        this.Power = 14;
        this.Haste = 0;
        this.critChance = 0;
        this.critDamage = 0;
        this.Speed = 0;
        this.attackSpeed = 1;


        this.equipped = new Gear[8];
    }

    public void equipGear(Gear x)
    {
        int tempSlot = x.equipSlot;
        if (equipped[tempSlot] == null)
        {
            equipped[tempSlot] = x;
            addStats(x);
        }
        else
        {
            removeStats(equipped[tempSlot]);
            SaveLoad.current.addGear(equipped[tempSlot]);
            equipped[tempSlot] = x;
            addStats(x);
        }
    }

    public void unequipGear(int x)
    {
        if (equipped[x] != null)
        {
            SaveLoad.current.addGear(equipped[x]);
            removeStats(equipped[x]);
            equipped[x] = null;
        }
    }


    public void addStats(Gear x)
    {
        Stamina += x.stamina;
        Power += x.power;
        Haste += x.haste;
        critChance += x.critChance;
        critDamage += x.critDamage;
        Speed += x.speed;
        luck += x.luck;
    }

    public void removeStats(Gear x)
    {
        Stamina -= x.stamina;
        Power -= x.power;
        Haste -= x.haste;
        critChance -= x.critChance;
        critDamage -= x.critDamage;
        Speed -= x.speed;
        luck -= x.luck;
    }

    public int DPS()
    {
        double baseDPS = ((double)Power) * (1 + (((double)Haste) / 100));
        double critMultiplier = 1.5 + (((double)critDamage) / 100);

        int DPS = (int)((baseDPS * critMultiplier * (((double)critChance) / 100)) + (baseDPS * (1 - ((double)critChance / 100))));

        if (heroType == 1)
        {
            return (2*DPS);
        }
        else
        {
            return DPS;
        }
    }

    public double calculateDamage()
    {
        float rand = Random.Range(0, 100);
        if (rand <= critChance)
        {
            return ((double)Power) * (1.5 + (((double)critDamage) / 100));
        }
        else
        {
            return (double) Power;
        }
    }

    public string mouseOverInfo()
    {
        string returnString = "";

        returnString += Name;
        returnString += "\n";
        if (legendaryHero)
        {
            returnString += "Legendary ";
        }
        returnString += className;

        returnString += "\n\n" + Stamina + " Stamina";
        returnString += "\n" + DPS() + " DPS";
        returnString += "\n" + Speed + " Speed";
        returnString += "\n" + luck + " Luck";


        return returnString;
    }

    public string compareHeroes(Hero x) //change font color somehow
    {
        string returnString = "";

        returnString += "Comparing " + Name + " (" + className + ") with " + x.Name + " (" + x.className + ")\n";

        if (x.Stamina > Stamina)
        {
            returnString += "\n+ " + (x.Stamina - Stamina) + " Stamina";
        }
        else if (x.Stamina < Stamina)
        {
            returnString += "\n- " + (Stamina - x.Stamina) + " Stamina";
        }
        else
        {
            returnString += "\n+ 0 Stamina";
        }

        if (x.DPS() > DPS())
        {
            returnString += "\n+ " + (x.DPS() - DPS()) + " DPS";
        }
        else if (x.DPS() < DPS())
        {
            returnString += "\n- " + (DPS() - x.DPS()) + " DPS";
        }
        else
        {
            returnString += "\n+ 0 DPS";
        }

        if (x.Speed > Speed)
        {
            returnString += "\n+ " + (x.Speed - Speed) + " Speed";
        }
        else if (x.Stamina < Stamina)
        {
            returnString += "\n- " + (Speed - x.Speed) + " Speed";
        }
        else
        {
            returnString += "\n+ 0 Speed";
        }

        if (x.luck > luck)
        {
            returnString += "\n+ " + (x.luck - luck) + " Luck";
        }
        else if (x.Stamina < Stamina)
        {
            returnString += "\n- " + (luck - x.luck) + " Luck";
        }
        else
        {
            returnString += "\n+ 0 Luck";
        }

        return returnString;
    }

    public string classString()
    {
        return Library.heroRanks[rank] + " " + className;
    }

    public string statString()
    {
        return Stamina + "\n" + Power + "\n" + Haste + "%\n" + critChance + "%\n" + (critDamage+150) + "%\n" + attackSpeed + "\n" + DPS() + "\n" + Speed + "\n" + luck;
    }


    //collect stats?





}
