using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gear {

    public string itemName;
    public string spriteLocation;
    public string flavorText;

    public bool identified;
    public int equipSlot;
    /*
     *  0 - helmet
     *  1 - chest
     *  2 - pants
     *  3 - boots
     *  4 - shoulders
     *  5 - wrists
     *  6 - neck
     *  7 - gloves
     */
    public string rarity;

    public int stamina;
    public int power;
    public int haste;
    public int critChance;
    public int critDamage;
    public int speed;
    public int luck;

    public Gear()
    {

    }

    public Gear(NameSet nameSet, PrefixSet prefixSet, int [] stats, string rarity)
    {
        this.itemName = prefixSet.prefix + " " + nameSet.name;
        this.spriteLocation = nameSet.spriteLocation;
        this.flavorText = prefixSet.flavortext;
        this.equipSlot = nameSet.equipSlot;
        this.rarity = rarity;
        this.stamina = stats[0];
        this.power = stats[1];
        this.haste = stats[2];
        this.critChance = stats[3];
        this.critDamage = stats[4];
        this.speed = stats[5];
        this.luck = stats[6];

        this.identified = false;
    }

    public int[] commonLowRange = { 1 };
    public int[] uncommonLowRange = { 3, 1, 1, 1, 5, 1, 1 };
    public int[] rareLowRange = { 6, 3, 3, 3, 11, 3, 3 };
    public int[] epicLowRange = { 11, 6, 6, 6, 16, 6, 6 };
    public int[] legendaryLowRange = { 16, 9, 9, 9, 21, 9, 9 };

    public int[] commonHighRange = { 2 };
    public int[] uncommonHighRange = { 5, 2, 2, 2, 10, 2, 2 };
    public int[] rareHighRange = { 10, 5, 5, 5, 15, 5, 5 };
    public int[] epicHighRange = { 15, 8, 8, 8, 20, 8, 8 };
    public int[] legendaryHighRange = { 18, 10, 10, 10, 24, 10, 10 };

    public int[] getLowRange(string rarity)
    {
        if (rarity.Equals("Common"))
        {
            return commonLowRange;
        }
        else if (rarity.Equals("Uncommon"))
        {
            return uncommonLowRange;
        }
        else if (rarity.Equals("Rare"))
        {
            return rareLowRange;
        }
        else if (rarity.Equals("Epic"))
        {
            return epicLowRange;
        }
        else //legendary
        {
            return legendaryLowRange;
        }
    }

    public int[] getHighRange(string rarity)
    {
        if (rarity.Equals("Common"))
        {
            return commonHighRange;
        }
        else if (rarity.Equals("Uncommon"))
        {
            return uncommonHighRange;
        }
        else if (rarity.Equals("Rare"))
        {
            return rareHighRange;
        }
        else if (rarity.Equals("Epic"))
        {
            return epicHighRange;
        }
        else //legendary
        {
            return legendaryHighRange;
        }
    }

    public int [] generateStats(bool [] statsToGenerate, string rarity)
    {
        int[] stats = new int[] { 0, 0, 0, 0, 0, 0, 0};
        int[] lowRange = getLowRange(rarity);
        int[] highRange = getHighRange(rarity);

        for (int x = 0; x < 7; x++)
        {
            if (statsToGenerate[x])
            {
                stats[x] = Random.Range(lowRange[x], highRange[x] + 1);
            }
        }

        return stats;
    }


    public Gear generateGear(string rarity)
    {
        PrefixSet PS = Library.getPrefixSet(rarity);
        int[] stats = generateStats(PS.statTemplate, rarity);
        NameSet NS = Library.getNameSet(rarity);
        
        return new Gear(NS, PS, stats, rarity);
    }

    public string infoString()
    {
        string returnString = "";

        if (identified)
        {
            returnString += itemName;
            returnString += "\n" + rarity + " " + Library.slotNames[equipSlot] + "\n";

            if (stamina > 0)
            {
                returnString += "\n" + stamina + " Stamina";
            }
            if (power > 0)
            {
                returnString += "\n" + power + " Power";
            }
            if (haste > 0)
            {
                returnString += "\n" + haste + " Haste";
            }
            if (critChance > 0)
            {
                returnString += "\n" + critChance + "% Critical Chance";
            }
            if (critDamage > 0)
            {
                returnString += "\n" + critDamage + "% Critical Damage";
            }
            if (speed > 0)
            {
                returnString += "\n" + speed + " Speed";
            }
            if (luck > 0)
            {
                returnString += "\n" + luck + " Luck";
            }
            //returnString += "\n\n\"" + flavorText + "\"";
            //sell price
        }
        else
        {
            returnString += "Unidentified " + Library.slotNames[equipSlot];
            returnString += "\n\n???";
        }


        return returnString;
    }

    public string compareItems(Gear x)
    {
        bool changes = false;
        string returnString = "";
        returnString += "Comparing " + itemName + " with " + x.itemName + "\n";

        if (x.stamina > stamina)
        {
            changes = true;
            returnString += "\n+ " + (x.stamina - stamina) + " Stamina";
        }
        else if (x.stamina < stamina)
        {
            changes = true;
            returnString += "\n- " + (stamina - x.stamina) + " Stamina";
        }

        if (x.power > power)
        {
            changes = true;
            returnString += "\n+ " + (x.power - power) + " Power";
        }
        else if (x.power < power)
        {
            changes = true;
            returnString += "\n- " + (power - x.power) + " Power";
        }

        if (x.haste > haste)
        {
            changes = true;
            returnString += "\n+ " + (x.haste - haste) + "% Haste";
        }
        else if (x.haste < haste)
        {
            changes = true;
            returnString += "\n- " + (haste - x.haste) + "% Haste";
        }

        if (x.critChance > critChance)
        {
            changes = true;
            returnString += "\n+ " + (x.critChance - critChance) + "% Critical Chance";
        }
        else if (x.critChance < critChance)
        {
            changes = true;
            returnString += "\n- " + (critChance - x.critChance) + "% Critical Chance";
        }

        if (x.critDamage > critDamage)
        {
            changes = true;
            returnString += "\n+ " + (x.critDamage - critDamage) + "% Critical Damage";
        }
        else if (x.critDamage < critDamage)
        {
            changes = true;
            returnString += "\n- " + (critDamage - x.critDamage) + "% Critical Damage";
        }

        if (x.speed > speed)
        {
            changes = true;
            returnString += "\n+ " + (x.speed - speed) + " Speed";
        }
        else if (x.speed < speed)
        {
            changes = true;
            returnString += "\n- " + (speed - x.speed) + " Speed";
        }

        if (x.luck > luck)
        {
            changes = true;
            returnString += "\n+ " + (x.luck - luck) + " Luck";
        }
        else if (x.luck < luck)
        {
            changes = true;
            returnString += "\n- " + (luck - x.luck) + " Luck";
        }

        if (!changes)
        {
            returnString += "\n No changes in stats.";
        }

        return returnString;
    }






}
