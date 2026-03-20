using UnityEngine;

public class PawnTank : Pawn
{
    [SerializeField] bool isGrounded;
    [SerializeField] float groundedRadius;
    [SerializeField] GameObject groundCheck;
    [SerializeField] LayerMask ground;

    protected Shooter shooter;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        //Save tank to gamemanager
        GameManager.instance.tanks.Add(this);

        shooter = GetComponent<Shooter>();

        //Do what all pawns do
        base.Start();
    }

    private void OnDestroy()
    {
        //Remove tank fro gamemanager
        GameManager.instance.tanks.Remove(this);
    }

    public override void Move(Vector3 moveDirection)
    {
        mover.Move(moveDirection);
    }

    public override void Rotate(Vector3 rotateDirection)
    {
        if (!isGrounded) return;
        mover.Rotate(rotateDirection);
    }

    public override void Shoot(float power)
    {
        if (Time.time > shooter.nextShootTime)
        {
            shooter.Fire(power);
            shooter.nextShootTime = Time.time + shooter.fireRate;
        }
        
    }

    public override void Flip()
    {
        Vector3 eulars = transform.eulerAngles;

        float x = Mathf.Round(transform.rotation.x) % 90f;
        float z = Mathf.Round(transform.rotation.z) % 90f;

        if (x != 0 || z != 0)
        {
            transform.rotation = Quaternion.Euler(0, eulars.y, 0);
        }
    }

    public override void RotateTowards(Vector3 position)
    {
        mover.RotateTowards(position);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundedRadius, ground);

        if (!isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, rb.linearVelocity.x);
        }
    }
}