using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float lifeSpan;

    [SerializeField] private int damage;

    public Controller owner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
