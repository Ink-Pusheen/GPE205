using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public TileLogic tileLogic = new TileLogic();

    private void Awake()
    {
        tileLogic.CreateID();
    }

    public void spawnTile(Tile tileToSpawn, bool generateMore)
    {
        bool hasAvailableSpawn = tileLogic.checkSpawnAvailability();

        if (!hasAvailableSpawn)
        {
            //Failed spawn

            if (!generateMore) return;

            if (tileLogic.mapGenInstance != null) tileLogic.mapGenInstance.FailedTileSpawn();
            else Debug.Log("Mapgen Null");
            return;
        }

        int chosenSpawn = tileLogic.chooseSpawnSpot();
        Debug.Log(chosenSpawn);
        Vector3 tileSpawn;
        Quaternion tileRotation;

        tileLogic.returnSpawnSpot(chosenSpawn, tileToSpawn, this.gameObject, out tileSpawn, out tileRotation);

        bool willOverlap = tileLogic.tileOverlapping(tileToSpawn, tileSpawn, this.gameObject);

        if (willOverlap)
        {
            Debug.Log($"Overlap Detected on {name} at position {tileSpawn} with tile {tileToSpawn}");

            if (!generateMore) return;

            if (tileLogic.mapGenInstance != null) tileLogic.mapGenInstance.FailedTileSpawn();
            else Debug.Log("Mapgen Null");

            return;
        }

        Tile spawnedTile = Instantiate(tileToSpawn, tileSpawn, tileRotation);

        //Debug.Log($"{this.name} spawned a tile on {spawnLocations[chosenSpawnSpot].name}"); //Debug line

        tileLogic.DeactivateDoorAndSpawner(chosenSpawn);

        spawnedTile.addTileToList(tileLogic.mapGenInstance, generateMore);
    }

    public void DoorChecks()
    {
        tileLogic.DoorChecks();
    }

    public void addTileToList(MapGenerator mapGen, bool moreGeneration)
    {
        gameObject.name = $"Tile: {tileLogic.ID}";
        tileLogic.addTileToList(mapGen, moreGeneration, this);
    }

    public void HallwayCheck()
    {
        tileLogic.HallwayCheck(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, tileLogic.overlapSize);
    }
}
