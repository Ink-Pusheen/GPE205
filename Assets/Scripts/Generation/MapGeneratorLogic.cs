using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[Serializable]
public class MapGeneratorLogic
{
    [Header("SeedLogic")]
    public uint seed = 1;

    private Unity.Mathematics.Random rng;

    [Header("TileLogic")]

    public int maxOdds;
    public List<int> TileTypeChances = new List<int>(); //Odds of certain tiles over others

    [Header("Maze Generation")]

    public Tile startTile;
    public List<Tile> normalTiles = new List<Tile>(); //Lists for maze generation
    public List<Tile> hallwayTiles = new List<Tile>();
    public List<Tile> generatedMazeTiles = new List<Tile>();

    public int mapSize;
    public int generatedTilesCount;

    [Header("Grid Generation")]

    public List<Tile> gridTiles = new List<Tile>(); //List for grid generation
    public Tile[,] generatedGridTiles; //Array for grid to hold generated tiles

    public int mapColumns;
    public int mapRows;

    [Header("Spawn Lists/Objects")]

    public List<GameObject> playerSpawnChoices = new List<GameObject>();
    public List<GameObject> enemySpawnChoices = new List<GameObject>();

    public GameObject[] aiTankObjects;
    public GameObject[] aiControllers; //Roamer, Rusher, Sniper, Smoke Screen 

    public void InitializeRandomSeed(RandomType randomGenType, MapGenerator mapGen)
    {
        setupOdds();
        newGrid();

        if (randomGenType == RandomType.Seeded)
        {
            GameManager.instance.rng.InitState(seed);
        }
        else if (randomGenType == RandomType.Random)
        {
            //Do nothing as random class auto seeds
            GameManager.instance.rng.InitState((uint)System.DateTime.Now.Ticks);
        }
        else if (randomGenType == RandomType.MapOfTheDay)
        {
            //Map of the day
            GameManager.instance.rng.InitState((uint)DateToInt(System.DateTime.Now.Date));
        }
        else
        {
            Debug.Log("No seed type chosen");
            return;
        }

        //Start map generation
        mapGen.StartGeneratingMap();
    }

    private int DateToInt(DateTime date)
    {
        return date.Year + date.Month + date.Day;
    }

    public void setupOdds()
    {
        maxOdds = 0;

        for (int i = 0; i < TileTypeChances.Count; i++)
        {
            maxOdds += TileTypeChances[i];
        }
    }

    public void newGrid()
    {
        generatedGridTiles = new Tile[mapColumns, mapRows];
    }

    public void deleteMap(GenType mapGenType)
    {
        switch (mapGenType)
        {
            case GenType.Grid:

                for (int column = 0; column < mapColumns; column++)
                {
                    for (int row = 0; row < mapRows; row++)
                    {
                        UnityEngine.Object.Destroy(generatedGridTiles[column, row].gameObject);
                    }
                }

                break;

            case GenType.MazeLike:

                for (int i = 0; i < generatedMazeTiles.Count; i++)
                {
                    UnityEngine.Object.Destroy(generatedMazeTiles[i].gameObject);
                }

                generatedMazeTiles.Clear();

                break;
        }
    }

    public List<Tile> chosenTileType()
    {
        int odds = GameManager.instance.rng.NextInt(0, maxOdds + 1);

        for (int i = 0; i < TileTypeChances.Count; i++)
        {
            odds -= TileTypeChances[i];

            if (odds <= 0)
            {
                switch (i)
                {
                    case 0:
                        return normalTiles;
                        
                    case 1:
                        return hallwayTiles;
                }

                break;
            }
        }
        Debug.Log("Return null");
        return null;
    }

    public Tile GenerateTile(List<Tile> chosenTileList)
    {
        if (chosenTileList == null)
        {
            Debug.Log("List null error");
            ArgumentException argument = new ArgumentException("Null Tile list");
            throw argument;
        }

        int chosenTile = GameManager.instance.rng.NextInt(0, chosenTileList.Count);
        return chosenTileList[chosenTile];
    }

    public void HallwayChecks(MapGenerator mapGen)
    {
        for (int i = generatedMazeTiles.Count - 1; i >= 0; i--)
        {
            generatedMazeTiles[i].HallwayCheck();
        }

        mapGen.SpawnEnemies();
    }
}
