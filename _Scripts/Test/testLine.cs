using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLine : MonoBehaviour {

    private LineRenderer LR;


	// Use this for initialization
	void Start () {
        LR = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        LR.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
