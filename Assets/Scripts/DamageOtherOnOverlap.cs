using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageOtherOnOverlap : MonoBehaviour
{
    public float damage;
    private Collider mCol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mCol = GetComponent<Collider>();
        mCol.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
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
