using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private MapGenerator mapGenInstance;

    public float tileSize;
    public Vector3 overlapSize;
    public List<GameObject> spawnLocations = new List<GameObject>();

    public int ID;

    [SerializeField] LayerMask tileLayer;

    //North - 0, East - 1, South - 2, West - 3 [Increments will change based on what is removed]
    public List<GameObject> doors = new List<GameObject>(); //doorNorth, doorEast, doorSouth, doorWest;

    private void Awake()
    {
        ID = Random.Range(0, 5000);

        for (int i = 0; i < spawnLocations.Count; i++)
        {
            spawnLocations[i].name = $"{spawnLocations[i].name}: {ID}";
        }
    }

    public void spawnTile(Tile tileToSpawn)
    {
        int chosenSpawnSpot = Random.Range(0, spawnLocations.Count);

        Vector3 startPos = spawnLocations[chosenSpawnSpot].transform.position;
        Vector3 forward = transform.TransformDirection(spawnLocations[chosenSpawnSpot].transform.forward);
        Vector3 spawnTransform = startPos + (forward * (tileToSpawn.tileSize * 0.5f));
        //Vector3 spawnTransform = transform.TransformDirection(spawnLocations[chosenSpawnSpot].transform.forward) * tileToSpawn.tileSize;
        //Debug.Log(spawnLocations[chosenSpawnSpot].transform.position);



        Vector3 lookRotation = spawnTransform - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookRotation);
        Debug.Log(rotation);
        bool willOverlap = CollidingWithOtherTile(tileToSpawn, spawnTransform);

        if (willOverlap)
        {
            Debug.Log($"Overlap Detected on {name} at position {spawnTransform} with tile {tileToSpawn}");
            
            if (mapGenInstance != null) mapGenInstance.FailedTileSpawn();
            else Debug.Log("Mapgen Null");
                return;
        }

        Tile spawnedTile = Instantiate(tileToSpawn, spawnTransform, rotation);

        Debug.Log($"{this.name} spawned a tile on {spawnLocations[chosenSpawnSpot].name}");

        DoorSpawnCheck door = spawnLocations[chosenSpawnSpot].GetComponent<DoorSpawnCheck>();
        if (door != null) door.activatable = false;
        else Debug.Log("No Door component");


        doors[chosenSpawnSpot].transform.Translate(0, -1, 0);

        spawnLocations.RemoveAt(chosenSpawnSpot);
        doors.RemoveAt(chosenSpawnSpot);
        //return;
        //Debug.Log("Keep Tile");
        spawnedTile.addTileToList(mapGenInstance);


    }

    public void DoorChecks()
    {
        if (spawnLocations.Count == 0) return;

        for (int i = spawnLocations.Count - 1; i > 0; i--)
        {
            DoorSpawnCheck c = spawnLocations[i].GetComponent<DoorSpawnCheck>();

            if (c == null)
            {
                //Debug.Log("Door check is null");
                continue;
            }

            c.checkForDoors();
        }
    }

    public void addTileToList(MapGenerator mapGen)
    {
        gameObject.name = $"Tile: {ID}";
        mapGenInstance = mapGen;
        mapGen.AddGeneratedTile(this);
    }

    bool CollidingWithOtherTile(Tile tileToCheck, Vector3 checkSpot)
    {
        int layermask = LayerMask.GetMask("Tile");
        Collider[] hits = Physics.OverlapBox(checkSpot, tileToCheck.overlapSize * 0.5f, Quaternion.identity, tileLayer);

        if (hits.Length > 0)
        {
            Debug.Log(hits.Length);

            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log($"{hits[i].name}: parented to {hits[i].transform.parent.name}");
            }

            foreach (Collider col in hits)
            {
                //Debug.Log($"Hit {col.gameObject.name}: {col.gameObject.transform.parent.name} at {col.transform.position}");
                if (col.transform.IsChildOf(transform) || col.gameObject.layer != LayerMask.NameToLayer("Tile")) continue;

                return true;
            }
        }
        else
        {
            Debug.Log("No hits");
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, overlapSize);
    }
}
