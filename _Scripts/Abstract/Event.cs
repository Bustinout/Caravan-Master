using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public int currentLocation;
    public int speed;
    public int exploreSpeed;
    public double progress;

    public string eventName;
    public string spriteLocation;

    public bool isActive;
    public bool isBattle;
    public bool spriteUpdated; // for optimization

    public Monster monster;
    //int[] AllowedPath //roads quests will travel on

    public List<int> path;


    //Quest
    public bool isQuestTarget;
    public int questID;


    //CaravanOnly
    public bool isCaravan;
    public bool exploring;


    public Event()
    {
        int tempX = Random.Range(1, 6);
        this.currentLocation = tempX;
        this.speed = 80;
        this.progress = 0;
        this.spriteLocation = "Sprites/UI/Patrol";
        this.isActive = true;
        this.isBattle = true;
        this.monster = new Monster();
        this.path = new List<int> { Library.getNextPath(tempX) };
    }

    public void update(float interval)
    {
        if (isActive)
        {
            if (progress < 1)
            {
                progress = progress + ((speed * interval) / Library.distanceBetweenNodes);
            }
            else
            {
                progress = 0;
                currentLocation = path[0];
                path.RemoveAt(0);
                if (path.Count == 0)
                {
                    if (isCaravan)
                    {
                        isActive = false;
                    }
                    else
                    {
                        getNewPath();
                    }
                }
            }
            spriteUpdated = false;
        }
        else if (exploring)
        {
            if (progress < 1)
            {
                progress = progress + ((double)exploreSpeed * interval);
            }
            else
            {
                endExploring();
            }
            spriteUpdated = false;
        }
    }

    public void getNewPath()
    {
        path.Add(Library.getNextPath(currentLocation));
    }

    //Caravan Only
    public void startExploring()
    {
        exploring = true;
        isActive = false;
    }

    public void endExploring()
    {
        exploring = false;
        progress = 0;
        //get rewards
        //trigger quests
    }

}


