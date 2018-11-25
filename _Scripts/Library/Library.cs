using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{

    public static int distanceBetweenNodes = 500; //make into array later for different paths

    public static string[] locationNames = { "Town", "Glacial Peak", "Desert Wastes", "Abyss Harbor", "Forlorn Woods", "Whispering Forest" };
    public static string getLocationName(int x)
    {
        return locationNames[x];
    }

    //global canvas coords
    public static coord[] locationCoords = { new coord(0, 0), new coord(0, 0), new coord(0, 0), new coord(0, 0), new coord(0, 0), new coord(0, 0) };
    //use the test in mainmanager to get new coords of moved nodes

    public static coord getCoord(int location, int destination, double progress)
    {
        coord start = locationCoords[location];
        coord end = locationCoords[destination];
        coord vector = coord.subCoords(end, start);

        return coord.addCoords(start , coord.multCoords(vector, progress));
    }

    public static coord getCoord(int location)
    {
        return locationCoords[location];
    }

    public static int nextDestination(int start, int end)
    {
        return 0;
    }

    //when looking for path, because it is ordered by size, if the current index is greater or = -1, return false (path not there)
    public static int[][] availablePaths =
    {
        new int [] { 1, 2, 3, 4, 5}, //Zone 0
        new int [] { 0, 2, 5, -1, -1},
        new int [] { 0, 1, 3, -1, -1},
        new int [] { 0, 2, 4, -1, -1},
        new int [] { 0, 3, 5, -1, -1},
        new int [] { 0, 1, 4, -1, -1} //Zone 5
    };

    public static bool pathAvailable(int location, int destination)
    {
        for (int x = 0; x < availablePaths[location].Length; x++)
        {
            if (availablePaths[location][x] == destination)
            {
                return true;
            }
            if (availablePaths[location][x] > destination || availablePaths[location][x] == -1)
            {
                return false;
            }
        }
        return false;
    }

    public static int getNextPath(int x)
    {
        int temp = -1;
        while (temp == -1) //|| temp == 0)
        {
            temp = availablePaths[x][Random.Range(0, availablePaths[x].Length)];
        }
        return temp;
    }

    public static Vector3 convertMouseToLocal(Vector3 x)
    {
        Vector3 temp = x;
        //temp.x -= 200;
        //temp.y = temp.y / 1.6f;
        //temp = camera.ViewportToWorldPoint(temp);

        return temp;
    }

    public static Event[] Events = {



    };


    //Converter (to String)
    public static string convertValue(float valueToConvert)
    {
        string converted;
        if (valueToConvert >= 1000000)
        {
            converted = (valueToConvert / 1000000f).ToString("f2") + "M";
        }
        if (valueToConvert >= 10000)
        {
            converted = (valueToConvert / 1000f).ToString("f2") + "K";
        }
        else
        {
            converted = (valueToConvert).ToString("f0");
        }
        return converted;
    }

    public static string convertTime(float valueToConvert)
    {
        string converted = "";
        float temp = valueToConvert;
        if (temp >= 3600)
        {
            converted += (valueToConvert / 3600).ToString("f0") + "H ";
            temp = temp % 3600;
        }
        else
        {
            converted += "0H ";
        }
        if (temp >= 60)
        {
            converted += (valueToConvert / 60).ToString("f0") + "M ";
            temp = temp % 60;
        }
        else
        {
            converted += "0M ";
        }
        converted += (temp).ToString("f0") + "S";
        return converted;
    }

    // comes after className
    public static string[] heroRanks =
    {
        "",
        "Adventurer",
        "Hero",
        "Master",
        "Legend"
    };

    //Gear
    public static string[] slotNames =
    {
        "Helm",
        "Chestpiece",
        "Pants",
        "Boots",
        "Shoulders",
        "Gloves",
        "Necklace",
        "Wrists"
    };

    public static string getRarityBackground(string rarity)
    {
        if (rarity.Equals("Common"))
        {
            return "ItemIcons/Rarity/common";
        }
        else if (rarity.Equals("Uncommon"))
        {
            return "ItemIcons/Rarity/uncommon";
        }
        else if (rarity.Equals("Rare"))
        {
            return "ItemIcons/Rarity/rare";
        }
        else if (rarity.Equals("Epic"))
        {
            return "ItemIcons/Rarity/epic";
        }
        else //legendary
        {
            return "ItemIcons/Rarity/legendary";
        }
    }

    public static PrefixSet[] commonPrefixSets = {

        new PrefixSet("Abandoned", new bool[] { true, false, false, false, false, false, false }, "Someone more tasteful must have left this behind."),
        new PrefixSet("Sticky", new bool[] { true, false, false, false, false, false, false }, "Why would you even touch this?"),
        new PrefixSet("Reeking", new bool[] { true, false, false, false, false, false, false }, "Renders all sneaking efforts completely useless."),
        new PrefixSet("Charred", new bool[] { true, false, false, false, false, false, false }, "Whoever wore these last must've tasted delicious."),
        new PrefixSet("Deformed", new bool[] { true, false, false, false, false, false, false }, "You're not sure if this was ever worn by a person."),
        new PrefixSet("Counterfeit", new bool[] { true, false, false, false, false, false, false }, "Identical in every way but form, function, and price!"),
        new PrefixSet("Itchy", new bool[] { true, false, false, false, false, false, false }, "The wearer must defy all intuition when putting these on."),
        new PrefixSet("Battlescarred", new bool[] { true, false, false, false, false, false, false }, "You've seen something like this before in a locked box."),
        new PrefixSet("Abused", new bool[] { true, false, false, false, false, false, false }, "It rocks slowly when left alone."),

    };

    public static PrefixSet[] uncommonPrefixSets = {

        new PrefixSet("Angry", new bool[] { true, true, false, false, false, false, false }, "It occasionally yells at you."),
        new PrefixSet("Magical", new bool[] { true, false, true, false, false, false, false }, "It wears an illusion. What is it hiding..."),
        new PrefixSet("Precise", new bool[] { true, false, false, true, false, false, false }, "Pefectly measured... for someone else."),
        new PrefixSet("Outlaw", new bool[] { true, false, false, false, true, false, false }, "Wanted for crimes against fashion."),
        new PrefixSet("Speedy", new bool[] { true, false, false, false, false, true, false }, "There seems to be some kind of substance lining the surface."),
        new PrefixSet("Lucky", new bool[] { true, false, false, false, false, false, true }, "You find a silver piece in it.")

    };

    public static PrefixSet[] rarePrefixSets = {

        new PrefixSet("Berserking", new bool[] { true, true, true, false, false, false, false }, "Annoying loud drums beat in the head of whoever wears this."),
        new PrefixSet("Remorseless", new bool[] { true, true, false, true, false, false, false }, "You feel less guilty as you remove this from your fallen opponent."),
        new PrefixSet("Mighty", new bool[] { true, true, false, false, true, false, false }, "Still unable to dissuade a sharpened blade."),
        new PrefixSet("Raging", new bool[] { true, true, false, false, false, true, false }, "As you drop it to the ground, it smashes a nearby mouse."),
        new PrefixSet("Reckless", new bool[] { true, true, false, false, false, false, true }, "Was it worth it? Risking your lives for this?"),

        new PrefixSet("Impatient", new bool[] { true, false, true, true, false, false, false }, "You can't wait… to bonk your foes in the noggen."),
        new PrefixSet("Destructive", new bool[] { true, false, true, false, true, false, false }, "Did it just knock over that bottle over there?"),
        new PrefixSet("Swift", new bool[] { true, false, true, false, false, true, false }, "There are some stars painted on this thing."),
        new PrefixSet("Blessed", new bool[] { true, false, true, false, false, false, true }, "You feel very blessed to be in this damned world."),

        new PrefixSet("Seeking", new bool[] { true, false, false, true, true, false, false }, "The item has found you."),
        new PrefixSet("Urgent", new bool[] { true, false, false, true, false, true, false }, "It's got places to be and things to kill."),
        new PrefixSet("Fortunate", new bool[] { true, false, false, true, false, false, true }, "No matter where you strike, you always seem to ook them in the dooker."),

        new PrefixSet("Foolish", new bool[] { true, false, false, false, true, true, false }, "Poor decision-making led this item into your possession."),
        new PrefixSet("Blindman's", new bool[] { true, false, false, false, true, false, true }, "Stolen from a napping blind man."),

        new PrefixSet("Comfortable", new bool[] { true, false, false, false, false, true, true }, "Hey... this feels pretty good."),

    };

    public static PrefixSet[] epicPrefixSets = {

        new PrefixSet("Champion's", new bool[] { true, true, false, true, true, false, false }, "Those who wear this walk slower and produce their own annoying theme song."),
        new PrefixSet("Assassin's", new bool[] { true, true, true, false, true, false, false }, "You'll probably only ever use like four of these pockets."),
        new PrefixSet("Paladin's", new bool[] { true, false, true, false, false, false, true }, "It's got a little compartment to house fireflies."),
        new PrefixSet("Wizard's", new bool[] { true, false, true, true, true, false, false }, "Bits of incantations are scrawled all over."),
        new PrefixSet("Thief's", new bool[] { true, false, true, false, false, true, true }, "Pretty ordinary, in fact you used to own one of these just like it."),
        new PrefixSet("Gambler's", new bool[] { true, false, false, true, true, false, true }, "?."), //WRITE SOMETHING HERE
        new PrefixSet("Nobleman's", new bool[] { true, true, false, false, false, true, true }, "It's got… uh… very deep pockets!"),

    };

    public static PrefixSet[] legendaryPrefixSets = {

        new PrefixSet("Dominating", new bool[] { true, true, false, true, true, false, false }, "It beckons you to try it on."),
        new PrefixSet("Deadly", new bool[] { true, true, true, false, true, false, false }, "Lined with spikes both inside and outside, a danger to both enemy and wearer."),
        new PrefixSet("Divine", new bool[] { true, false, true, false, false, false, true }, "The wearers actions reflect the very will of the heavens."),
        new PrefixSet("Unleashed", new bool[] { true, false, true, true, true, false, false }, "Someone put a leash on it."),
        new PrefixSet("Invisible", new bool[] { true, false, true, false, false, true, true }, "Passerbys avert your gaze… probably because they can't see you in the first place."),
        new PrefixSet("Fortune's", new bool[] { true, false, false, true, true, false, true }, "Where others find pieces of silver, you find solid blocks of gold. Now if only you were strong to carry them."),
        new PrefixSet("Extravagant", new bool[] { true, true, false, false, false, true, true }, "It scoffs at you."),

    };

    public static PrefixSet getPrefixSet(string rarity)
    {
        if (rarity.Equals("Common"))
        {
            return (commonPrefixSets[Random.Range(0, commonPrefixSets.Length)]);
        }
        else if (rarity.Equals("Uncommon"))
        {
            return (uncommonPrefixSets[Random.Range(0, uncommonPrefixSets.Length)]);
        }
        else if (rarity.Equals("Rare"))
        {
            return (rarePrefixSets[Random.Range(0, rarePrefixSets.Length)]);
        }
        else if (rarity.Equals("Epic"))
        {
            return (epicPrefixSets[Random.Range(0, epicPrefixSets.Length)]);
        }
        else //legendary
        {
            return (legendaryPrefixSets[Random.Range(0, legendaryPrefixSets.Length)]);
        }
    }


    public static NameSet[] nameSets =
    {
        new NameSet(0, "Helmet", "ItemIcons/Helmets/helmet2"),
        new NameSet(1, "Chest", "ItemIcons/Chests/chest1"),
        new NameSet(2, "Pants", "ItemIcons/Legs/shittypants"),
        new NameSet(3, "Boots", "ItemIcons/Boots/boots1"),
        new NameSet(4, "Shoulders", "ItemIcons/Shoulders/shoulders2"),
        new NameSet(5, "Gloves", "ItemIcons/Gloves/shittygloves"),
        new NameSet(6, "Necks", "ItemIcons/Necks/neck1"),
        new NameSet(7, "Wrists", "ItemIcons/Wrists/bracer1")
    };

    public static NameSet getNameSet(string rarity) //add others
    {
        //if (rarity.equals("Common")
        return nameSets[Random.Range(0, nameSets.Length)];
    }

    public static int convertRarity(Gear x)
    {
        if (x.rarity.Equals("Common"))
        {
            return 0;
        }
        if (x.rarity.Equals("Uncommon"))
        {
            return 1;
        }
        if (x.rarity.Equals("Rare"))
        {
            return 2;
        }
        if (x.rarity.Equals("Epic"))
        {
            return 3;
        }
        if (x.rarity.Equals("Legendary"))
        {
            return 4;
        }
        return 0;
    }

    public Gear[] sortLoot(Gear[] items)
    {
        if (items.Length > 1)
        {
            Gear[] sorted = items;

            for (int i = 1; i < sorted.Length; i++)
            {
                Gear x = sorted[i];
                int j = i - 1;
                while (j >= 0 && convertRarity(sorted[j]) > convertRarity(x))
                {
                    sorted[j + 1] = sorted[j];
                    j = j - 1;
                }
                sorted[j + 1] = x;
            }
            return sorted;
        }
        else
        {
            return items;
        }
    }




    /*
    public List<Hero> sortHeroes(List<Hero> heroes)
    {
        if (heroes.Count > 1)
        {
            List<Hero> sorted = heroes;

            for (int i = 1; i < sorted.Count; i++)
            {
                Hero x = sorted[i];
                int j = i - 1;
                while (j >= 0 && sorted[j].DPS() > x.DPS()) //figure out what needs to be sorted
                {
                    sorted[j + 1] = sorted[j];
                    j = j - 1;
                }
                sorted[j + 1] = x;
            }
            return sorted;
        }
        else
        {
            return heroes;
        }
    }
    */

}
