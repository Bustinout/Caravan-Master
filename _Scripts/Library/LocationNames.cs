using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationNames {

	public static string getName(int x)
    {
        if (x == 0)
        {
            return "Town";
        }
        else if (x == 1)
        {
            return "Glacial Peak";
        }
        else if (x == 2)
        {
            return "Whispering Woods";
        }
        else if (x == 3)
        {
            return "Volcanic Wastes";
        }
        else if (x == 4)
        {
            return "The Deep Dark";
        }
        else //if (x == 5)
        {
            return "Sands of Sharim";
        }
    }
}
