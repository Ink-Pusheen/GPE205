using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUIPlayer : UIBase
{
    private ControllerPlayer player;

    [SerializeField] Image[] lifeSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<ControllerPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void updateHealthBar(float newHealth, float maxHealth)
    {
        base.updateHealthBar(newHealth, maxHealth);

        if (newHealth <= 0)
        {
            //Update the lives icons
            player.lives--;

            if (player.lives <= 0)
            {
                Debug.Log("Game Over");

                for (int i = 0; i < 3; i++)
                {
                    lifeSprites[i].color = Color.red;
                }

                GameManager.instance.ChangeState("GameOverMenu");
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i < player.lives) lifeSprites[i].color = Color.green;

                    else lifeSprites[i].color = Color.red;
                }

                StartCoroutine(DelaySpawn());
            }
        }
    }


    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(1);

        MapGenerator mapGen = FindFirstObjectByType<MapGenerator>();
        //Respawn player tank
        if (mapGen != null) mapGen.RespawnPlayer(player);
    }
}
