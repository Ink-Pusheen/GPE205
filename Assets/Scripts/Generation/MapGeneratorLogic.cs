using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapGeneratorLogic
{
    [Header("SeedLogic")]
    public int seed = 235124;

    [Header("TileLogic")]
    public List<Tile> availableTiles = new List<Tile>();
    public List<Tile> generatedTiles = new List<Tile>();

    public float tileWidth;
    public float tileLength;

    public int mapSize;
    public int generatedTilesCount;

    public int mapColumns;
    public int mapRows;

    public Tile[,] grid;

    public void newGrid()
    {
        grid = new Tile[mapColumns, mapRows];
    }

    public Tile GenerateTile()
    {
        int chosenTile = UnityEngine.Random.Range(0, availableTiles.Count);

        return availableTiles[chosenTile];
    }
}
