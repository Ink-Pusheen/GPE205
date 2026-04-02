using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[Serializable]
public class GameState
{
    public string name;
    public GameObject objectRef;
}

public class GameManager : MonoBehaviour
{
    [Header("Instance and Randomness")]
    public static GameManager instance;

    public Unity.Mathematics.Random rng;

    [Header("Tanks")]
    public GameObject tankPrefab;
    public GameObject playerControllerPrefab;

    public List<Controller> players;
    public List<Pawn> tanks;

    public bool generatePlayer;

    [Header("GameState")]

    public GameState[] states;

    int curState = 0;


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
            return;
        }

        DisableAllStates();
        ChangeState("TitleScreen");

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

    #region Tank Spawning

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

    public void RespawnPlayer(Controller controllerToPossess, GameObject spawnPosition)
    {
        Pawn playerTank = SpawnTank(tankPrefab, spawnPosition);

        controllerToPossess.Possess(playerTank);
        controllerToPossess.updateHealth();
    }

    public ControllerAI SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemySpawn = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        ControllerAI enemyController = enemySpawn.GetComponent<ControllerAI>();

        return enemyController;
    }

    #endregion Tank Spawning

    #region Game State Swapping

    private void DisableAllStates()
    {
        //Loop through the states to disable them all
        foreach (GameState state in states)
        {
            state.objectRef.SetActive(false);
        }
    }

    public void HideSpecificState(string stateToHide)
    {
        //For loop finding the new state to disable
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].name == stateToHide)
            {
                states[i].objectRef.SetActive(false);
                break;
            }
        }
    }

    public void RevealSpecificState(string stateToReveal)
    {
        //For loop finding the new state to enable
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].name == stateToReveal)
            {
                states[i].objectRef.SetActive(true);
                break;
            }
        }
    }

    public void ChangeState(string newState)
    {
        //Disable other states
        DisableAllStates();

        //For loop finding the new state to enable
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].name == newState)
            {
                states[i].objectRef.SetActive(true);
                break;
            }
        }

        if (newState == "Gameplay") RevealSpecificState("GameplaySetup");
    }

    #endregion

    public void CloseApplication()
    {
        Debug.Log("Close Application");
        Application.Quit();
    }
}
