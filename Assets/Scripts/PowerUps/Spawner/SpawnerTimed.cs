using UnityEngine;

public class SpawnerTimed : MonoBehaviour
{

    public GameObject pickup;

    public bool isSpawnOnStart;
    public float countdownTimer;
    public float timeBetweenSpawns;

    private GameObject spawnedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set first spawn time
        if (isSpawnOnStart)
        {
            countdownTimer = 0;
        }
        else
        {
            countdownTimer = timeBetweenSpawns;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject != null) return;

        //Subtract how much time has passed
        countdownTimer -= Time.deltaTime;

        //Check if timer has hit 0
        if (countdownTimer <= 0)
        {
            //Spawn object
            spawnedObject = Instantiate(pickup, transform.position, transform.rotation) as GameObject;

            //Reset timer
            countdownTimer = timeBetweenSpawns;
        }
    }
}
