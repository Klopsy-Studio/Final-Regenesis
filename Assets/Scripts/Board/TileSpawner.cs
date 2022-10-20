using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TileType
{
    Placeholder, Desert, NonPlayable
};
public enum DesertType
{
    Desert1, Desert2, Desert3, Quicksand
}

[System.Serializable]
public class TileSpawner
{
    //public typeOfTiles TypeToSpawn;

    //Desert
    public GameObject[] desertTiles;


    //Placeholder
    public GameObject[] placeholderTiles;


    //Non Playable
    public GameObject[] nonPlayableTiles;
}
