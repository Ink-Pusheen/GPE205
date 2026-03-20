using UnityEngine;

public class DoorSpawnCheck : MonoBehaviour
{
    [SerializeField] GameObject doorToLower;
    [SerializeField] GameObject doorSpawner;

    public bool activatable = true;

    [SerializeField] Tile parentTile;

    private void OnTriggerEnter(Collider other)
    {
        if (!activatable) return;

        if (other.CompareTag("Door"))

            if (other.gameObject != doorToLower)
            {
                doorToLower.transform.Translate(0, -1, 0);
                activatable = !activatable;

                //TODO: Make the designated door and spawnpoint remove themselves as a possible spawn spot.
                parentTile.tileLogic.doors.Remove(doorToLower);
                parentTile.tileLogic.genSpawnLocations.Remove(doorSpawner);
            }
    }

    public void checkForDoors()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1), transform.rotation);

        if (hits.Length > 0)
        {
            for (int i = hits.Length - 1; i >= 0; i--)
            {
                if (hits[i].gameObject == doorToLower || hits[i] == null) continue;

                if (hits[i].gameObject.CompareTag("Door"))
                {
                    doorToLower.transform.Translate(0, -1, 0);
                    activatable = !activatable;

                    //TODO: Make the designated door and spawnpoint remove themselves as a possible spawn spot.
                    parentTile.tileLogic.doors.Remove(doorToLower);
                    parentTile.tileLogic.genSpawnLocations.Remove(doorSpawner);

                    Tile doorCheck = hits[i].gameObject.transform.parent.parent.GetComponentInParent<Tile>();

                    if (doorCheck != null)
                    {
                        doorCheck.DoorChecks();
                    }
                }
            }

        }
    }

    public void ForceOpen()
    {
        if (!activatable) return;

        doorToLower.transform.Translate(0, -1, 0);
        activatable = !activatable;

        //TODO: Make the designated door and spawnpoint remove themselves as a possible spawn spot.
        parentTile.tileLogic.doors.Remove(doorToLower);
        parentTile.tileLogic.genSpawnLocations.Remove(doorSpawner);

        activatable = !activatable;
    }
}
