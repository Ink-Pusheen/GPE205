using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Variables")]

     public float currentHealth;
    public float maxHealth;

    [SerializeField] private Pawn parentPawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentPawn = GetComponent<Pawn>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmgTaken, Controller hitByController)
    {
        currentHealth -= dmgTaken;

        //Clamp the hp between 0 and max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Check for death
        if (currentHealth <= 0)
        {
            //Play the death sound, and delay the destruction of the object
            parentPawn.audioSource.PlaySoundOneShot(0, 1, 1);
            parentPawn.audioSource.QueueSelfDestruct(4);
            parentPawn.audioSource.transform.SetParent(null, true);

            //Reward Points if this is an AI
            ControllerAI aiController = parentPawn.controller.GetComponent<ControllerAI>();

            if (aiController != null)
            {
                Controller rewardedOwner = hitByController;
                rewardedOwner.scoredPoints += 100;
                rewardedOwner.updateScore();
            }

            //Perish
            Die();
        }
        else
        {
            //Play the damage sound
            parentPawn.audioSource.PlaySoundOneShot(0, 1, 0);
        }

            //Update the tank UI
            parentPawn.controller.tankUI.updateHealthBar(currentHealth, maxHealth);

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

        CameraScript playerCam = GetComponentInChildren<CameraScript>();
        Debug.Log(playerCam);
        if (playerCam != null) playerCam.transform.SetParent(parentPawn.controller.transform, true);

        Destroy(gameObject);
    }
}
