using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor
{
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
                AddTileSpawnButton("Choose Placeholder 1", current.spawner.placeholder1Tile);
                AddTileSpawnButton("Choose Placeholder 2", current.spawner.placeholder2Tile);
                AddTileSpawnButton("Choose Placeholder 3", current.spawner.placeholder3Tile);
                break;
            case TileType.Desert:
                AddTileSpawnButton("Choose Desert Tile 1", current.spawner.desert1Tile);
                AddTileSpawnButton("Choose Desert Tile 2", current.spawner.desert2Tile);
                AddTileSpawnButton("Choose Desert Tile 3", current.spawner.desert3Tile);
                AddTileSpawnButton("Choose Quicksand Tile", current.spawner.desertQuicksandTile);


                break;
            case TileType.NonPlayable:
                AddTileSpawnButton("Choose Non Playable 1", current.spawner.nonPlayable1Tile);
                AddTileSpawnButton("Choose Non Playable 2", current.spawner.nonPlayable2Tile);
                AddTileSpawnButton("Choose Non Playable 3", current.spawner.nonPlayable3Tile);
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
                        current.Grow();
                        current.Grow();
                        current.Grow();
                        current.Grow();
                        current.UpdateMarker();
                        e.Use();
                    }

                    break;
            }
        }
    }
}
