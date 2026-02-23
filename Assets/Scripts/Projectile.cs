using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float lifeSpan;

    [SerializeField] private int damage;

    [SerializeField] GameObject owner;

    public void setOwner(GameObject setOwner)
    {
        owner = setOwner;
    }

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
        if (other.gameObject == owner) return; //Ignore the owner

        //Else check if it has the health component, and if it does, deal damage
        Health hitHealth = other.GetComponent<Health>();

        if (hitHealth != null)
        {
            hitHealth.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
