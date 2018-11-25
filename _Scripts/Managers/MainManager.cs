using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    public CaravanFrame caravanFrame1;
    public CaravanFrame caravanFrame2;
    public CaravanFrame caravanFrame3;
    public EventManager EM;
    public SelectedWindow SW;

    public GameObject mapCover;
    public GameObject selectedWindow;
    public int selectedCaravan;

    public bool paused;
    public Image pauseImage;
    public GameObject PauseText;

    public CaravanFrame FrameFetcher(int x)
    {
        if (x == 0)
        {
            return caravanFrame1;
        }
        else if (x == 1)
        {
            return caravanFrame2;
        }
        else
        {
            return caravanFrame3;
        }
    }

    void Start()
    {
        SaveLoad.Load();
        initializeAllFrames();
        getLocationNodes();
        EM.manualStart();

    }

    public void initializeAllFrames()
    {
        caravanFrame1.intializeFrame();
        caravanFrame2.intializeFrame();
        caravanFrame3.intializeFrame();
    }

    public void refreshAllFrames()
    {
        caravanFrame1.refreshText();
        caravanFrame2.refreshText();
        caravanFrame3.refreshText();
        if (selectedCaravan != -1)
        {
            SW.updateProgressBar();
            SW.updateTravelText();
        }
    }

    public void deselectAll()
    {
        caravanFrame1.selectedButton.SetActive(false);
        caravanFrame2.selectedButton.SetActive(false);
        caravanFrame3.selectedButton.SetActive(false);
    }

    public void closeSelected()
    {
        FrameFetcher(selectedCaravan).selectedButton.SetActive(false);
        selectedCaravan = -1;
        mapCover.SetActive(false);
        selectedWindow.SetActive(false);
    }

    public void setSelected(int x)
    {
        deselectAll();
        selectedCaravan = x;
        SW.initializeWindow();
        FrameFetcher(x).selectedButton.SetActive(true);
        mapCover.SetActive(true);
        selectedWindow.SetActive(true);
    }

    public CaravanFrame selectedFrame()
    {
        return FrameFetcher(selectedCaravan);
    }

    public void unPause()
    {
        EM.pause(false);
        paused = false;
        PauseText.SetActive(false);
    }

    public void Pause()
    {
        if (paused)
        {
            unPause();
            pauseImage.overrideSprite = Resources.Load<Sprite>("Sprites/UI/Pause");
        }
        else
        {
            EM.pause(true);
            paused = true;
            PauseText.SetActive(true);
            pauseImage.overrideSprite = Resources.Load<Sprite>("Sprites/UI/Resume");
        }
    }

    public GameObject locationNodeParent;
    public void getLocationNodes() //record positions of nodes
    {
        int counter = 0;
        foreach (Transform x in locationNodeParent.transform)
        {
            Library.locationCoords[counter] = new coord(x.transform.localPosition.x, x.transform.localPosition.y);
            //Debug.Log("Node " + counter + " - " + x.transform.localPosition.x + " " + x.transform.localPosition.y);
            counter++;
        }
    }

    public void toTown()
    {
        //SaveLoad.Save();
        SceneManager.LoadScene("Town");
    }

}
