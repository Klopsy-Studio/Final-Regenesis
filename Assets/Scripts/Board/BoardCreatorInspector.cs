using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor
{
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
        if (GUILayout.Button("Clear"))
            current.Clear();
        if (GUILayout.Button("Grow"))
            current.Grow();
        if (GUILayout.Button("Shrink"))
            current.Shrink();
        if (GUILayout.Button("Add Obstacle"))
            current.AddObstacle();
        if (GUILayout.Button("Add Player Spawn"))
            current.AddPlayerSpawnPoint();
        if (GUILayout.Button("Add Enemy Spawn"))
            current.AddEnemySpawnPoint();
        if (GUILayout.Button("Clear Player Spawn"))
            current.ClearPlayerSpawnPoints();
        if (GUILayout.Button("Clear Enemy Spawn"))
            current.ClearEnemySpawnPoints();
        if (GUILayout.Button("Grow Area"))
            current.GrowArea();
        if (GUILayout.Button("Shrink Area"))
            current.ShrinkArea();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();
        
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
