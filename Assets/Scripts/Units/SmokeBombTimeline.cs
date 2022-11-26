using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBombTimeline : MonoBehaviour
{
    public ItemRange range;
    [SerializeField] int decreaseAmmount;
    public void ApplyEffect(Board board)
    {
        List<Tile> tiles = range.GetTilesInRange(board);
        List<Unit> units = new List<Unit>();

        foreach(Tile t in tiles)
        {
            if(t.content != null)
            {
                if(t.content.GetComponent<Unit>() != null)
                {
                    units.Add(t.content.GetComponent<Unit>());
                }
            }
        }

        foreach(Unit u in units)
        {
            if(u.TimelineVelocity == 0)
            {
                continue;
            }

            u.DecreaseTimelineVelocity(decreaseAmmount);

            
        }
    }
}
