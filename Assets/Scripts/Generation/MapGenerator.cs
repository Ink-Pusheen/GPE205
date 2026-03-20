using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;


public enum RandomType { Random, Seeded, MapOfTheDay}
public enum GenType { Grid, MazeLike }
public class MapGenerator : MonoBehaviour
{
    [Header("Random Data")]
    public RandomType randomType;
    public GenType genType;

    [Header("Map Generator Logic")]

    public MapGeneratorLogic mapLogic = new MapGeneratorLogic();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //TODO: Remove later, For testing
        mapLogic.InitializeRandomSeed(randomType, this);
        //InitializeRandomSeed();
    }

    public void Start()
    {
        StartGeneratingMap();
    }

    public void InitializeRandomSeed()
    {
        mapLogic.InitializeRandomSeed(randomType, this);
    }
    //zach waz here
    public void StartGeneratingMap()
    {
        if (genType == GenType.MazeLike)
        {
            mapLogic.generatedTilesCount = 0;

            Tile startTile = Instantiate(mapLogic.startTile, Vector3.zero, Quaternion.identity) as Tile;

            if (startTile == null)
            {
                Debug.Log("Error");
                return;
            }

            startTile.addTileToList(this, true);
        }
        else
        {
            //Starting position
            Vector3 spawnPos = Vector3.zero;

            //Iterate through and generate all the map tiles
            for (int column = 0; column < mapLogic.mapColumns; column++)
            {
                spawnPos.x = 0;

                for (int row = 0; row < mapLogic.mapRows; row++)
                {
                    Tile gridTile = Instantiate(GenerateTile(false)) as Tile; //Create a map tile
                    //Tile tempTile = Instantiate(mapLogic.GenerateTile()) as Tile;

                    gridTile.transform.position = spawnPos; //Put it in the right position

                    spawnPos.x += gridTile.tileLogic.tileSize;

                    //Name the tile and add it
                    gridTile.name = $"Tile [{column}, {row}]";

                    mapLogic.generatedGridTiles[column, row] = gridTile; //Save it to the grid

                    //Grab the player spawns
                    for (int playerSpawn = 0; playerSpawn < gridTile.tileLogic.playerSpawnPoints.Count; playerSpawn++)
                    {
                        mapLogic.playerSpawnChoices.Add(gridTile.tileLogic.playerSpawnPoints[playerSpawn]);
                    }

                    //Grab enemy spawns
                    for (int enemySpawn = 0; enemySpawn < gridTile.tileLogic.enemySpawnPoints.Count; enemySpawn++)
                    {
                        mapLogic.enemySpawnChoices.Add(gridTile.tileLogic.enemySpawnPoints[enemySpawn]);
                    }
                }

                spawnPos.z += 27;
            }

            SpawnEnemies();
        }
    }

    #region Maze Generation Logic

    //Maze Like
    private void GenerateMazeTile()
    {
        if (mapLogic.generatedTilesCount >= mapLogic.mapSize)
        {
            //Stops generating and checks hallways for availability of final rooms of the generation
            mapLogic.HallwayChecks(this);
        }
        else
        {
            if (mapLogic.generatedMazeTiles.Count == 0)
            {
                print("Not enough tiles");
                StartCoroutine(Delay());
                return;
            }

            
            int randomTile = GameManager.instance.rng.NextInt(0, mapLogic.generatedMazeTiles.Count);

            if (mapLogic.generatedMazeTiles[randomTile] == null)
            {
                Debug.Log("Null reference;");
                return;
            }

            Tile tileToSpawn = GenerateTile(false);
            mapLogic.generatedMazeTiles[randomTile].spawnTile(tileToSpawn, true);

            mapLogic.generatedTilesCount++;
        }
    }

    public void FailedTileSpawn()
    {
        Debug.Log("Failed Spawn");
        //GameManager.instance.rng.state = mapLogic.saveStates[0];
        //Debug.Log("Seed Reset");
        mapLogic.generatedTilesCount--;
        StartCoroutine(Delay());
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.075f);
        //yield return new WaitForSeconds(1.5f); //Testing value

        GenerateMazeTile();
    }

    public void AddGeneratedTile(Tile tileToAdd, bool moreGeneration)
    {
        mapLogic.generatedMazeTiles.Add(tileToAdd);

        //Grab player spawn points
        for (int spawns = 0; spawns < tileToAdd.tileLogic.playerSpawnPoints.Count; spawns++)
        {
            mapLogic.playerSpawnChoices.Add(tileToAdd.tileLogic.playerSpawnPoints[spawns]);
        }

        //Grab enemy spawns
        for (int enemySpawn = 0; enemySpawn < tileToAdd.tileLogic.enemySpawnPoints.Count; enemySpawn++)
        {
            mapLogic.enemySpawnChoices.Add(tileToAdd.tileLogic.enemySpawnPoints[enemySpawn]);
        }

        if (moreGeneration) StartCoroutine(Delay());
    }

    #endregion



    public void deleteMap()
    {
        mapLogic.deleteMap(genType);
    }

    public Tile GenerateTile(bool inFinalization)
    {
        switch (genType)
        {
            case GenType.MazeLike:

                if (!inFinalization)
                {
                    List<Tile> tiles = mapLogic.chosenTileType();

                    Tile generatedTile = mapLogic.GenerateTile(tiles);

                    return generatedTile;
                }

                else
                {
                    Tile chosenTile = mapLogic.GenerateTile(mapLogic.normalTiles);

                    return chosenTile;
                }

            case GenType.Grid:

                Tile randomTile = mapLogic.GenerateTile(mapLogic.gridTiles);

                return randomTile;
        }

        ArgumentException arguement = new ArgumentException("Couldn't Generate Tile");
        throw arguement;
    }

    public void SpawnEnemies()
    {
        for (int enemySpawns = 0; enemySpawns < mapLogic.enemySpawnChoices.Count; enemySpawns++)
        {
            int chosenAI = GameManager.instance.rng.NextInt(0, mapLogic.aiControllers.Length);
            GameManager.instance.EnemyInitialization(mapLogic.aiTankObjects[chosenAI], mapLogic.aiControllers[chosenAI], mapLogic.enemySpawnChoices[enemySpawns]);
        }

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        int chosenSpawn = GameManager.instance.rng.NextInt(0, mapLogic.playerSpawnChoices.Count);
        GameObject playerSpawn = mapLogic.playerSpawnChoices[chosenSpawn];

        GameManager.instance.StartGame(playerSpawn);
    }
}
