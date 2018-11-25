using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipItem : MonoBehaviour {

    public BarracksManager BM;

    public int slotID;

    public int realID; //for filtering

    public Image rarityBackground;
    public Image itemSprite;

    public bool stashItem;
    private bool interactable;
    private bool selfDragged;


    public void refresh()
    {
        if (!stashItem)
        {
            displayEquippedItem();
        }
        else
        {
            displayStashItem();
        }
    }

    public void displayEquippedItem()
    {
        if (!BM.inspectInactiveHero)
        {
            if (SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[slotID] != null)
            {
                rarityBackground.overrideSprite = Resources.Load<Sprite>(Library.getRarityBackground(SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[slotID].rarity));
                itemSprite.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[slotID].spriteLocation);
                interactable = true;
            }
            else
            {
                emptyItem();
            }
        }
        else
        {
            if (SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[slotID] != null)
            {
                rarityBackground.overrideSprite = Resources.Load<Sprite>(Library.getRarityBackground(SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[slotID].rarity));
                itemSprite.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[slotID].spriteLocation);
                interactable = true;
            }
            else
            {
                emptyItem();
            }
        }
    }

    public void displayStashItem()
    {
        if (SaveLoad.current.items.Count > slotID)
        {
            rarityBackground.overrideSprite = Resources.Load<Sprite>(Library.getRarityBackground(SaveLoad.current.items[slotID].rarity));
            itemSprite.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.items[slotID].spriteLocation);
            interactable = true;
        }
        else
        {
            emptyItem();
        }
    }

    public void emptyItem()
    {
        rarityBackground.overrideSprite = Resources.Load<Sprite>("Sprites/Splash/emptySplash");
        itemSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/blank");
        interactable = false;
    }

    public void mouseOverStart()
    {
        if (!selfDragged)
        {
            if (interactable)
            {
                if (!BM.draggingFromEquipped && !BM.draggingFromStash)
                {
                    displayTooltip();
                }



                if (!stashItem)
                {
                    if (BM.draggingFromStash && SaveLoad.current.items[BM.stashedSwapIndex].equipSlot == slotID)
                    {
                        displayComparisonText(true);
                        BM.readyToDropItem = true;
                        BM.equippedSwapIndex = slotID;
                    }
                    else if (BM.draggingFromEquipped)
                    {
                        BM.readyToDropItem = true;
                        BM.stashedSwapIndex = -1;
                    }
                    else
                    {
                        BM.readyToDropItem = false;
                    }
                }
                else
                {
                    if (BM.draggingFromEquipped)
                    {
                        BM.readyToDropItem = true;
                        if (BM.equippedSwapIndex == SaveLoad.current.items[slotID].equipSlot)
                        {
                            displayComparisonText(true);
                            BM.stashedSwapIndex = slotID;
                        }
                        else
                        {
                            BM.stashedSwapIndex = -1;
                        }
                    }
                    else
                    {
                        BM.readyToDropItem = false;
                    }
                }
            }
            else //dragging to an empty slot
            {
                if (!stashItem)
                {
                    if (BM.draggingFromStash && SaveLoad.current.items[BM.stashedSwapIndex].equipSlot == slotID)
                    {
                        BM.readyToDropItem = true;
                        BM.equippedSwapIndex = slotID;
                    }
                    else if (BM.draggingFromEquipped)
                    {
                        BM.readyToDropItem = true;
                        BM.stashedSwapIndex = -1;
                    }
                    else
                    {
                        BM.readyToDropItem = false;
                    }
                }
                else
                {
                    if (BM.draggingFromEquipped)
                    {
                        BM.readyToDropItem = true;
                        BM.stashedSwapIndex = -1;
                    }
                    else
                    {
                        BM.readyToDropItem = false;
                    }
                }
            }
        }
        else
        {
            BM.readyToDropItem = false;
        }
    }

    public void mouseOverEnd()
    {
        if (interactable)
        {
            BM.itemTooltip.SetActive(false);
            displayComparisonText(false);
        }
        if (selfDragged && !stashItem)
        {
            BM.readyToDropItem = true;
        }
        else
        {
            if (BM.draggingFromEquipped)
            {
                BM.stashedSwapIndex = -1;
                BM.readyToDropItem = true;
            }
            else
            {
                BM.readyToDropItem = false;
            }
        }
    }

    public void dragStart()
    {
        if (interactable)
        {
            selfDragged = true;
            BM.itemTooltip.SetActive(false);
            rarityBackground.overrideSprite = Resources.Load<Sprite>("Sprites/Splash/emptySplash");
            itemSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/blank");

            BM.DragItem.SetActive(true);
            BM.itemCompareText.text = BM.itemTooltipText.text;
            BM.itemCompareFlavorText.text = BM.flavorText.text;

            if (!stashItem)
            {
                BM.equippedSwapIndex = slotID;
                BM.draggingFromEquipped = true;
                if (!BM.inspectInactiveHero)
                {
                    BM.DragItem.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(Library.getRarityBackground(SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[slotID].rarity));
                    BM.DragItemSprite.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[slotID].spriteLocation);
                }
                else
                {
                    BM.DragItem.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(Library.getRarityBackground(SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[slotID].rarity));
                    BM.DragItemSprite.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[slotID].spriteLocation);
                }
            }
            else
            {
                BM.HW.chanceEquipOpacity(SaveLoad.current.items[slotID].equipSlot, false);

                BM.stashedSwapIndex = slotID;
                BM.draggingFromStash = true;
                BM.DragItem.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(Library.getRarityBackground(SaveLoad.current.items[slotID].rarity));
                BM.DragItemSprite.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(SaveLoad.current.items[slotID].spriteLocation);
            }
        }
    }

    public void dragEnd()
    {
        if (interactable)
        {
            selfDragged = false;
            if (!stashItem)
            {
                BM.draggingFromEquipped = false;
            }
            else
            {
                BM.HW.chanceEquipOpacity(SaveLoad.current.items[slotID].equipSlot, true);
                BM.draggingFromStash = false;
            }
            BM.DragItem.SetActive(false);

            if (BM.readyToDropItem)
            {
                BM.swapItems();
            }
            else
            {
                BM.unreadyItemSwap();
                refresh();
            }
        }
    }

    public void changeItemOpacity(bool x)
    {
        if (x)
        {
            Color c = rarityBackground.color;
            c.a = 1f;
            rarityBackground.color = c;

            Color c1 = itemSprite.color;
            c1.a = 1f;
            itemSprite.color = c1;
        }
        else
        {
            Color c = rarityBackground.color;
            c.a = 0.5f;
            rarityBackground.color = c;

            Color c1 = itemSprite.color;
            c1.a = 0.5f;
            itemSprite.color = c1;
        }
    }

    public void displayTooltip()
    {
        if (stashItem)
        {
            BM.itemTooltipText.text = SaveLoad.current.items[slotID].infoString();
            BM.flavorText.text = SaveLoad.current.items[slotID].flavorText;
        }
        else
        {
            if (!BM.inspectInactiveHero)
            {
                BM.itemTooltipText.text = SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[slotID].infoString();
                BM.flavorText.text = SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[slotID].flavorText;
            }
            else
            {
                BM.itemTooltipText.text = SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[slotID].infoString();
                BM.flavorText.text = SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[slotID].flavorText;
            }
        }
        BM.itemTooltip.SetActive(true);
        BM.itemTooltip.transform.position = tooltipPosition();
    }

    public Vector3 tooltipPosition()
    {
        return new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
    }


    public void displayComparisonText(bool x)
    {
        if (x)
        {
            if (!stashItem)
            {
                if (!BM.inspectInactiveHero)
                {
                    BM.itemCompareText.text = SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[slotID].compareItems(SaveLoad.current.items[BM.stashedSwapIndex]);
                }
                else
                {
                    BM.itemCompareText.text = SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[slotID].compareItems(SaveLoad.current.items[BM.stashedSwapIndex]);
                }
            }
            else
            {
                if (!BM.inspectInactiveHero)
                {
                    BM.itemCompareText.text = SaveLoad.current.caravans[BM.currentCaravanIndex].heroes[BM.inspectHeroID].equipped[BM.equippedSwapIndex].compareItems(SaveLoad.current.items[slotID]);
                }
                else
                {
                    BM.itemCompareText.text = SaveLoad.current.inactiveHeroes[BM.inspectHeroID].equipped[BM.equippedSwapIndex].compareItems(SaveLoad.current.items[slotID]);
                }
            }
        }
        else
        {
            BM.itemCompareText.text = BM.itemTooltipText.text;
            BM.itemCompareFlavorText.text = BM.flavorText.text;
        }

    }

}
