using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour {

    public BarracksManager BM;

    public GameObject barracksWindow;

    

	// Use this for initialization
	void Start () {
        SaveLoad.Load();
        barracksButton();
    }

    public void closeAllOthers()
    {
        barracksWindow.SetActive(false);




    }

    public void barracksButton()
    {
        closeAllOthers();
        barracksWindow.SetActive(true);
        BM.manualStart();
    }

}
