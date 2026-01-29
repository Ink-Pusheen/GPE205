using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject playerPrefab;
    public GameObject playerPawnPrefab;

    public List<Controller> players;
    public List<Pawn> tanks;

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
        StartGame();
    }

    public void StartGame()
    {
        //Do premature checks for setup

        //Spawn a tank pawn (and store it in tanks)
        Pawn tempTank = SpawnTank(playerPawnPrefab);

        //Spawn a player controller (and store it in players)
        Controller tempPlayer = SpawnPlayer(playerPrefab);

        //Have the player posses the pawn
        tempPlayer.Possess(tempTank);
        tempPlayer.SetupControls();
    }

    public Pawn SpawnTank(GameObject prefab)
    {
        GameObject pawnSpawn = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        Pawn tempTankPawn = pawnSpawn.GetComponent<Pawn>();
        tanks.Add(tempTankPawn);

        return tempTankPawn;
    }

    public Controller SpawnPlayer(GameObject prefab)
    {
        GameObject playerSpawn = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        Controller tempPlayerController = playerSpawn.GetComponent<Controller>();
        players.Add(tempPlayerController);

        return tempPlayerController;
    }
}
