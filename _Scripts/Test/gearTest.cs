using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gearTest : MonoBehaviour {

	// Use this for initialization
	void Start () {


        Gear x = new Gear().generateGear("Common");

        x.identified = true;

        Debug.Log(x.infoString());















	}
	
}
