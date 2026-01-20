using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Controller playerOne;
    public Pawn startPawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Have Player One connect to a pawn
        playerOne.Possess(startPawn);
    }
}
