using UnityEngine;
using System.Collections.Generic;
using System;


public enum RandomType { Random, Seeded, MapOfTheDay}
public class MapGenerator : MonoBehaviour
{
    [Header("Random Data")]
    public RandomType randomType;

    public int seed = 235124;

    [Header("TileData")]
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
        InitializeRandomSeed();
        GenerateMap();
    }

    public void InitializeRandomSeed()
    {
        if (randomType == RandomType.Seeded)
        {
            UnityEngine.Random.InitState(seed);
        }
        else if (randomType == RandomType.Random)
        {
            //Do nothing as random class auto seeds
            UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        }
        else if(randomType == RandomType.MapOfTheDay)
        {
            //Map of the day
            UnityEngine.Random.InitState(DateToInt(System.DateTime.Now.Date));
        }
        else
        {
            Debug.Log("No seed type chosen");
        }
    }

    public int DateToInt(DateTime date)
    {
        return date.Year + date.Month + date.Day;
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
                
                //If bottom row, disable the north door
                if (row == 0)
                {
                    tempTile.doorNorth.SetActive(false);
                }

                //If top row, disable the south door.
                else if (row == mapRows - 1)
                {
                    tempTile.doorSouth.SetActive(false);
                }

                //Otherwise in the middle disable both
                else
                {
                    tempTile.doorNorth.SetActive(false);
                    tempTile.doorSouth.SetActive(false);
                }

                if (column == 0)
                {
                    tempTile.doorWest.SetActive(false);
                }

                else if (column == mapColumns - 1)
                {
                    tempTile.doorEast.SetActive(false);
                }

                else
                {
                    tempTile.doorEast.SetActive(false);
                    tempTile.doorWest.SetActive(false);
                }

                //Name the tile and add it
                tempTile.name = $"Tile [{column}, {row}]";

                grid[column, row] = tempTile;
            }

            spawnPos.z += 25;
        }

        //Create a map tile
        //Put it in the right position
        //Open the correct doors
        //Save it to the grid
        Invoke("deleteMap", 6);
    }

    void deleteMap()
    {
        Debug.Log("Delete");
        for (int column = 0; column < mapColumns; column++)
        {
            for (int row = 0; row < mapRows; row++)
            {
                Destroy(grid[column, row].gameObject);
            }
        }
    }

    public Tile GenerateTile()
    {
        int chosenTile = UnityEngine.Random.Range(0, availableTiles.Count);

        return availableTiles[chosenTile];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
