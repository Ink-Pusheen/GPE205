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
        Debug.Log("Taken Damage");
        //Clamp the hp between 0 and max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Check for death
        if (currentHealth <= 0)
        {
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
