using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksManager : MonoBehaviour {

    public TownManager TM;

    public Text caravanName;
    public Text tooltipText;
    public Text itemTooltipText;
    public Text compareText;
    public Text itemCompareText;
    public Text itemCompareFlavorText;
    public Text flavorText;

    public int currentCaravanIndex;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject DragHero;
    public GameObject DragItem;
    public GameObject DragItemSprite;
    public GameObject inactiveHeroesPanel;
    public GameObject tooltip;
    public GameObject itemTooltip;
    public GameObject compareTextBox;


    public HeroPortrait HP1;
    public HeroPortrait HP2;
    public HeroPortrait HP3;
    public HeroPortrait HP4;

    private int numberOfUnlocked;



    public bool draggingFromCaravanPortrait;
    public bool draggingFromBarracksPortrait;

    public bool draggingFromEquipped;
    public bool draggingFromStash;


    private void Update()
    {
        if (draggingFromCaravanPortrait || draggingFromBarracksPortrait)
        {
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            mousePos = new Vector3((mousePos.x * 801) - 400, (mousePos.y * 1425) - 700, -5);
            DragHero.transform.localPosition = mousePos;
        }

        if (draggingFromEquipped || draggingFromStash)
        {
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            mousePos = new Vector3((mousePos.x * 801) - 400, (mousePos.y * 1425) - 700, -5);
            DragItem.transform.localPosition = mousePos;
        }
    }

    // Use this for initialization
    public void manualStart() {
        currentCaravanIndex = 0;
        getNumberOfUnlocked();
        if (numberOfUnlocked == 1)
        {
            disableArrows();
        }

        refresh();
    }

    public void refresh() // refresh temp info
    {
        caravanName.text = SaveLoad.current.caravans[currentCaravanIndex].partyName;

        HP1.refresh();
        HP2.refresh();
        HP3.refresh();
        HP4.refresh();

        foreach (HeroPortrait x in inactiveHeroesPanel.GetComponentsInChildren<HeroPortrait>())
        {
            x.refresh();
        }
    }

    

    public void getNumberOfUnlocked()
    {
        numberOfUnlocked = 0;
        foreach (Caravan x in SaveLoad.current.caravans)
        {
            if (x.unlocked)
            {
                numberOfUnlocked++;
            }
        }
    }

    

    public void disableArrows()
    {
        Color c = rightArrow.GetComponent<Image>().color;
        c.a = 0.5f;
        rightArrow.GetComponent<Image>().color = c;
        leftArrow.GetComponent<Image>().color = c;
        rightArrow.GetComponent<Button>().interactable = false;
        leftArrow.GetComponent<Button>().interactable = false;
    }

    public void nextCaravan()
    {
        currentCaravanIndex = (currentCaravanIndex + 1) % numberOfUnlocked;
        refresh();
    }

    public void prevCaravan()
    {
        currentCaravanIndex = ((currentCaravanIndex - 1 + numberOfUnlocked) % numberOfUnlocked);
        refresh();
    }




    //Swapping Heroes
    public bool readyToDropPortrait;
    public int caravanSwapIndex = -1;
    public int inactiveSwapIndex = -1;
    public int caravanSwapIndex2 = -1;

    public void unreadySwap()
    {
        readyToDropPortrait = false;
        caravanSwapIndex = -1;
        inactiveSwapIndex = -1;
        caravanSwapIndex2 = -1;
    }

    public void swapHeroes()
    {
        if (inactiveSwapIndex >= 0) //swap caravan and inactive
        {
            if (caravanSwapIndex >= SaveLoad.current.caravans[currentCaravanIndex].heroes.Count) //dragging to empty caravan slot
            {
                SaveLoad.current.caravans[currentCaravanIndex].heroes.Add(SaveLoad.current.inactiveHeroes[inactiveSwapIndex]);
                SaveLoad.current.inactiveHeroes.RemoveAt(inactiveSwapIndex);
            }
            else
            {
                Hero temp = SaveLoad.current.inactiveHeroes[inactiveSwapIndex];
                SaveLoad.current.inactiveHeroes[inactiveSwapIndex] = SaveLoad.current.caravans[currentCaravanIndex].heroes[caravanSwapIndex];
                SaveLoad.current.caravans[currentCaravanIndex].heroes[caravanSwapIndex] = temp;
            }
        }
        else if (caravanSwapIndex2 >= 0)
        {
            Hero temp = SaveLoad.current.caravans[currentCaravanIndex].heroes[caravanSwapIndex2];
            SaveLoad.current.caravans[currentCaravanIndex].heroes[caravanSwapIndex2] = SaveLoad.current.caravans[currentCaravanIndex].heroes[caravanSwapIndex];
            SaveLoad.current.caravans[currentCaravanIndex].heroes[caravanSwapIndex] = temp;
        }
        else //dragged caravan to nowhere (remove from caravan)
        {
            SaveLoad.current.inactiveHeroes.Add(SaveLoad.current.caravans[currentCaravanIndex].heroes[caravanSwapIndex]);
            SaveLoad.current.caravans[currentCaravanIndex].heroes.RemoveAt(caravanSwapIndex);
        }

        unreadySwap();
        refresh();
    }

    public bool swapToEndBool;
    public void swapToEnd() //move dragged hero to the end of caravan
    {
        Hero temp = SaveLoad.current.caravans[currentCaravanIndex].heroes[caravanSwapIndex];
        SaveLoad.current.caravans[currentCaravanIndex].heroes.RemoveAt(caravanSwapIndex);
        SaveLoad.current.caravans[currentCaravanIndex].heroes.Add(temp);
        swapToEndBool = false;
    }

    public GameObject heroWindow;
    public heroWindow HW;
    public bool inspectInactiveHero;
    public int inspectHeroID;

    public void inspectHero()
    {
        HW.manualStart();
        heroWindow.SetActive(true);
    }

    public GameObject itemsPanel;
    public void refreshItems()
    {
        foreach (equipItem x in itemsPanel.GetComponentsInChildren<equipItem>())
        {
            x.refresh();
        }
    }

    //item swapping
    public bool readyToDropItem;
    public int equippedSwapIndex = -1;
    public int stashedSwapIndex = -1;

    public void unreadyItemSwap()
    {
        readyToDropItem = false;
        equippedSwapIndex = -1;
        stashedSwapIndex = -1;
    }

    public void swapItems() // and equip
    {
        if (!inspectInactiveHero)
        {
            if (stashedSwapIndex == -1)
            {
                SaveLoad.current.caravans[currentCaravanIndex].heroes[inspectHeroID].unequipGear(equippedSwapIndex);
            }
            else
            {
                Gear temp = SaveLoad.current.items[stashedSwapIndex];
                SaveLoad.current.items.Remove(temp);
                SaveLoad.current.caravans[currentCaravanIndex].heroes[inspectHeroID].equipGear(temp);
            }
        }
        else
        {
            if (stashedSwapIndex == -1)
            {
                SaveLoad.current.inactiveHeroes[inspectHeroID].unequipGear(equippedSwapIndex);
            }
            else
            {
                Gear temp = SaveLoad.current.items[stashedSwapIndex];
                SaveLoad.current.items.Remove(temp);
                SaveLoad.current.inactiveHeroes[inspectHeroID].equipGear(temp);
            }
        }
        unreadyItemSwap();
        HW.refresh();
    }

    public void sortHeroes()
    {
        SaveLoad.current.sortInactiveHeroes();
        refresh();
    }

    public void closeWindow()
    {
        TM.barracksWindow.SetActive(false);
    }

}
