using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationNode : MonoBehaviour {

    public EventManager EM;
    public int locationID;

    public void mouseEnter()
    {
        if (EM.selectingNodes)
        {
            if (EM.nodePath.Count == 0)
            {
                AddFirstNode();
            }
            else
            {
                AddNode();
            }
        }

        //testCoords();
    }

    public void mouseExit()
    {
        
    }

    public void AddFirstNode()
    {
        if (SaveLoad.current.caravans[EM.caravanBeingControlled].progress == 0)
        {
            if (Library.pathAvailable(SaveLoad.current.caravans[EM.caravanBeingControlled].currentLocation, locationID))
            {
                EM.nodePath.Add(locationID);
                drawLine();
            }
            else
            {
                //Debug.Log("No Path Available");
            }
        }
        else //caravan is between currentlocation and the first int in its path List<int>
        {
            if (locationID == SaveLoad.current.caravans[EM.caravanBeingControlled].currentLocation || locationID == SaveLoad.current.caravans[EM.caravanBeingControlled].path[0])
            {
                EM.nodePath.Add(locationID);
                drawLine();
                EM.tempCurrentLocation = SaveLoad.current.caravans[EM.caravanBeingControlled].path[0];
            }
            else
            {
                //Debug.Log("No Path Available");
            }
        }
    }

    public void AddNode()
    {
        if (Library.pathAvailable(EM.nodePath[EM.nodePath.Count-1], locationID))
        {
            EM.nodePath.Add(locationID);
            drawLine();
        }
    }

    public void drawLine()
    {
        EM.addLine(EM.lastNode, locationID);
        EM.lastNode = locationID;
        EM.currentlDrawLine.SetPosition(0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void testCoords()
    {
        Debug.Log("Node " + locationID + " local coords: " + transform.localPosition);
    }


}
