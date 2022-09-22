using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionsExtensions 
{
    public static Directions GetDirections(this Tile t1, Tile t2) //Compare to tiles and get the direction relative to those tiles
    {
        if (t1.pos.y < t2.pos.y)
        {
            return Directions.North;
        }
        if (t1.pos.x < t2.pos.x)
        {
            return Directions.East;
        }
        if (t1.pos.y > t2.pos.y)
        {
            return Directions.South;
        }
        return Directions.West;
    }

    public static bool CheckSpecificDirection(this Tile t1, Tile t2, Directions directionToCheck) //Compare to tiles and get the direction relative to those tiles
    {
        switch (directionToCheck)
        {
            case Directions.North:
                if (t1.pos.y < t2.pos.y)
                {
                    return true;
                }
                return false;
            case Directions.East:
                if (t1.pos.x < t2.pos.x)
                {
                    return true;
                }
                return false;
            case Directions.South:
                if (t1.pos.y > t2.pos.y)
                {
                    return true;
                }
                return false;
            case Directions.West:
                if(t1.pos.x > t2.pos.x)
                {
                    return true;
                }
                return false;
            default:
                return false;
        }
    }
    public static Vector3 ToEuler (this Directions d) //Remember that an enum start at 0
    {
        return new Vector3(0, (int)d * 90, 0);
    }
}
