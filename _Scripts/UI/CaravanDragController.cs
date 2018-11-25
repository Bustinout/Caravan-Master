using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanDragController : MonoBehaviour {

    public EventManager EM;
    public int caravanID;

    private bool dragged;

    public void mouseEnter()
    {
        EM.mouseOnCaravan = true;
        EM.caravanBeingControlled = caravanID;

    }

    public void mouseExit()
    {
        EM.mouseOnCaravan = false;
    }

}
