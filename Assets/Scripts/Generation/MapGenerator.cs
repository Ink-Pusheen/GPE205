using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{

    public List<Tile> availableTiles = new List<Tile>();

    public float tileWidth;
    public float tileLength;

    public int mapColumns;
    public int mapRows;

    public Tile[,] grid;

    MapGeneratorLogic mapLogic = new MapGeneratorLogic();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TODO: Remove later, For testing
        GenerateMap();
    }

    public void GenerateMap()
    {
        //Create the grid array to hold the map
        grid = new Tile[mapColumns, mapRows];
        //mapLogic.newGrid();

        //Starting position
        Vector3 spawnPos = Vector3.zero;

        //Iterate through and generate all the map tiles
        for (int column = 0; column < mapColumns; column++)
        {
            spawnPos.x = 0;

            for (int row = 0; row < mapRows; row++)
            {
                Tile tempTile = Instantiate(GenerateTile()) as Tile;
                //Tile tempTile = Instantiate(mapLogic.GenerateTile()) as Tile;

                tempTile.transform.position = spawnPos;

                spawnPos.x += 25;

                if (row > 0)
                {
                    tempTile.doorSouth.SetActive(false);
                }

                if (row < mapRows - 1)
                {
                    tempTile.doorNorth.SetActive(false);
                }

                if (column > 0)
                {
                    tempTile.doorEast.SetActive(false);
                }

                if (column < mapColumns - 1)
                {
                    tempTile.doorWest.SetActive(false);
                }

                grid[column, row] = tempTile;
            }

            spawnPos.z += 25;
        }

        //Create a map tile
        //Put it in the right position
        //Open the correct doors
        //Save it to the grid
    }

    public Tile GenerateTile()
    {
        int chosenTile = Random.Range(0, availableTiles.Count);

        return availableTiles[chosenTile];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
