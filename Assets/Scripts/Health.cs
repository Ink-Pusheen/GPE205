using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Variables")]

    [HideInInspector] public float currentHealth;
    public float maxHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmgTaken)
    {
        currentHealth -= dmgTaken;

        //Clamp the hp between 0 and max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Check for death
        if (currentHealth <= 0) Die();
    }

    public void Heal(float healthGained)
    {
        currentHealth += healthGained;

        //Clamp the hp between 0 and max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Check for death
        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} has Perished ;w;");
        Destroy(gameObject);
    }
}
