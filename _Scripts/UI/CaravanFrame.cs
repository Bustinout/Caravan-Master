using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaravanFrame : MonoBehaviour {

    public MainManager MM;

    public int caravanSlot; //0, 1, 2


    public GameObject lockedButton;
    public GameObject selectedButton;

    public UnityEngine.UI.Text locationText;
    public UnityEngine.UI.Text DPSText;
    public UnityEngine.UI.Text SpeedText;

    public Image Splash1;
    public Image Splash2;
    public Image Splash3;
    public Image Splash4;

    public GameObject DPSInfo;
    public GameObject HPInfo;
    public GameObject TravelInfo;
    public GameObject CombatInfo;

    // Use this for initialization
    void Start () {

    }
	
    public void intializeFrame()
    {
        if (SaveLoad.current.caravans[caravanSlot].unlocked)
        {
            locationText.text = SaveLoad.current.caravans[caravanSlot].getLocationText();
            DPSText.text = SaveLoad.current.caravans[caravanSlot].DPS + "";
            SpeedText.text = SaveLoad.current.caravans[caravanSlot].speed + "";
            refreshSplashArt();
            setMode();
        }
        else
        {
            lockedButton.SetActive(true);
        }
    }

    public void refreshText()
    {
        locationText.text = SaveLoad.current.caravans[caravanSlot].getLocationText();
        DPSText.text = SaveLoad.current.caravans[caravanSlot].DPS + "";
        SpeedText.text = SaveLoad.current.caravans[caravanSlot].speed + "";
    }

    public void refreshSplashArt()
    {
        for (int x = 0; x < SaveLoad.current.caravans[caravanSlot].heroes.Count; x++)
        {
            if (x == 0)
            {
                Splash1.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[caravanSlot].heroes[x].spriteLocation);
            }
            else if (x == 1)
            {
                Splash2.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[caravanSlot].heroes[x].spriteLocation);
            }
            else if (x == 2)
            {
                Splash3.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[caravanSlot].heroes[x].spriteLocation);
            }
            else
            {
                Splash4.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[caravanSlot].heroes[x].spriteLocation);
            }
        }
    }

    public void setMode()
    {
        if (SaveLoad.current.caravans[caravanSlot].inBattle)
        {
            DPSInfo.SetActive(true);
            HPInfo.SetActive(false);
            TravelInfo.SetActive(false);
            CombatInfo.SetActive(true);

            refreshCombatNames();
            refreshCombatBar();
            refreshEnemyCombatBar();
        }
        else
        {
            DPSInfo.SetActive(false);
            HPInfo.SetActive(true);
            TravelInfo.SetActive(true);
            CombatInfo.SetActive(false);

            refreshHPBar();
        }
    }

    public Slider HPBar;
    public Slider CombatHPBar;
    public Slider CombatEnemyBar;

    public Text HPText;
    public Text CombatName;
    public Text CombatEnemyName;
    public Text CombatHPText;
    public Text CombatEnemyHPText;

    public void refreshCombatNames()
    {
        CombatName.text = SaveLoad.current.caravans[caravanSlot].partyName;
        CombatEnemyName.text = SaveLoad.current.caravans[caravanSlot].currentEvent.monster.name;
    }

    public void refreshCombatBar()
    {
        CombatHPBar.value = (float)(SaveLoad.current.caravans[caravanSlot].HP / SaveLoad.current.caravans[caravanSlot].maxHP);
        CombatHPText.text = SaveLoad.current.caravans[caravanSlot].HP + "/" + SaveLoad.current.caravans[caravanSlot].maxHP;
    }

    public void refreshEnemyCombatBar()
    {
        int temp = SaveLoad.current.caravans[caravanSlot].currentEvent.monster.hp;
        int temp2 = (int) (temp - SaveLoad.current.caravans[caravanSlot].damageDone);
        CombatEnemyBar.value = (float)temp2 / (float)temp;
        CombatEnemyHPText.text = (temp2) + "/" + temp;
    }

    public void refreshHPBar()
    {
        HPBar.value = (float) (SaveLoad.current.caravans[caravanSlot].HP / SaveLoad.current.caravans[caravanSlot].maxHP);
        HPText.text = SaveLoad.current.caravans[caravanSlot].HP + "/" + SaveLoad.current.caravans[caravanSlot].maxHP;
    }

    public bool selected;

    public void setSelected()
    {
        if (SaveLoad.current.caravans[caravanSlot].unlocked)
        {

            MM.setSelected(caravanSlot);

        }
    }

}
