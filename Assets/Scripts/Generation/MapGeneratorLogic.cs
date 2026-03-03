using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorLogic
{
    public List<Tile> availableTiles = new List<Tile>();

    public float tileWidth;
    public float tileLength;

    public int mapColumns;
    public int mapRows;

    public Tile[,] grid;

    public void newGrid()
    {
        grid = new Tile[mapColumns, mapRows];
    }

    public Tile GenerateTile()
    {
        int chosenTile = Random.Range(0, availableTiles.Count);

        return availableTiles[chosenTile];
    }
}
