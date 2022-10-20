using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public enum propType
{
    //Here we add every single prop we have
}
public class BoardCreator : MonoBehaviour
{
    public TileSpawner spawner;
    [SerializeField] GameObject tileToSpawn;


    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject tileSelectionIndicatorPrefab;

    Transform marker
    {
        get
        {
            if(_marker == null)
            {
                GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
                _marker = instance.transform;
            }
            return _marker;
        }
      
    }

    Transform _marker;

    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    public List<Tile> tilesScript = new List<Tile>();
    public List<GameObject> tilesObstacles = new List<GameObject>();

    [Header("Spawn Points")]
    [SerializeField] int howManyPlayers = 2;
    [SerializeField] int howManyEnemies = 1;
    [SerializeField] GameObject playerSpawnPrefab;
    [SerializeField] Dictionary<Point, GameObject> playerSpawnDictionary = new Dictionary<Point, GameObject>();
    [SerializeField] GameObject enemySpawnPrefab;
    [SerializeField] Dictionary<Point, GameObject> enemySpawnDictionary = new Dictionary<Point, GameObject>();

    [Header("Tile Variables")]
    public List<Point> playerSpawnPoints = new List<Point>();
    public List<Point> enemySpawnPoints = new List<Point>();
    [SerializeField] int width = 10;
    [SerializeField] int depth = 10;
    [SerializeField] int height = 8;
    [SerializeField] Point pos;
    [SerializeField] LevelData levelData;


    //WE HAVE TO ADD A NEW METHOD TO UPDATE THE DICTIONARY TILES WHEN WE MOVE A TILE, TO SAVE IT LATER.

    [Header("Mission Data Variables")]

    [SerializeField] private int position;
    [SerializeField] public int rank;
    [SerializeField] public string missionName;
    [SerializeField] private Type missionType;

    [Header("Environment information")]
    [SerializeField] public Zone zone;
    [SerializeField] public Hazard hazard;
    [SerializeField] public List<string> otherCreatures;

    [Header("Rewards")]
    [SerializeField] public int money;
    [SerializeField] public List<string> items;

    [Header("Tile Spawn")]
    public TileType TypeOfTile;
    public bool makeTilePlayable;
    public void GrowArea()
    {
        Rect r = RandomRect();
        GrowRect(r);
    }
    public void ShrinkArea()
    {
        Rect r = RandomRect();
        ShrinkRect(r);
    }

    Rect RandomRect()
    {
        int x = UnityEngine.Random.Range(0, width);
        int y = UnityEngine.Random.Range(0, depth);
        int w = UnityEngine.Random.Range(1, width - x + 1);
        int h = height;/* UnityEngine.Random.Range(1, depth - y + 1);*/
        return new Rect(x, y, w, h);
    }

