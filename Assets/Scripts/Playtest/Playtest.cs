using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playtest : MonoBehaviour
{
    public List<TimelineElements> elements;
     


    public void JumpToUnitTurn(TimelineElements element)
    {
        element.timelineFill = 100;
    }
}
