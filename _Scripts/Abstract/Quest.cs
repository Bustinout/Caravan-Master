using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {

    public int currentStage = 0;
    public int totalStages;
    public bool stageCompleted;

    public string questTitle;
    public string [] questName;
    public string [] questDescription;

    public int[] questType;
    /*
        1 - Reach location / different locations
        2 - Explore location / different locations
        3 - kill spawned monster / kill monsters
        4 - collect notes
    */

    public int currentObjectiveCount;
    public int [] requiredObjectiveCount;

    // type 1 and 2
    public int [][] destinations; // -1 for any new location
    public List<int> [] visitedLocations; // add visited locations here to encourage going to new areas
    //type 3
    public Monster [] targetMonsters;
    public int [] spawnLocations;
    //type 4
    public int [][] notesType;

    

    //rewards
    public int [][] researchNotes;
    public int [] gold;
    public Gear[] gear;


    public Quest(string x = "Crusader")
    {
        this.questTitle = "Crusader Main Quest"; //change to something edgy
        this.currentStage = 0;
        //this.totalStages 
        
    }

    public string getObjectiveString()
    {
        string returnString = "";

        if (questType[currentStage] == 1)
        {
            if (destinations[currentStage].Length == 1)
            {
                returnString += "- Visit " + Library.locationNames[destinations[currentStage][0]] + ". (" + currentObjectiveCount + "/" + requiredObjectiveCount + ")";
            }
            else
            {
                for (int x = 0; x < destinations[currentStage].Length; x++)
                {
                    returnString += "- Visit different locations. (" + currentObjectiveCount + "/" + requiredObjectiveCount + ")";
                }
            }
        }
        else if (questType[currentStage] == 2)
        {
            if (destinations[currentStage].Length == 1)
            {
                returnString += "- Explore " + Library.locationNames[destinations[currentStage][0]] + ". (" + currentObjectiveCount + "/" + requiredObjectiveCount + ")";
            }
            else
            {
                for (int x = 0; x < destinations[currentStage].Length; x++)
                {
                    returnString += "- Explore different locations. (" + currentObjectiveCount + "/" + requiredObjectiveCount + ")";
                }
            }
        }
        else if (questType[currentStage] == 3)
        {
            returnString += "- Slay " + targetMonsters[currentStage].name + ". (" + currentObjectiveCount + "/" + requiredObjectiveCount + ")";
        }
        else if (questType[currentStage] == 4)
        {
            // do something with research notes
        }

        return returnString;
    }

    public void checkMonsterProgress(Monster x)
    {
        if (targetMonsters[currentStage] == x)
        {

        }
    }

    public void checkForCompletion()
    {
        if (currentObjectiveCount >= requiredObjectiveCount[currentStage])
        {
            stageCompleted = true;
        }
    }

    public void progressToNextStage()
    {
        //claim rewards
        SaveLoad.current.gold += gold[currentStage];
        SaveLoad.current.items.Add(gear[currentStage]);
        for (int x = 0; x < 4; x++)
        {
            SaveLoad.current.researchNotes[x] += researchNotes[currentStage][x];
        }
        //upgrade hero level

        //next stage if any 
        if (currentStage < totalStages)
        {
            currentStage++;
            stageCompleted = false;
        }
    }

}
