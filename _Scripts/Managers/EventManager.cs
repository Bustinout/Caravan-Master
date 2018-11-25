using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    public MainManager MM;
    public GameObject sprite;
    public Transform parent;
    public Transform lineParent;
    private float interval = 0.05f;

    public List<GameObject> eventSprites;

    public GameObject caravan1Controller;
    public GameObject caravan2Controller;
    public GameObject caravan3Controller;
    public GameObject dragCancel;

    //public int selectedCaravan;
    public List<int> selectedNodes;

    public bool mouseOnCaravan;
    public bool dragged;
    public bool paused; //for anything using realtime
    public Image cancelButton;
    public bool currentlyDrawing;
    public GameObject currentlDrawLineParent;
    public LineRenderer currentlDrawLine;
    public GameObject localLineCopy;
    public List<GameObject> pathLines;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseOnCaravan)
            {
                startCaravanDrag();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (dragged)
            {
                endCaravanDrag();
                dragged = false;
            }
        }

        if (currentlyDrawing)
        {
            currentlDrawLine.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

    }

    public void manualStart ()
    {
        initializeSprites();
        StartCoroutine("EventTracker");
    }


    public void pause(bool x)
    {
        if (x)
        {
            paused = true;
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
        }
    }

    public GameObject caravanFetcher(int x)
    {
        if (x == 0)
        {
            return caravan1Controller;
        }
        else if (x == 1)
        {
            return caravan2Controller;
        }
        else
        {
            return caravan3Controller;
        }
    }

    public void setCaravanControllerRaycasts(bool x)
    {
        if (x)
        {
            caravan1Controller.GetComponent<Image>().raycastTarget = true;
            caravan2Controller.GetComponent<Image>().raycastTarget = true;
            caravan3Controller.GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            caravan1Controller.GetComponent<Image>().raycastTarget = false;
            caravan2Controller.GetComponent<Image>().raycastTarget = false;
            caravan3Controller.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void eventRaycast(bool input)
    {
        foreach (GameObject x in eventSprites)
        {
            x.GetComponent<Image>().raycastTarget = input;
        }
    }

    public void clearAllSprites()
    {
        foreach (GameObject x in eventSprites)
        {
            Destroy(x);
        }
    }

    public void initializeSprites() // do everytime event is added or removed
    {
        clearAllSprites();
        eventSprites = new List<GameObject>();

        for (int x = 0; x < SaveLoad.current.events.Count; x++)
        {
            eventSprites.Add(Instantiate(sprite, parent, true));
            eventSprites[x].SetActive(true);
            coord temp = Library.getCoord(SaveLoad.current.events[x].currentLocation, SaveLoad.current.events[x].path[0], SaveLoad.current.events[x].progress);
            eventSprites[x].transform.localPosition = new Vector3((float)temp.x, (float)temp.y, 0);
            eventSprites[x].GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.events[x].spriteLocation);
        }

        for (int x = 0; x < SaveLoad.current.caravans.Length; x++)
        {
            if (SaveLoad.current.caravans[x].unlocked)
            {
                if (SaveLoad.current.caravans[x].progress > 0)
                {
                    coord temp = Library.getCoord(SaveLoad.current.caravans[x].currentLocation, SaveLoad.current.caravans[x].path[0], SaveLoad.current.caravans[x].progress);
                    caravanFetcher(x).transform.localPosition = new Vector3((float)temp.x, (float)temp.y, 0);
                }
                else
                {
                    coord temp = Library.getCoord(SaveLoad.current.caravans[x].currentLocation);
                    caravanFetcher(x).transform.localPosition = new Vector3((float)temp.x, (float)temp.y, 0);
                }


                setCaravanSprite(x);

                caravanFetcher(x).SetActive(true);
            }
        }
    }

    public void updateSprites()
    {
        for (int x = 0; x < SaveLoad.current.events.Count; x++)
        {
            if (!SaveLoad.current.events[x].spriteUpdated)
            {
                if (SaveLoad.current.events[x].path.Count == 0)
                {
                    coord temp = Library.locationCoords[SaveLoad.current.events[x].currentLocation];
                    eventSprites[x].transform.localPosition = new Vector3((float)temp.x, (float)temp.y, 0);
                }
                else
                {
                    coord temp = Library.getCoord(SaveLoad.current.events[x].currentLocation, SaveLoad.current.events[x].path[0], SaveLoad.current.events[x].progress);
                    eventSprites[x].transform.localPosition = new Vector3((float)temp.x, (float)temp.y, 0);
                }
                SaveLoad.current.events[x].spriteUpdated = true;
            }
        }

        for (int x = 0; x < SaveLoad.current.caravans.Length; x++)
        {
            if (SaveLoad.current.caravans[x].unlocked && !SaveLoad.current.caravans[x].spriteUpdated)
            {
                if (SaveLoad.current.caravans[x].path.Count == 0)
                {
                    coord temp = Library.locationCoords[SaveLoad.current.caravans[x].currentLocation];
                    caravanFetcher(x).transform.localPosition = new Vector3((float)temp.x, (float)temp.y, 0);
                }
                else
                {
                    coord temp = Library.getCoord(SaveLoad.current.caravans[x].currentLocation, SaveLoad.current.caravans[x].path[0], SaveLoad.current.caravans[x].progress);
                    caravanFetcher(x).transform.localPosition = new Vector3((float)temp.x, (float)temp.y, 0);
                }

                SaveLoad.current.caravans[x].spriteUpdated = true;
            }
            if (SaveLoad.current.caravans[x].updateNeeded)
            {
                setCaravanSprite(x);
            }
        }
    }

    public void setCaravanSprite(int x)
    {
        MM.FrameFetcher(x).setMode();
        if (SaveLoad.current.caravans[x].inBattle)
        {
            caravanFetcher(x).GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/UI/Battle");
        }
        else if (SaveLoad.current.caravans[x].isDead)
        {
            caravanFetcher(x).GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/UI/RIP");
        }
        else //its fine
        {
            caravanFetcher(x).GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[x].spriteLocation);
        }
        SaveLoad.current.caravans[x].updateNeeded = false;
    }

    public void caravanCollisionCheck()
    {
        for (int c = 0; c < SaveLoad.current.caravans.Length; c++)
        {
            if (SaveLoad.current.caravans[c].unlocked && (!SaveLoad.current.caravans[c].inBattle) && (!SaveLoad.current.caravans[c].isDead))
            {
                for (int x = 0; x < SaveLoad.current.events.Count; x++)
                {
                    if (SaveLoad.current.caravans[c].progress == 0 || SaveLoad.current.events[x].progress == 0)
                    {
                        if (SaveLoad.current.caravans[c].currentLocation == SaveLoad.current.events[x].currentLocation &&
                            SaveLoad.current.events[x].progress == 0 && SaveLoad.current.caravans[c].progress == 0)
                        {
                            triggerEvent(c, x);
                        }
                    }
                    else if (SaveLoad.current.caravans[c].currentLocation == SaveLoad.current.events[x].currentLocation &&
                        SaveLoad.current.caravans[c].path[0] == SaveLoad.current.events[x].path[0])
                    {
                        if (SaveLoad.current.caravans[c].progress > Mathf.Max((float)SaveLoad.current.events[x].progress - 0.02f, 0) && SaveLoad.current.caravans[c].progress < Mathf.Min((float)SaveLoad.current.events[x].progress + 0.02f, 1))
                        {
                            triggerEvent(c, x);
                        }
                    }
                    else if (SaveLoad.current.caravans[c].currentLocation == SaveLoad.current.events[x].path[0] &&
                             SaveLoad.current.caravans[c].path[0] == SaveLoad.current.events[x].currentLocation)
                    {
                        if (SaveLoad.current.caravans[c].progress > Mathf.Max((float)(1 - SaveLoad.current.events[x].progress - 0.02f), 0) && SaveLoad.current.caravans[c].progress < Mathf.Min((float)(1 - SaveLoad.current.events[x].progress + 0.02f), 1))
                        {
                            triggerEvent(c, x);
                        }
                    }
                }
            }
        }
    }

    public void triggerEvent(int caravanIndex, int eventIndex)
    {
        SaveLoad.current.caravans[caravanIndex].newEvent(SaveLoad.current.events[eventIndex]);

        if (SaveLoad.current.events[eventIndex].isBattle)
        {
            foreach (Hero x in SaveLoad.current.caravans[caravanIndex].heroes){
                if (!x.coroutineActive)
                {
                    StartCoroutine(battleTracker(x));
                    x.coroutineActive = true;
                }
            }
            StartCoroutine(enemyTracker(SaveLoad.current.caravans[caravanIndex].currentEvent.monster));
        }
        setCaravanSprite(caravanIndex);
        SaveLoad.current.events.RemoveAt(eventIndex);
        Destroy(eventSprites[eventIndex]);
        eventSprites.RemoveAt(eventIndex);
        addEvent();
    }

    public void addEvent()
    {
        SaveLoad.current.addEvent(new Event());
        initializeSprites();
    }

    //BATTLE 
    IEnumerator battleTracker(Hero x) //coroutine does not stop and reactivate if new monster joins before it can yield break
    {
        while (true)
        {
            //Debug.Log(x.Name + " COROUTINE ACTIVE");

            if (SaveLoad.current.caravans[x.currentCaravan].inBattle)
            {
                if (SaveLoad.current.caravans[x.currentCaravan].damageDone < SaveLoad.current.caravans[x.currentCaravan].currentEvent.monster.hp)
                {
                    SaveLoad.current.caravans[x.currentCaravan].damageDone += x.calculateDamage();
                    MM.FrameFetcher(x.currentCaravan).refreshEnemyCombatBar();
                }
                else
                {
                    deactivateCoroutine(x);
                    yield break;
                }

                if (SaveLoad.current.caravans[x.currentCaravan].damageDone < SaveLoad.current.caravans[x.currentCaravan].currentEvent.monster.hp)
                {
                    
                    yield return new WaitForSeconds(x.attackSpeed * (1 + (x.Haste / 100)));
                }
                else
                {
                    SaveLoad.current.caravans[x.currentCaravan].battleWon = true;
                    SaveLoad.current.caravans[x.currentCaravan].endBattle();
                    deactivateCoroutine(x);
                    yield break;
                }

            }
            else
            {
                deactivateCoroutine(x);
                yield break;
            }

        }
    }

    public void deactivateCoroutine(Hero x)
    {
        SaveLoad.current.caravans[x.currentCaravan].heroes[x.slotInCaravan].coroutineActive = false;
        //Debug.Log(x.Name + " COROUTINE DEACTIVATED");
    }



    IEnumerator enemyTracker(Monster x)
    {
        while (true)
        {
            //Debug.Log(x.name + " COROUTINE ACTIVE");
            if (!SaveLoad.current.caravans[x.targetCaravanID].inBattle || SaveLoad.current.caravans[x.targetCaravanID].currentEvent.monster != x)
            {
                //Debug.Log(x.name + " COROUTINE DEACTIVED");
                yield break;
            }
            else
            {
                SaveLoad.current.caravans[x.targetCaravanID].HP = (SaveLoad.current.caravans[x.targetCaravanID].HP) - (x.calculateDamage());
                MM.FrameFetcher(x.targetCaravanID).refreshCombatBar();
                //Debug.Log(SaveLoad.current.caravans[x.targetCaravanID].HP);
                if (SaveLoad.current.caravans[x.targetCaravanID].HP <= 0)
                {
                    SaveLoad.current.caravans[x.targetCaravanID].HP = 0;
                    SaveLoad.current.caravans[x.targetCaravanID].battleWon = false;
                    SaveLoad.current.caravans[x.targetCaravanID].endBattle();
                    //Debug.Log(x.name + " COROUTINE DEACTIVED");
                    yield break;
                }
                else
                {
                    yield return new WaitForSeconds(x.attackSpeed * (1 + (x.Haste / 100)));
                }
            }
        }
    }

    //TRAVEL / SPRITES
    IEnumerator EventTracker()
    {
        while (true)
        {
            foreach (Event x in SaveLoad.current.events)
            {
                x.update(interval);
            }
            caravanCollisionCheck();
            foreach (Caravan x in SaveLoad.current.caravans)
            {
                x.update(interval);
            }
            updateSprites();
            MM.refreshAllFrames();
            yield return new WaitForSeconds(interval);
        }
    }

    public int caravanBeingControlled;
    public List<int> nodePath;
    public bool selectingNodes;
    public int tempCurrentLocation;
    public bool pathCanceled;
    public void setNewCaravanPath()
    {
        if (!pathCanceled)
        {
            if (nodePath.Count > 0)
            {
                SaveLoad.current.caravans[caravanBeingControlled].path = nodePath;
                if (SaveLoad.current.caravans[caravanBeingControlled].currentLocation == nodePath[0])
                {
                    SaveLoad.current.caravans[caravanBeingControlled].progress = 1 - SaveLoad.current.caravans[caravanBeingControlled].progress;
                    SaveLoad.current.caravans[caravanBeingControlled].currentLocation = tempCurrentLocation;
                }
                if (!SaveLoad.current.caravans[caravanBeingControlled].inBattle)
                {
                    SaveLoad.current.caravans[caravanBeingControlled].isActive = true;
                }
            }
        }
        else
        {
            pathCanceled = false;
        }
        clearLines();

    }

    public void changeCancelOpacity(bool x)
    {
        if (x)
        {
            Color c = cancelButton.color;
            c.a = 1f;
            cancelButton.color = c;
        }
        else
        {
            Color c = cancelButton.color;
            c.a = 0.5f;
            cancelButton.color = c;
        }
    }

    public void startCaravanDrag()
    {
        if (SaveLoad.current.caravans[caravanBeingControlled].inBattle || SaveLoad.current.caravans[caravanBeingControlled].isDead)
        {
            //cant drag when in battle or dead
        }
        else
        {
            dragged = true;
            pause(true);
            setCaravanControllerRaycasts(false);
            selectingNodes = true;
            nodePath = new List<int>();
            changeCancelOpacity(false);
            dragCancel.SetActive(true);
            //draw lines
            currentlyDrawing = true;
            currentlDrawLine.SetPosition(0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            currentlDrawLineParent.SetActive(true);
            lastNode = -1;
        }
    }

    public void endCaravanDrag()
    {
        if (!MM.paused)
        {
            pause(false);
        }
        setCaravanControllerRaycasts(true);
        selectingNodes = false;
        setNewCaravanPath();
        dragCancel.SetActive(false);
        //clear lines
        currentlyDrawing = false;
        currentlDrawLineParent.SetActive(false);
        clearLines();
    }

    public void cancelEnter()
    {
        pathCanceled = true;
        changeCancelOpacity(true);
    }

    public void cancelExit()
    {
        pathCanceled = false;
        changeCancelOpacity(false);
    }

    public int lastNode; //is -1 before any node is touched (start from caravan)
    public void addLine(int start, int end)
    {
        pathLines.Add(Instantiate(localLineCopy, lineParent, true));
        LineRenderer temp = pathLines[pathLines.Count-1].GetComponent<LineRenderer>();
        coord temp1 = new coord();
        if (start == -1)
        {
            temp.SetPosition(0, caravanFetcher(caravanBeingControlled).transform.localPosition);
        }
        else
        {
            temp1 = Library.locationCoords[start];
            temp.SetPosition(0, new Vector3((float)temp1.x, (float)temp1.y, 0));
        }

        coord temp2 = Library.locationCoords[end];
        temp.SetPosition(1, new Vector3((float)temp2.x, (float)temp2.y, 0));
        pathLines[pathLines.Count - 1].SetActive(true);

        
    }

    public void clearLines()
    {
        for (int x = 0; x < pathLines.Count; x++)
        {
            GameObject.Destroy(pathLines[x]);
        }
        pathLines = new List<GameObject>();
    }

    


}
