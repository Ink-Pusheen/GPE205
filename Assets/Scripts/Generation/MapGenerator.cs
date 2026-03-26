using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public enum RandomType { Random, Seeded, MapOfTheDay}
public enum GenType { Grid, MazeLike }
public class MapGenerator : MonoBehaviour
{
    [Header("Random Data")]
    public RandomType randomType;
    public GenType genType;

    [Header("Map Generator Logic")]

    public MapGeneratorLogic mapLogic = new MapGeneratorLogic();

    [SerializeField] TMP_Dropdown genTypeDropdown;
    [SerializeField] TMP_InputField seedInput;
    [SerializeField] Image mapOfTheDayImage;
    [SerializeField] GameObject warningPopup;
    [SerializeField] TMP_Text warningText;

    public void InitializeRandomSeed()
    {
        //Run the map randomization and start generating the map
        mapLogic.InitializeRandomSeed(randomType, this);
    }
    //zach waz here

    #region Global Generation

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

    public void deleteMap()
    {
        mapLogic.deleteMap(genType);
    }

    #endregion

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

    #region Tank Spawning

    public void SpawnEnemies()
    {
        for (int enemySpawns = 0; enemySpawns < mapLogic.enemySpawnChoices.Count; enemySpawns++)
        {
            int chosenAI = GameManager.instance.rng.NextInt(0, mapLogic.aiControllers.Length);

            GameObject aiTankObject = mapLogic.aiTankObjects[chosenAI];
            GameObject aiController = mapLogic.aiControllers[chosenAI];
            GameObject aiSpawnPoint = mapLogic.enemySpawnChoices[enemySpawns];

            GameManager.instance.EnemyInitialization(aiTankObject, aiController, aiSpawnPoint);
        }

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        int chosenSpawn = GameManager.instance.rng.NextInt(0, mapLogic.playerSpawnChoices.Count);
        GameObject playerSpawn = mapLogic.playerSpawnChoices[chosenSpawn];

        GameManager.instance.StartGame(playerSpawn);
    }

    #endregion Tank Spawning

    #region Generation Setup and Execution

    public void SwitchGenType()
    {
        switch (genTypeDropdown.value)
        {
            //Case Grid
            case 0:
                genType = GenType.Grid;
                break;

            //Case Maze
            case 1:
                genType = GenType.MazeLike;
                break;
        }

    }

    public void ToggleMapOfTheDay()
    {
        if (randomType == RandomType.MapOfTheDay)
        {
            //Switch off and inspect current seed input state
            InspectSeedInput();

            seedInput.interactable = true;
            mapOfTheDayImage.color = Color.red;
        }
        else
        {
            //Switch on and disable interaction with the seed input
            randomType = RandomType.MapOfTheDay;

            seedInput.interactable = false;
            mapOfTheDayImage.color = Color.green;
        }
    }

    public void InspectSeedInput()
    {
        if (seedInput.text.Length == 0)
        {
            seedInput.textComponent.color = Color.black;

            randomType = RandomType.Random;
            return;
        }

        if (int.TryParse(seedInput.text, out int result))
        {
            seedInput.textComponent.color = Color.black;

            int zero = 0;

            for (int i = 0; i < seedInput.text.Length; i++)
            {
                int value = int.Parse(seedInput.text[i].ToString());

                zero += value;
            }

            if (zero == 0) randomType = RandomType.Random;

            else randomType = RandomType.Seeded;
        }
        else
        {
            Debug.Log("Invalid Character, Default Random");
            seedInput.textComponent.color = Color.red;
            randomType = RandomType.Random;
        }
    }

    //Ensure to put a warning on the start generating function if the value is 0 or a character is detected
    public void SetupGame()
    {
        //Initialize Random Default if there are no characters
        if (seedInput.text.Length == 0)
        {
            InitializeRandomSeed();
            GameManager.instance.HideSpecificState("GameplaySetup");
            return;
        }

        if (int.TryParse(seedInput.text, out int result))
        {
            seedInput.textComponent.color = Color.black;

            int zero = 0;

            for (int i = 0; i < seedInput.text.Length; i++)
            {
                int value = int.Parse(seedInput.text[i].ToString());

                zero += value;
            }

            if (zero == 0)
            {
                //Insert warning of invalid generation (Default Random)
                warningPopup.SetActive(true);

                warningText.text = "Seed cannot equate to 0, default generation will be Random. \n Do you wish to proceed?";
            }

            else
            {
                InitializeRandomSeed();
                GameManager.instance.HideSpecificState("GameplaySetup");
            }
        }
        else
        {
            //Insert warning of invalid generation (Default Random)
            warningPopup.SetActive(true);

            warningText.text = "Invalid non numerical characters detected in seed, default generation will be Random. \n Do you wish to proceed?";
        }
    }

    #endregion Generation Setup and Execution
}
