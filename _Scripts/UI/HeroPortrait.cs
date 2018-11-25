using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeroPortrait : MonoBehaviour {

    public CaravanFrame CF;
    public int heroIndex;

    public GameObject mainSceneToolTip;
    public Text mainSceneToolTipText;

    void Start () {
		
	}

    public void portraitClicked()
    {
        if (SaveLoad.current.caravans[CF.caravanSlot].unlocked)
        {
            if ((SaveLoad.current.caravans[CF.caravanSlot].heroes.Count - 1) >= heroIndex)
            {
                useAbility();
            }
        }
    }

	
	public void useAbility()
    {
        if (SaveLoad.current.caravans[CF.caravanSlot].inBattle)
        {
            Debug.Log(SaveLoad.current.caravans[CF.caravanSlot].heroes[heroIndex].Name + " uses combat ability!");
        }
        else if (SaveLoad.current.caravans[CF.caravanSlot].isDead)
        {
            Debug.Log(SaveLoad.current.caravans[CF.caravanSlot].heroes[heroIndex].Name + " is dead!");
        }
        else
        {
            Debug.Log(SaveLoad.current.caravans[CF.caravanSlot].heroes[heroIndex].Name + " uses travel ability!");
        }

    }

    public void setToolTipText()
    {
        if (SaveLoad.current.caravans[CF.caravanSlot].inBattle)
        {
            mainSceneToolTipText.text = SaveLoad.current.caravans[CF.caravanSlot].heroes[heroIndex].Name + " COMBAT SKILL TOOLTIP TEXT";
        }
        else
        {
            mainSceneToolTipText.text = SaveLoad.current.caravans[CF.caravanSlot].heroes[heroIndex].Name + " TRAVEL SKILL TOOLTIP TEXT";
        }
    }

    public void portraitMouseEnter()
    {
        if (!CF.MM.SW.dragging && !CF.MM.EM.currentlyDrawing)
        if (SaveLoad.current.caravans[CF.caravanSlot].heroes.Count > heroIndex)
        {
            setToolTipText();
            mainSceneToolTip.SetActive(true);
        }
    }

    public void portraitMouseExit()
    {
        mainSceneToolTip.SetActive(false);
    }






    //BARRACKS CARAVAN PORTRAITS
    public BarracksManager BM;
    private bool BMInteractable;
    public bool inactivePortrait;
    private bool selfDragged;

    public void refresh()
    {
        if (inactivePortrait)
        {
            if ((SaveLoad.current.inactiveHeroes.Count - 1) >= heroIndex)
            {
                BMInteractable = true;
                GetComponent<Button>().interactable = true;
                GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.inactiveHeroes[heroIndex].spriteLocation);
            }
            else
            {
                BMInteractable = false;
                GetComponent<Button>().interactable = false;
                GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Splash/emptySplash");
            }
        }
        else
        {
            if ((SaveLoad.current.caravans[BM.currentCaravanIndex].heroes.Count - 1) >= heroIndex)
            {
                BMInteractable = true;
                GetComponent<Button>().interactable = true;
                GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[heroIndex].spriteLocation);
            }
            else
            {
                BMInteractable = false;
                GetComponent<Button>().interactable = false;
                GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Splash/emptySplash");
            }
        }
        
        //refresh animation IF CHANGED
    }

    public void barracksMouseOverStart()
    {
        if (!selfDragged)
        {
            if (BMInteractable)
            {
                if (inactivePortrait)
                {
                    if (BM.draggingFromCaravanPortrait)
                    {
                        displayComparisonTooltip(true); 
                        //BM.readyToDropPortrait = true;
                        BM.inactiveSwapIndex = heroIndex;
                    }
                    else if (!BM.draggingFromBarracksPortrait)
                    {
                        displayTooltip();
                    }
                }
                else //caravan portrait 
                {
                    if (BM.draggingFromBarracksPortrait)
                    {
                        displayComparisonTooltip(true);
                        BM.readyToDropPortrait = true;
                        BM.caravanSwapIndex = heroIndex;
                    }
                    else
                    {
                        if (!BM.draggingFromCaravanPortrait)
                        {
                            displayTooltip();
                        }
                        else
                        {
                            //BM.readyToDropPortrait = true;
                            BM.caravanSwapIndex2 = heroIndex;
                        }
                    }

                }
            }
            else
            {
                if (!inactivePortrait) //dragging to empty caravan
                {
                    if (BM.draggingFromBarracksPortrait)
                    {
                        BM.readyToDropPortrait = true;
                        BM.caravanSwapIndex = heroIndex;
                    }
                    else
                    {
                        BM.readyToDropPortrait = false;
                        BM.swapToEndBool = true;
                    }
                }
            }
        }
        else
        {
            BM.readyToDropPortrait = false; //drag out then back in does not swap
        }

    }

    public void barracksMouseOverEnd()
    {
        if (BMInteractable)
        {
            BM.tooltip.SetActive(false);
        }
        BM.swapToEndBool = false;

        if (selfDragged && !inactivePortrait)
        {
            BM.readyToDropPortrait = true;
        }
        else
        {
            if (BM.draggingFromCaravanPortrait)
            {
                BM.inactiveSwapIndex = -1;
                BM.caravanSwapIndex2 = -1;
                BM.readyToDropPortrait = true;
                BM.compareText.text = BM.tooltipText.text;
            }
            else
            {
                BM.readyToDropPortrait = false;
                BM.compareText.text = BM.tooltipText.text;
            }
        }

    }

    public void barracksDragStart()
    {
        if (BMInteractable)
        {
            selfDragged = true;
            BM.tooltip.SetActive(false);

            BM.compareTextBox.SetActive(true);
            BM.compareText.text = BM.tooltipText.text;


            GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Splash/emptySplash");
            BM.DragHero.SetActive(true);

            if (inactivePortrait)
            {
                BM.inactiveSwapIndex = heroIndex;
                BM.draggingFromBarracksPortrait = true;
                BM.DragHero.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.inactiveHeroes[heroIndex].spriteLocation);
            }
            else //caravan portrait
            {
                BM.caravanSwapIndex = heroIndex;
                BM.draggingFromCaravanPortrait = true;
                BM.DragHero.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[heroIndex].spriteLocation);
            }
        }
        
    }

    public void barracksDragEnd()
    {
        if (BMInteractable)
        {
            selfDragged = false;
            if (inactivePortrait)
            {
                BM.draggingFromBarracksPortrait = false;
            }
            else //caravan portrait
            {
                BM.draggingFromCaravanPortrait = false;
            }
            BM.DragHero.SetActive(false);

            if (BM.readyToDropPortrait)
            {
                BM.swapHeroes();
            }
            else
            {
                if (BM.swapToEndBool) //reorder swap
                {
                    BM.swapToEnd();
                }
                BM.unreadySwap();
                refresh();
            }
        }
    }

    public void displayTooltip()
    {
        if (inactivePortrait)
        {
            BM.tooltipText.text = SaveLoad.current.inactiveHeroes[heroIndex].mouseOverInfo();
        }
        else
        {
            BM.tooltipText.text = SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[heroIndex].mouseOverInfo();
        }
        BM.tooltip.SetActive(true);
        BM.tooltip.transform.position = tooltipPosition();
    }

    public void displayComparisonTooltip(bool x)
    {
        if (x)
        {
            if (!inactivePortrait)
            {
                BM.compareText.text = SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[heroIndex].compareHeroes(SaveLoad.current.inactiveHeroes[BM.inactiveSwapIndex]);
            }
            else
            {
                BM.compareText.text = SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.caravanSwapIndex].compareHeroes(SaveLoad.current.inactiveHeroes[heroIndex]);
            }
            BM.compareTextBox.SetActive(true);
        }
        else
        {
            BM.compareTextBox.SetActive(false);
        }
    }

    public Vector3 tooltipPosition()
    {
        if (inactivePortrait)
        {
            return new Vector3(transform.position.x, transform.position.y + 2.2f, transform.position.z);
        }
        else
        {
            return new Vector3(transform.position.x, transform.position.y + 2.67f, transform.position.z);
        }
    }

    private bool inspectBlocked;
    public void inspectHero()
    {
        if (!selfDragged)
        {
            BM.inspectInactiveHero = inactivePortrait;
            BM.inspectHeroID = heroIndex;
            BM.inspectHero();
        }
    }

}
