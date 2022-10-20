using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor
{
    public bool growOrShrink = true;
    public void AddTileSpawnButton(string buttonName, GameObject newTile)
    {
        if (GUILayout.Button(buttonName))
        {
            current.ChangeTileToSpawn(newTile);
        }
    }
    public BoardCreator current
    {
        get
        {
            return (BoardCreator)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10f);

        switch (current.TypeOfTile)
        {
            case TileType.Placeholder:
                AddTileSpawnButton("Choose Placeholder 1", current.spawner.placeholderTiles[0]);
                AddTileSpawnButton("Choose Placeholder 2", current.spawner.placeholderTiles[1]);
                AddTileSpawnButton("Choose Placeholder 3", current.spawner.placeholderTiles[2]);
                break;
            case TileType.Desert:
                AddTileSpawnButton("Choose Desert Tile 1", current.spawner.desertTiles[0]);
                AddTileSpawnButton("Choose Desert Tile 2", current.spawner.desertTiles[1]);
                AddTileSpawnButton("Choose Desert Tile 3", current.spawner.desertTiles[2]);
                AddTileSpawnButton("Choose Quicksand Tile", current.spawner.desertTiles[3]);


                break;
            case TileType.NonPlayable:
                AddTileSpawnButton("Choose Non Playable 1", current.spawner.nonPlayableTiles[0]);
                AddTileSpawnButton("Choose Non Playable 2", current.spawner.nonPlayableTiles[1]);
                AddTileSpawnButton("Choose Non Playable 3", current.spawner.nonPlayableTiles[2]);
                break;
            default:
                break;
        }
        GUILayout.Space(10f);
        if (GUILayout.Button("Clear"))
            current.Clear();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();
        GUILayout.Space(10f);
        if (GUILayout.Button("Grow"))
            current.Grow();
        if (GUILayout.Button("Shrink"))
            current.Shrink();
        if (GUILayout.Button("Add Obstacle"))
            current.AddObstacle();
        GUILayout.Space(10f);
        if (GUILayout.Button("Add Player Spawn"))
            current.AddPlayerSpawnPoint();
        if (GUILayout.Button("Add Enemy Spawn"))
            current.AddEnemySpawnPoint();
        GUILayout.Space(10f);

        if (GUILayout.Button("Clear Player Spawn"))
            current.ClearPlayerSpawnPoints();
        if (GUILayout.Button("Clear Enemy Spawn"))
            current.ClearEnemySpawnPoints();
        
        
        if (GUI.changed)
            current.UpdateMarker();
    }


    private void OnSceneGUI()
    {
        Event e = Event.current;

        switch (e.type)
        {
            case EventType.KeyDown:
            {

                    if (Event.current.keyCode == KeyCode.W)
                    {
                        current.MoveTileSelectionUpwards();
                        current.UpdateMarker();
                        e.Use();
                    }
                    if (Event.current.keyCode == KeyCode.A)
                    {
                        current.MoveTileSelectionLeft();
                        current.UpdateMarker();
                        e.Use();
                    }
                    if (Event.current.keyCode == KeyCode.S)
                    {
                        current.MoveTileSelectionDownwards();
                        current.UpdateMarker();
                        e.Use();
                    }
                    if (Event.current.keyCode == KeyCode.D)
                    {
                        current.MoveTileSelectionRight();
                        current.UpdateMarker();
                        e.Use();
                    }

                    if(Event.current.keyCode == KeyCode.Space)
                    {
                        if (growOrShrink)
                        {
                            growOrShrink = false;
                        }
                        else
                        {
                            growOrShrink = true;
                        }
                        e.Use();
                    }


                    break;
            }


            case EventType.MouseDown:
            {
                    if (growOrShrink)
                    {
                        current.Grow();
                        current.Grow();
                        current.Grow();
                        current.Grow();
                        current.UpdateMarker();
                        e.Use();
                        break;
                    }
                    else
                    {
                        current.Shrink();
                        e.Use();
                        break;
                    }
                        
            }

            //case EventType.MouseMove:
            //{
            //       current.MoveTileSelection(new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y));
            //       e.Use();
            //       break;
            //}


        }
    }
}
