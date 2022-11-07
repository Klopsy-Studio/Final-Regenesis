using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObstacleType
{
    Null,
    RegularObstacle,
}
[System.Serializable]
public class Tile : MonoBehaviour
{
    public TileData data;
    public int tileIndex;
    public const float stepHeight = 0.25f; 
    public Point pos;
    public int height;
    public Vector3 center { get { return new Vector3(pos.x, height * stepHeight, pos.y); } } //Center of the tile. Allow the units to center themselves

    [SerializeField] Renderer model;

    
    public string displayName;
    public GameObject content
    {
        get
        {
            return m_content;
        }
        set
        {
            m_content = value;

            if (content == null)
            {
                OnUnitLeave();
            }
            else
            {
                OnUnitArrive();
            }
        }
    }

    public bool occupied;

    public GameObject m_content = null;

    [HideInInspector] public Tile prev; //stores the tile which was traversed to reach it
    [HideInInspector] public int distance; //numbers of tiles which have been crossed to reach the point 

    public bool selected;

    [Header("Tile Selection")]
    public GameObject emptySelection;
    public GameObject selectMovementObject;
    public GameObject selectAttackObject;
    GameObject currentObject;
    [HideInInspector] public GameObject previousObject;
    public Material defaultMaterial;
    [HideInInspector] public Material prevMaterial;
    public Material attackMaterial;
    public Material movementMaterial;



    private void Start()
    {
        currentObject = emptySelection;
    }
    public void Match() //Matches the values of the variables with the gameObjects transforms values
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }

    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }
    
    public void Load (Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    public virtual void OnUnitArrive()
    {
        
    }

    public virtual void OnUnitLeave()
    {
      
    }


    public void ChangeTile(GameObject newSelection)
    {
        if(newSelection != null)
        {
            if (currentObject == null)
            {
                currentObject = newSelection;
                currentObject.transform.localPosition = new Vector3(newSelection.transform.localPosition.x, 0.505f, newSelection.transform.localPosition.z);
            }
            else
            {
                previousObject = currentObject;
                currentObject.transform.localPosition = new Vector3(currentObject.transform.localPosition.x, 0, currentObject.transform.localPosition.z);
                newSelection.transform.localPosition = new Vector3(newSelection.transform.localPosition.x, 0.505f, newSelection.transform.localPosition.z);
                currentObject = newSelection;
            }
        }

        else
        {
            if(currentObject != null)
            {
                currentObject.transform.localPosition = new Vector3(newSelection.transform.localPosition.x, 0.505f, newSelection.transform.localPosition.z);
            }
        }
        
    }

    public void ChangeTileToDefault()
    {
        if(currentObject != null)
        {
            previousObject = currentObject;
            currentObject.transform.localPosition = new Vector3(currentObject.transform.localPosition.x, 0, currentObject.transform.localPosition.z);
            currentObject = null;
        }
    }

    public Tile CheckSurroundings(Board board)
    {
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if(!IsTileValid(board, x, y))
                {
                    return null;
                }
            }
        }

        return this;
    }

    public bool IsTileValid(Board board, int offsetX, int offsetY)
    {
        if (board.GetTile(new Point(pos.x + offsetX, pos.y+offsetY)) != null)
        {
            if(/*!board.GetTile(new Point(pos.x + offsetX, pos.y + offsetY)).occupied && */board.GetTile(new Point(pos.x + offsetX, pos.y + offsetY)).content == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }


    public bool CheckNextTile(Directions dir, Board board)
    {
        switch (dir)
        {
            case Directions.North:
                if(board.GetTile(new Point(pos.x, pos.y--)) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Directions.East:
                if (board.GetTile(new Point(pos.x--, pos.y)) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Directions.South:
                if (board.GetTile(new Point(pos.x, pos.y++)) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Directions.West:
                if (board.GetTile(new Point(pos.x++, pos.y)) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }
}

