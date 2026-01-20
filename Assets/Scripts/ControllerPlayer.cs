using UnityEngine;

public class ControllerPlayer : Controller
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void MakeDecisions()
    {
        //TODO: Write the logic of the player here

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Hello");
        }
    }
}
