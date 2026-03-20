using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Unity.Mathematics.Random rng;

    public GameObject tankPrefab;
    public GameObject playerControllerPrefab;

    public List<Controller> players;
    public List<Pawn> tanks;

    public bool generatePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Create our up to date list objects (not just memory locations, but actual lists)
        players = new List<Controller>();
        tanks = new List<Pawn>();
    }

    private void Start()
    {
        //For testing purposes
        if (!generatePlayer) return;
        GameObject zero = new GameObject("Zero");
        zero.transform.position = Vector3.zero;

        StartGame(zero);

        Destroy(zero); //Temp
    }

    public void StartGame(GameObject spawnPoint)
    {
        //Do premature checks for setup

        //Spawn a tank pawn (and store it in tanks)
        Pawn playerTank = SpawnTank(tankPrefab, spawnPoint);

        //Spawn a player controller (and store it in players)
        Controller playerController = SpawnPlayer(playerControllerPrefab);

        //Have the player posses the pawn
        playerController.Possess(playerTank);
        playerController.SetupControls();
    }

    public void EnemyInitialization(GameObject tankPrefab, GameObject enemyAI, GameObject spawnPosition)
    {
        //Spawn the enemy tank and possess it
        Pawn enemyPawn = SpawnTank(tankPrefab, spawnPosition);
        ControllerAI enemyController = SpawnEnemy(enemyAI);

        //Apply spawn information
        EnemySpawn enemSpawnInfo = spawnPosition.GetComponent<EnemySpawn>();
        
        if (enemSpawnInfo == null) throw new ArgumentException("Tile spawn contains no enemy spawner script");
        else enemyController.roamPoints = enemSpawnInfo.roamPoints;

        enemyController.Possess(enemyPawn);
        enemyController.SetupControls();
    }

    public Pawn SpawnTank(GameObject tankPrefab, GameObject spawnPosition)
    {
        GameObject pawnSpawn = Instantiate(tankPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
        Pawn tempTankPawn = pawnSpawn.GetComponent<Pawn>();

        return tempTankPawn;
    }

    public Controller SpawnPlayer(GameObject playerPrefab)
    {
        GameObject playerSpawn = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Controller playerController = playerSpawn.GetComponent<Controller>();

        return playerController;
    }

    public ControllerAI SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemySpawn = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        ControllerAI enemyController = enemySpawn.GetComponent<ControllerAI>();

        return enemyController;
    }
}
