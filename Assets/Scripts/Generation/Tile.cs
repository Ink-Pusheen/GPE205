using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float radiusSize;
    Vector3 overlapSize;

    public List<GameObject> spawnLocations = new List<GameObject>();

    //North - 0, East - 1, South - 2, West - 3 [Increments will change based on what is removed]
    public List<GameObject> doors = new List<GameObject>(); //doorNorth, doorEast, doorSouth, doorWest;

    public void spawnTile(Tile tileToSpawn)
    {
        int chosenSpawnSpot = Random.Range(0, spawnLocations.Count);

        Tile spawnedTile = Instantiate(tileToSpawn, spawnLocations[chosenSpawnSpot].transform.forward + new Vector3(0, 0, radiusSize), Quaternion.identity);

        spawnedTile.gameObject.transform.rotation = Quaternion.FromToRotation(-spawnedTile.transform.forward, spawnLocations[chosenSpawnSpot].transform.forward);

        bool colliding = CollidingWithOtherTile(spawnedTile);

        if (colliding)
        {
            Destroy(spawnedTile.gameObject);
        }
        else
        {
            doors[chosenSpawnSpot].transform.Translate(0, -1, 0);

            spawnLocations.RemoveAt(chosenSpawnSpot);
            doors.RemoveAt(chosenSpawnSpot);
        }

    }


    bool CollidingWithOtherTile(Tile tileToCheck)
    {
        return Physics.CheckBox(tileToCheck.transform.position, tileToCheck.overlapSize, transform.rotation, 7);
    }
}
