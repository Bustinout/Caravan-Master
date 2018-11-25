using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectedWindow : MonoBehaviour {

    public MainManager MM;
    public Text caravanName;
    public Text travelText;

    public Image Splash1;
    public Image Splash2;
    public Image Splash3;
    public Image Splash4;

    public GameObject Portrait1;
    public GameObject Portrait2;
    public GameObject Portrait3;
    public GameObject Portrait4;

    public Slider progressBar;

    public GameObject MainWindow;
    public GameObject StatsWindow;
    public GameObject LootWindow;

    public Text DPSText;
    public Text SpeedText;
    public Text StaminaText;
    public Text LuckText;

    public GameObject lootPanel;
    public bool dragging;
    public bool deleting;
    public Image DeleteIcon;

    public GameObject DragItem;

    public Text GoldText;
    public Text GenRNText;
    public Text TnkRNText;
    public Text BrnRNText;
    public Text SctRNText;
    public Image artifactSprite;
    public Text artifactName;


    private void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            mousePos = new Vector3((mousePos.x * 801) - 450, (mousePos.y * 1425) - 780, -5);
            DragItem.transform.localPosition = mousePos;
        }
    }

    public void initializeWindow()
    {
        caravanName.text = SaveLoad.current.caravans[MM.selectedCaravan].partyName;
        setCaravanFrame();

        refreshSplashArt();
        updateProgressBar();
        skillsButton();
    }

    public void refreshSplashArt()
    {
        for (int x = 0; x < SaveLoad.current.caravans[MM.selectedCaravan].heroes.Count; x++)
        {
            if (x == 0)
            {
                Splash1.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[MM.selectedCaravan].heroes[x].spriteLocation);
            }
            else if (x == 1)
            {
                Splash2.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[MM.selectedCaravan].heroes[x].spriteLocation);
            }
            else if (x == 2)
            {
                Splash3.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[MM.selectedCaravan].heroes[x].spriteLocation);
            }
            else
            {
                Splash4.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[MM.selectedCaravan].heroes[x].spriteLocation);
            }
        }
    }

    public void setCaravanFrame()
    {

        Portrait1.GetComponent<HeroPortrait>().CF = MM.selectedFrame();
        Portrait2.GetComponent<HeroPortrait>().CF = MM.selectedFrame();
        Portrait3.GetComponent<HeroPortrait>().CF = MM.selectedFrame();
        Portrait4.GetComponent<HeroPortrait>().CF = MM.selectedFrame();

    }

    public void updateProgressBar()
    {
        progressBar.value = (float) SaveLoad.current.caravans[MM.selectedCaravan].progress;
    }

    public void updateTravelText()
    {
        travelText.text = SaveLoad.current.caravans[MM.selectedCaravan].getLocationText();
    }

    public void activateStatsText()
    {
        StatsWindow.SetActive(true);
        SaveLoad.current.caravans[MM.selectedCaravan].refreshDPS();
        DPSText.text = SaveLoad.current.caravans[MM.selectedCaravan].DPS + "";
        SpeedText.text = SaveLoad.current.caravans[MM.selectedCaravan].speed + "";
        LuckText.text = SaveLoad.current.caravans[MM.selectedCaravan].luck + "";
    }

    public void skillsButton()
    {
        MainWindow.SetActive(true);
        StatsWindow.SetActive(false);
        LootWindow.SetActive(false);
    }

    public void statsButton()
    {
        MainWindow.SetActive(false);
        activateStatsText();
        LootWindow.SetActive(false);
    }

    public void lootButton()
    {
        MainWindow.SetActive(false);
        StatsWindow.SetActive(false);
        refreshLoot();
        refreshLootTexts();
        LootWindow.SetActive(true);
    }

    public void refreshLoot()
    {
        foreach (Transform child in lootPanel.transform)
        {
            child.GetComponent<lootItem>().refresh();
        }
        changeDeleteOpacity(false);
    }

    public void refreshLootTexts()
    {
        GoldText.text = Library.convertValue(SaveLoad.current.caravans[MM.selectedCaravan].gold);
        GenRNText.text = "x " + SaveLoad.current.caravans[MM.selectedCaravan].researchNotes[0];
        TnkRNText.text = "x " + SaveLoad.current.caravans[MM.selectedCaravan].researchNotes[1];
        BrnRNText.text = "x " + SaveLoad.current.caravans[MM.selectedCaravan].researchNotes[2];
        SctRNText.text = "x " + SaveLoad.current.caravans[MM.selectedCaravan].researchNotes[3];

        //artifactSprite.overrideSprite = Resources.Load<Sprite>();
        //artifactName.text = "";

    }


























    public void mouseEnterDelete()
    {
        if (dragging)
        {
            deleting = true;
        }
    }

    public void mouseExitDelete()
    {
        if (dragging)
        {
            deleting = false;
        }
    }

    public void changeDeleteOpacity(bool x)
    {
        if (x)
        {
            Color c = DeleteIcon.color;
            c.a = 1f;
            DeleteIcon.color = c;
        }
        else
        {
            Color c = DeleteIcon.color;
            c.a = 0.5f;
            DeleteIcon.color = c;
        }
    }






}
