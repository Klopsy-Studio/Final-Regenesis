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
    public GameObject desert1Tile;
    public GameObject desert2Tile;
    public GameObject desert3Tile;
    public GameObject desertQuicksandTile;

    //Placeholder
    public GameObject placeholder1Tile;
    public GameObject placeholder2Tile;
    public GameObject placeholder3Tile;

    //Non Playable
    public GameObject nonPlayable1Tile;
    public GameObject nonPlayable2Tile;
    public GameObject nonPlayable3Tile;
}
