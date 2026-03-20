using System;
using System.Collections.Generic;
using UnityEngine;


public enum TileType { Basic, Hallway }
[Serializable]
public class TileLogic
{
    public TileType tileType;
    public MapGenerator mapGenInstance { get; set; }

    [SerializeField] GameObject spawnSpot; //Primary generation tool

    public float tileSize;
    public Vector3 overlapSize;

    //Ensure that all doors and spawn locations are on the same respective array value
    public List<GameObject> genSpawnLocations = new List<GameObject>();
    public List<GameObject> doors = new List<GameObject>(); //doorNorth, doorEast, doorSouth, doorWest;

    //Lists for Enemy, Player, and Powerup Spawns
    public List<GameObject> enemySpawnPoints = new List<GameObject>();
    public List<GameObject> playerSpawnPoints = new List<GameObject>();


    public int ID;

    public void CreateID()
    {
        //ID = UnityEngine.Random.Range(0, 5001);

        for (int i = 0; i < genSpawnLocations.Count; i++)
        {
            genSpawnLocations[i].name = $"{genSpawnLocations[i].name}: {ID}";
        }
    }

    public bool tileOverlapping(Tile tileToSpawn, Vector3 checkSpot, GameObject parentTile)
    {
        int layerIndex = 7;
        int mask = 1 << layerIndex;


        Collider[] hits = Physics.OverlapBox(checkSpot, tileToSpawn.tileLogic.overlapSize * 0.5f, Quaternion.identity, mask);

        if (hits.Length > 0)
        {
            foreach (Collider col in hits)
            {
                //Debug.Log($"Hit {col.gameObject.name}: {col.gameObject.transform.parent.name} at {col.transform.position}");
                if (col.transform.IsChildOf(parentTile.transform) || col.gameObject.layer != LayerMask.NameToLayer("Tile")) continue;

                return true;
            }
        }

        return false;
    }

    public bool checkSpawnAvailability()
    {
        return genSpawnLocations.Count > 0? true : false;
    }

    public int chooseSpawnSpot()
    {
        return GameManager.instance.rng.NextInt(0, genSpawnLocations.Count);
    }

    public void returnSpawnSpot(int chosenSpawn, Tile tileToSpawn, GameObject parentTile, out Vector3 outSpawn, out Quaternion outRotation)
    {
        spawnSpot.transform.position = genSpawnLocations[chosenSpawn].transform.position;

        Quaternion expectedRotation = genSpawnLocations[chosenSpawn].transform.localRotation;
        spawnSpot.transform.rotation = expectedRotation;
        Vector3 forward = parentTile.transform.TransformDirection(spawnSpot.transform.forward);
        Vector3 spawnTransform = spawnSpot.transform.position + (forward * (tileToSpawn.tileLogic.tileSize * 0.5f));

        Vector3 lookRotation = parentTile.transform.position - spawnTransform;
        Quaternion rotation = Quaternion.LookRotation(lookRotation);

        outSpawn = spawnTransform;
        outRotation = rotation;
    }

    public void DeactivateDoorAndSpawner(int chosenDoor)
    {
        DoorSpawnCheck door = genSpawnLocations[chosenDoor].GetComponent<DoorSpawnCheck>();
        if (door != null) door.activatable = false;
        else Debug.Log("No Door component");

        doors[chosenDoor].transform.Translate(0, -1, 0);

        genSpawnLocations.RemoveAt(chosenDoor);
        doors.RemoveAt(chosenDoor);
    }

    public void DoorChecks()
    {
        if (genSpawnLocations.Count == 0) return;

        for (int i = genSpawnLocations.Count - 1; i > 0; i--)
        {
            DoorSpawnCheck c = genSpawnLocations[i].GetComponent<DoorSpawnCheck>();

            if (c == null) continue;

            c.checkForDoors();
        }
    }

    public void HallwayCheck(Tile parentTile)
    {
        if (tileType != TileType.Hallway) return;

        if (genSpawnLocations.Count > 0)
        {
            for (int i = genSpawnLocations.Count - 1; i >= 0; i--)
            {
                Tile tileToSpawn = mapGenInstance.GenerateTile(true);

                parentTile.spawnTile(tileToSpawn, false);
            }
        }
    }

    public void addTileToList(MapGenerator mapGen, bool moreGeneration, Tile parentTile)
    {
        mapGenInstance = mapGen;
        mapGen.AddGeneratedTile(parentTile, moreGeneration);
    }
}
