using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PawnToFollow;

    [SerializeField] Vector3 offset;

    public void SetPosition()
    {
        transform.localPosition = Vector3.zero;

        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
