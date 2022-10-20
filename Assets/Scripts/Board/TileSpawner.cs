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

public enum PropType
{
    City, Desert, Park
}
public enum ThingToSpawn
{
    Tiles, Props
}
[System.Serializable]
public class TileSpawner
{
    //public typeOfTiles TypeToSpawn;
    [Header("Tiles")]
    //Desert
    public GameObject[] desertTiles;


    //Placeholder
    public GameObject[] placeholderTiles;


    //Non Playable
    public GameObject[] nonPlayableTiles;

    [Space]
    [Header("Props")]
    public GameObject[] cityProps;

    public GameObject[] desertProps;

    public GameObject[] parkProps;
}
