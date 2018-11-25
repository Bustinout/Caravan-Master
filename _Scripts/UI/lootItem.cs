using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lootItem : MonoBehaviour {

    public MainManager MM;
    public int slotID;
    public Image rarityBackground;
    public Image itemSprite;
    public GameObject textBox;
    public Text selectedText;

    public void refresh()
    {
        if (slotID < SaveLoad.current.caravans[MM.selectedCaravan].items.Count)
        {
            updateSprites();
            selectedText.text = SaveLoad.current.caravans[MM.selectedCaravan].items[slotID].infoString();
        }
        else
        {
            rarityBackground.overrideSprite = Resources.Load<Sprite>("Sprites/Splash/emptySplash");
            itemSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/blank");
        }
    }

    public void resetSprites()
    {

    }

    public void updateSprites()
    {
        rarityBackground.overrideSprite = Resources.Load<Sprite>(Library.getRarityBackground(SaveLoad.current.caravans[MM.selectedCaravan].items[slotID].rarity));
        itemSprite.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[MM.selectedCaravan].items[slotID].spriteLocation);
    }

    public void mouseOverStart()
    {
        if (slotID < SaveLoad.current.caravans[MM.selectedCaravan].items.Count && !MM.SW.dragging)
        {
            textBox.SetActive(true);
        }
    }

    public void mouseOverEnd()
    {
        textBox.SetActive(false);
    }

    public GameObject dragSprite;
    public Image dragBackground;
    public Image dragItemSprite;

    public void startDrag()
    {
        if (slotID < SaveLoad.current.caravans[MM.selectedCaravan].items.Count)
        {
            mouseOverEnd();
            MM.SW.dragging = true;
            MM.SW.changeDeleteOpacity(true);

            dragBackground.overrideSprite = Resources.Load<Sprite>("ItemIcons/Rarity/common");
            dragItemSprite.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.caravans[MM.selectedCaravan].items[slotID].spriteLocation);
            dragSprite.SetActive(true);

            rarityBackground.overrideSprite = Resources.Load<Sprite>("Sprites/Splash/emptySplash");
            itemSprite.overrideSprite = Resources.Load<Sprite>("Sprites/UI/blank");
        }
    }

    public void endDrag()
    {
        dragSprite.SetActive(false);

        if (MM.SW.deleting)
        {
            SaveLoad.current.caravans[MM.selectedCaravan].items.RemoveAt(slotID);
            MM.SW.refreshLoot();
            MM.SW.deleting = false;
        }
        refresh();
        MM.SW.dragging = false;
        MM.SW.changeDeleteOpacity(false);
    }

}
