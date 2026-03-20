using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageOtherOnOverlap : MonoBehaviour
{
    private GameObject owner;

    public float damage;
    private Collider mCol;

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mCol = GetComponent<Collider>();
        mCol.isTrigger = true;

        owner = transform.GetComponent<Projectile>().owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner || other.CompareTag("DoorCheck")) return;

        //Get the other objects health component to see if it exists
        Health hp = other.GetComponent<Health>();

        if (hp != null)
        {
            hp.TakeDamage(damage);
        }

        //Destroy this gameobject
        Destroy(gameObject);
    }
}
