using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Caravan : Event  {

    public bool unlocked;
    public double maxHP;
    public double HP;
    public double luck;

    public string partyName;
    public int caravanID;
    public Event currentEvent;

    public bool isDead;
    public bool inTown;
    public float timeRemaining; //respawn time

    public bool inBattle;
    public int DPS;
    public double damageDone; // for battle progress
    

    public bool clickable; //to show button to claim loot;
    public bool updateNeeded; //to change sprite from battle/event to caravan default

    //inventory
    public int fame; // not implemented yet (shown in vault)
    public List<Gear> items;
    public int gold;
    public int[] researchNotes;

    //artifact;

    //upgrades

    //party
    public List<Hero> heroes; //size cannot exceed 4

    public Caravan()
    {
        unlocked = false;
    }

    public Caravan(int x)
    {
        if (x == 1)
        {
            this.unlocked = true;
            this.isActive = false;
            this.isCaravan = true;
            this.maxHP = 2000;
            this.HP = 2000;
            this.currentLocation = 0;
            this.progress = 0;
            this.speed = 200;
            this.partyName = "The Travel Boys";
            this.caravanID = 0;
            this.heroes = new List<Hero>() { };
            this.spriteLocation = "Sprites/UI/Caravan";
            this.path = new List<int> {  };
            this.items = new List<Gear> { new Gear().generateGear("Common"), new Gear().generateGear("Common"), new Gear().generateGear("Common") };
            this.gold = 6029;
            this.researchNotes = new int[] {0, 0, 0, 0 };
        }
        else if (x == 2)
        {
            this.unlocked = true;
            this.isActive = false;
            this.isCaravan = true;
            this.maxHP = 4200;
            this.HP = 4200;
            this.currentLocation = 1;
            this.progress = 0;
            this.speed = 200;
            this.partyName = "Going Crew";
            this.caravanID = 1;
            this.heroes = new List<Hero>() { };
            this.spriteLocation = "Sprites/UI/Caravan";
            this.path = new List<int> { };
            this.items = new List<Gear> { new Gear().generateGear("Common"), new Gear().generateGear("Common"), new Gear().generateGear("Common") };
            this.gold = 1592;
            this.researchNotes = new int[] { 3, 4, 1, 13 };
        }
        else
        {

        }
    }

    public static void unlockCaravan(int x)
    {
        if (x == 1)
        {
            SaveLoad.current.caravans[0] = new Caravan(1);
            SaveLoad.current.caravans[0].addHero(new Crusader());
            SaveLoad.current.caravans[0].addHero(new Crusader("bababooey"));
            SaveLoad.current.inactiveHeroes.Add(new Crusader("Pooper Scooper"));
        }
        else if (x == 2)
        {
            SaveLoad.current.caravans[1] = new Caravan(2);
            SaveLoad.current.caravans[1].addHero(new Crusader());
            SaveLoad.current.caravans[1].addHero(new Crusader("cornholio"));
            SaveLoad.current.inactiveHeroes.Add(new Crusader("Bungus"));
        }
        else
        {

        }
    }

    public void refreshDPS()
    {
        DPS = 0;
        foreach (Hero x in heroes)
        {
            DPS += x.DPS();
        }
    }

    public void addHero(Hero x)
    {
        if (heroes.Count < 4)
        {
            maxHP += x.Stamina;
            HP += x.Stamina;
            speed += x.Speed;
            luck += x.luck;
            x.currentCaravan = caravanID;
            x.slotInCaravan = heroes.Count;
            heroes.Add(x);
            refreshDPS();
        }
        else
        {
            //error message
        }
    }

    public void removeHero(int x)
    {
        maxHP += heroes[x].Stamina;
        HP += heroes[x].Stamina;
        speed += heroes[x].Speed;
        luck -= heroes[x].luck;
        foreach (Hero h in heroes)
        {
            if (h.slotInCaravan > x){
                h.slotInCaravan -= 1;
            }
        }
        heroes.RemoveAt(x);
        refreshDPS();
    }

    public string getEnergyText()
    {
        return HP + "/" + maxHP;
    }

    public float getEnergyBarValue()
    {
        return ((float)HP) / ((float)maxHP) * 100;
    }

    public string getLocationText()
    {
        if (inBattle)
        {
            return "In combat with " + currentEvent.monster.name + "."; // + (currentEvent.monster.hp - (int)damageDone) + "/"+currentEvent.monster.hp;
        }
        else if (isDead)
        {
            return "Wiped out.";
        }
        else
        {
            if (path.Count == 0)
            {
                return "Resting at " + Library.getLocationName(currentLocation) + ".";
            }
            else
            {
                return "Traveling to " + Library.getLocationName(path[0]) + ".";//display progress?
            }
        }
    }

    public void newEvent(Event x)
    {
        currentEvent = x;
        isActive = false; // Random events halt the caravan until a decision is made

        if (x.isBattle)
        {
            inBattle = true;
            currentEvent.monster.targetCaravanID = caravanID;
        }
        //when clicked, shows event and options
    }

    public void gainLoot()
    {
        //generate loot from monster
        //temporary for debugging
        items.Add(new Gear ().generateGear("Common"));
    }

    public bool battleWon;
    public void endBattle()
    {
        inBattle = false;
        if (path.Count > 0)
        {
            isActive = true;
        }
        damageDone = 0;

        if (battleWon)
        {
            Debug.Log("BATTLE WON");
            gainLoot();
        }
        else
        {
            Debug.Log("BATTLE LOST");
            isDead = true;
            isActive = false;
        }
        //get rewards

        updateNeeded = true; //marked for sprite change
    }

    
}
