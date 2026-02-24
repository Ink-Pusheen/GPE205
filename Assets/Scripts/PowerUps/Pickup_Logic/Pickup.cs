using UnityEngine;

[RequireComponent (typeof(Collider))]
public abstract class Pickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        //Set collider to be a trigger
        Collider selfCollider = GetComponent<Collider>();
        if (selfCollider != null) selfCollider.isTrigger = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //TODO: Anything our pickup does every frame draw
    }

    public void OnTriggerEnter(Collider other)
    {
        //Check if other object has a powerup manager
        //If so, add the powerup the the manager
        PowerupManager PUM = other.GetComponent<PowerupManager>();
        Debug.Log(PUM);
        if (PUM == null) return;

        //Add this to the powerup manager
        ApplyPowerup(PUM);

        //Afterwards destroy this object
        Destroy(this.gameObject);
    }

    public abstract void ApplyPowerup(PowerupManager PUM);
}
