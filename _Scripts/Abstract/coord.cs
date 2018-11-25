using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coord {

    public double x;
    public double y;

    public coord()
    {

    }

    public coord(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public static coord addCoords(coord x1, coord x2)
    {
        return new coord((x1.x + x2.x), (x1.y + x2.y));
    }

    public static coord subCoords(coord x1, coord x2)
    {
        return new coord((x1.x - x2.x), (x1.y - x2.y));
    }

    public static coord multCoords(coord x1, double mult)
    {
        return new coord((x1.x * mult), (x1.y * mult));
    }


}