    void GrowRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                GrowSingle(p);
            }
        }
    }

    void ShrinkRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                ShrinkSingle(p);
            }
        }
    }

    Tile Create()
    {
        GameObject instance = Instantiate(tileToSpawn) as GameObject;
        instance.transform.parent = transform;
        return instance.GetComponent<Tile>();
        //switch (chosenTile)
        //{
        //    case TypesOfTiles.DessertTile1:

        //        GameObject instance = Instantiate(dessertTile1) as GameObject;
        //        instance.transform.parent = transform;
        //        return instance.GetComponent<Tile>();
        //    case TypesOfTiles.DessertTile2:
        //        instance = Instantiate(dessertTile2) as GameObject;
        //        instance.transform.parent = transform;
        //        return instance.GetComponent<Tile>();

        //    case TypesOfTiles.DessertTile3:
        //        instance = Instantiate(dessertTile3) as GameObject;
        //        instance.transform.parent = transform;
        //        return instance.GetComponent<Tile>();

        //    case TypesOfTiles.QuicksandTile:
        //        instance = Instantiate(quicksandTile) as GameObject;
        //        instance.transform.parent = transform;
        //        return instance.GetComponent<Tile>();
        //    default:
        //        return null;
        //}
    }

    Tile GetOrCreate(Point p)
    {
        if (tiles.ContainsKey(p))
            return tiles[p];

        Tile t = Create();
        t.Load(p, 0);
        tiles.Add(p, t);
        tilesScript.Add(t);
        return t;
    }

    public void LoadTiles(Tile t)
    {
        switch (t.data.tileType)
        {
            case TileType.Placeholder:
                switch (t.tileIndex)
                {
                    case 0:
                        
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
                break;
            case TileType.Desert:
                break;
            case TileType.NonPlayable:
                break;
            default:
                break;
        }
    }
    public void AddObstacle()
    {
        Tile t = GetOrCreate(pos);

        if(t.content == null)
        {
            GameObject instance = Instantiate(obstaclePrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
            instance.transform.parent = transform;
            t.content = instance;
            tilesObstacles.Add(instance);
        }
    }
    void GrowSingle(Point p)
    {
        Tile t = GetOrCreate(p);

        if (makeTilePlayable)
        {
            t.data.isPlayable = true;
        }
        else
        {
            t.data.isPlayable = false;
        }
        if (t.height < height)
            t.Grow();
    }

    void ShrinkSingle(Point p)
    {
        if (!tiles.ContainsKey(p))
        {
            Debug.Log("No key");
            return;
        }
            

        Tile t = tiles[p];
        tiles.Remove(p);
        tilesScript.Remove(t);
        DestroyImmediate(t.gameObject);
    }

    public void Grow()
    {
        GrowSingle(pos);
    }
    public void Shrink()
    {
        ShrinkSingle(pos);
    }

    public void UpdateMarker()
    {
        Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
        marker.localPosition = t != null ? t.center : new Vector3(pos.x, 0, pos.y);
    }

    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
        for (int i = 0; i < tilesObstacles.Count; i++)
            DestroyImmediate(tilesObstacles.ToArray()[i]);

        tilesObstacles.Clear();
        tiles.Clear();
        tilesScript.Clear();
        //ClearEnemySpawnPoints();
        ClearPlayerSpawnPoints();

        rank = 0;
        missionName = null;
        missionType = Type.Hunt;
        zone = Zone.Desert;
        hazard = Hazard.Thunderstorm;

        otherCreatures = null;

        money = 0;

        items = null;

    }

    public void Save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        LevelData board = ScriptableObject.CreateInstance<LevelData>();

        board.tiles = new List<Vector3>(tilesScript.Count);

        board.tileData = new List<TileData>(tilesScript.Count);
        board.tileContent = new List<ObstacleType>(tilesScript.Count);
        board.playerSpawnPoints = new List<Point>(playerSpawnPoints.Count);
        board.enemySpawnPoints = new List<Point>(enemySpawnPoints.Count);

        foreach (Tile t in tiles.Values)
        {
            board.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));

            if (t.content != null)
            {
                board.tileContent.Add(ObstacleType.RegularObstacle);
            }
            else
            {
                board.tileContent.Add(ObstacleType.Null);
            }
        }

        foreach(Point p in playerSpawnPoints)
        {
            board.playerSpawnPoints.Add(p);
        }

        foreach(Point p in enemySpawnPoints)
        {
            board.enemySpawnPoints.Add(p);
        }

        foreach(Tile t in tilesScript)
        {
            board.tileData.Add(t.data);
        }

        board.rank = rank;
        board.missionName = missionName;
        board.type = missionType;

        board.zone = zone;
        board.hazard = hazard;
        board.otherCreatures = otherCreatures;
        board.money = money;
        board.items = items;


        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, "Mission_"+rank+"_"+position);
        AssetDatabase.CreateAsset(board, fileName);
    }
    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }

    public void Load()
    {
        Clear();
        if (levelData == null)
        {
            Debug.Log("No level data");
            return;
        }
            

        for (int i = 0; i < levelData.tiles.Count; i++)
        {
            Tile t = LoadTileBoard(i);
            t.Load(levelData.tiles.ToArray()[i]);

            if(levelData.tileContent.ToArray()[i] == ObstacleType.RegularObstacle)
            {
                GameObject instance = Instantiate(obstaclePrefab, new Vector3(t.pos.x, 0, t.pos.y), Quaternion.identity);
                t.content = instance;
                instance.transform.parent = t.transform;
                tilesObstacles.Add(instance);
            }

            tiles.Add(t.pos, t);
            tilesScript.Add(t);
        }

        foreach (Point p in levelData.playerSpawnPoints)
        {
            playerSpawnPoints.Add(p);
        }

        foreach (Point p in levelData.enemySpawnPoints)
        {
            enemySpawnPoints.Add(p);
        }

        rank = levelData.rank;
        missionName = levelData.missionName;
        missionType = levelData.type;
        zone = levelData.zone;
        hazard = levelData.hazard;

        if (levelData.otherCreatures != null)
        {
            otherCreatures = levelData.otherCreatures;
        }

        money = levelData.money;

        if (levelData.items != null)
        {
            items = levelData.items;
        }
    }

    public Tile LoadTileBoard(int index)
    {
        switch (levelData.tileData.ToArray()[index].tileType)
        {
            case TileType.Placeholder:
                GameObject instance = Instantiate(spawner.placeholderTiles[levelData.tileData[index].typeIndex]);
                Tile t = instance.GetComponent<Tile>();
                t.data.isPlayable = levelData.tileData.ToArray()[index].isPlayable;
                instance.transform.parent = transform;

                return t;
            case TileType.Desert:
                instance = Instantiate(spawner.desertTiles[levelData.tileData[index].typeIndex]);
                t = instance.GetComponent<Tile>();
                t.data.isPlayable = levelData.tileData.ToArray()[index].isPlayable;
                instance.transform.parent = transform;

                return t;
            case TileType.NonPlayable:
                instance = Instantiate(spawner.nonPlayableTiles[levelData.tileData[index].typeIndex]);
                t = instance.GetComponent<Tile>();
                t.data.isPlayable = levelData.tileData.ToArray()[index].isPlayable;
                instance.transform.parent = transform;

                return t;
            default:
                return null;
        }
    }
    public void MoveTileSelectionUpwards()
    {
        pos += new Point(0, 1);
    }
    
    public void MoveTileSelectionDownwards()
    {
        pos -= new Point(0, 1);
    }

    public void MoveTileSelectionLeft()
    {
        pos -= new Point(1, 0);
    }
    public void MoveTileSelectionRight()
    {
        pos += new Point(1, 0);
    }

    public void MoveTileSelection(Vector2 mousePosition)
    {
        pos = new Point((int)mousePosition.x, (int)mousePosition.y);
    }
    public void ClearPlayerSpawnPoints()
    {
        //foreach(Point p in playerSpawnPoints)
        //{
        //    if(playerSpawnDictionary[p].gameObject != null)
        //    {
        //        DestroyImmediate(playerSpawnDictionary[p].gameObject);
        //    }
        //}

        playerSpawnDictionary.Clear();
        playerSpawnPoints.Clear();
    }
    public void AddPlayerSpawnPoint()
    {
        AddSpawnPoint(playerSpawnPoints, howManyPlayers);

        GameObject instance = Instantiate(playerSpawnPrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
        instance.transform.parent = transform;
        playerSpawnDictionary.Add(pos, instance);
    }

    public void ClearEnemySpawnPoints()
    {
        //foreach (Point p in enemySpawnPoints)
        //{
        //    DestroyImmediate(enemySpawnDictionary[p].gameObject);
        //}
        enemySpawnDictionary.Clear();
        enemySpawnPoints.Clear();
    }
    public void AddEnemySpawnPoint()
    {
        AddSpawnPoint(enemySpawnPoints, howManyEnemies);

        GameObject instance = Instantiate(enemySpawnPrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
        instance.transform.parent = transform;
        enemySpawnDictionary.Add(pos, instance);
    }
    public void AddSpawnPoint(List<Point> list, int limit)
    {
        if (list.Count < limit && !list.Contains(pos))
        {
            list.Add(pos);
        }

        else
        {
            Debug.Log("Warning, there aren't anymore spawn points left");
        }
    }

    public void ChangeTileToSpawn(GameObject newTileToSpawn)
    {
        tileToSpawn = newTileToSpawn;
    }
}
