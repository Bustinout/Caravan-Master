using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroWindow : MonoBehaviour {

    public BarracksManager BM;
    public GameObject equippedItemsPanel;

    public bool editable; // unable to edit when active

    public Text nameText;
    public Text classText;
    public Text statText;

    public GameObject equip0;
    public GameObject equip1;
    public GameObject equip2;
    public GameObject equip3;
    public GameObject equip4;
    public GameObject equip5;
    public GameObject equip6;
    public GameObject equip7;

    public void manualStart()
    {
        if (!BM.inspectInactiveHero && !SaveLoad.current.caravans[BM.currentCaravanIndex].inTown)
        {
            editable = false;
        }
        else
        {
            editable = true;
        }

        refresh();
    }

    public void refresh()
    {
        if (!BM.inspectInactiveHero) //caravan hero
        {
            showInfo(SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID]);
        }
        else
        {
            showInfo(SaveLoad.current.inactiveHeroes[BM.inspectHeroID]);
        }
        foreach (equipItem x in equippedItemsPanel.GetComponentsInChildren<equipItem>())
        {
            x.refresh();
        }
        BM.refreshItems();
    }

    public void showInfo(Hero x)
    {
        //start animation (but only first time)
        nameText.text = x.Name;
        classText.text = x.classString();
        statText.text = x.statString();
    }

    public void closeWindow()
    {
        BM.heroWindow.SetActive(false);
    }

    public void sortItems()
    {
        SaveLoad.current.sortItems();
        BM.refreshItems();
    }

    public GameObject equipFetcher(int x)
    {
        if (x == 0)
        {
            return equip0;
        }
        else if (x == 1)
        {
            return equip1;
        }
        else if (x == 2)
        {
            return equip2;
        }
        else if (x == 3)
        {
            return equip3;
        }
        else if (x == 4)
        {
            return equip4;
        }
        else if (x == 5)
        {
            return equip5;
        }
        else if (x == 6)
        {
            return equip6;
        }
        else 
        {
            return equip7;
        }
    }

    public void chanceEquipOpacity(int x, bool y)
    {
        if (y)
        {
            foreach (Image i in equipFetcher(x).GetComponentsInChildren<Image>())
            {
                Color c = i.color;
                c.r = 1f;
                c.g = 1f;
                c.b = 1f;
                i.color = c;
            }
        }
        else
        {
            foreach (Image i in equipFetcher(x).GetComponentsInChildren<Image>())
            {
                Color c = i.color;
                c.r = 0.5f;
                c.g = 0.5f;
                c.b = 0.5f;
                i.color = c;
            }
        }
    }

}
