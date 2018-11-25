using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{

    public string playerName;
    public int gold;

    //Heroes
    public List<Hero> inactiveHeroes; // heroes that were added before, chilling at barracks
    public List<Hero> unrecruitedHeroes; // untouched
    public List<Gear> items;

    //Caravans (active heroes are added to caravans)
    public Caravan[] caravans;

    //public static List<Quests> activeQuests;
    //public static List<Quests> availableQuests;

    //Events (Patrols, Quest Events)
    public List<Event> events;
    

    //Research
    public int[] tinkerUpgrades; // [ upgrade0-level, upgrade1-level, etc.]
    public int[] generalUpgrades;
    public int[] baronUpgrades;
    public int[] scoutUpgrades;
    public int[] researchNotes; // [ tNotes, gNotes, bNotes, sNotes ]

    //Altar
    public int[] reagents; // [ reagent0, reagent1, reagent2, reagent3, reagent4 ]
    public string currentBuff;

    //Journal stuff

    //Progress Trackers / Milestones


    //Caravans
    //public Caravans[] caravans;

    public Game()
    {

    }

    public Game(string playerName)
    {
        this.playerName = playerName;
        this.gold = 0;

        this.tinkerUpgrades = new int[20];
        this.generalUpgrades = new int[20];
        this.baronUpgrades = new int[20];
        this.scoutUpgrades = new int[20];
        this.researchNotes = new int[4];

        this.items = new List<Gear>();

        this.inactiveHeroes = new List<Hero>();
        //add list of unrecruited heroes

        this.reagents = new int[5];
        this.currentBuff = "";

        this.caravans = new Caravan[] { new Caravan(), new Caravan(), new Caravan() };
        this.events = new List<Event>() { new Event(), new Event() };
    }

    public void addEvent(Event x)
    {
        events.Add(x);
    }

    public void addGear(Gear x)
    {
        items.Add(x);
    }

    public void sortInactiveHeroes()
    {
        List<Hero> newList = new List<Hero>();

        if (inactiveHeroes.Count > 0)
        {
            newList.Add(inactiveHeroes[0]);
            for (int x = 1; x < inactiveHeroes.Count; x++)
            {
                bool added = false;
                for (int y = 0; y < newList.Count; y++)
                {
                    if (inactiveHeroes[x].rank > newList[y].rank)
                    {
                        newList.Insert(y, inactiveHeroes[x]);
                        added = true;
                        break;
                    }
                    else if (inactiveHeroes[x].rank == newList[y].rank)
                    {
                        if (inactiveHeroes[x].heroType < newList[y].heroType)
                        {
                            newList.Insert(y, inactiveHeroes[x]);
                            added = true;
                            break;
                        }
                        else if (inactiveHeroes[x].heroType == newList[y].heroType)
                        {
                            if (inactiveHeroes[x].className.CompareTo(newList[y].className) < 0)
                            {
                                newList.Insert(y, inactiveHeroes[x]);
                                added = true;
                                break;
                            }
                            else if (inactiveHeroes[x].className.CompareTo(newList[y].className) == 0)
                            {
                                if (inactiveHeroes[x].Name.CompareTo(newList[y].Name) < 0)
                                {
                                    newList.Insert(y, inactiveHeroes[x]);
                                    added = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!added)
                {
                    newList.Add(inactiveHeroes[x]);
                }
            }

            inactiveHeroes = newList;
        }
        
    }


    public void sortItems()
    {
        List<Gear> newList = new List<Gear>();

        if (items.Count > 0)
        {
            newList.Add(items[0]);
            for (int x = 1; x < items.Count; x++)
            {
                bool added = false;
                for (int y = 0; y < newList.Count; y++)
                {
                    if (Library.convertRarity(items[x]) > Library.convertRarity(newList[y]))
                    {
                        newList.Insert(y, items[x]);
                        added = true;
                        break;
                    }
                    else if (Library.convertRarity(items[x]) == Library.convertRarity(newList[y]))
                    {
                        if (items[x].equipSlot < newList[y].equipSlot)
                        {
                            newList.Insert(y, items[x]);
                            added = true;
                            break;
                        }
                        else if (items[x].equipSlot == newList[y].equipSlot)
                        {
                            if (items[x].itemName.CompareTo(newList[y].itemName) < 0)
                            {
                                newList.Insert(y, items[x]);
                                added = true;
                                break;
                            }
                        }
                    }
                }
                if (!added)
                {
                    newList.Add(items[x]);
                }
            }
            items = newList;
        }
    }

}
