using UnityEngine;

public class DoorSpawnCheck : MonoBehaviour
{
    [SerializeField] GameObject doorToLower;

    bool activatable;

    private void OnTriggerEnter(Collider other)
    {
        if (!activatable) return;
        if (other.gameObject == doorToLower) return;

        if (other.CompareTag("Door"))
        {
            doorToLower.transform.Translate(0, -1, 0);
            activatable = !activatable;

            //TODO: Make the designated door and spawnpoint remove themselves as a possible spawn spot.
        }
    }
}
