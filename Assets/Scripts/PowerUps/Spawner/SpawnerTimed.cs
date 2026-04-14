using UnityEngine;

public class SpawnerTimed : MonoBehaviour
{
    [SerializeField] GameObject[] powerupPool;
    [SerializeField] float[] spawnTimer;

    public GameObject pickup;

    public bool isSpawnOnStart;
    public float countdownTimer;
    public float timeBetweenSpawns;

    private GameObject spawnedObject;

    private void Awake()
    {
        //Use instance random to generate a random powerup
        int powerupToSpawn = GameManager.instance.rng.NextInt(powerupPool.Length);
        pickup = powerupPool[powerupToSpawn];
        timeBetweenSpawns = spawnTimer[powerupToSpawn];
    }

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
        if (pickup == null) return;

        if (spawnedObject != null) return;

        //Subtract how much time has passed
        countdownTimer -= Time.deltaTime;

        //Check if timer has hit 0
        if (countdownTimer <= 0)
        {
            //Spawn object
            spawnedObject = Instantiate(pickup, transform.position + new Vector3(0, 0.6f, 0), transform.rotation) as GameObject;

            //Reset timer
            countdownTimer = timeBetweenSpawns;
        }
    }
}
