using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Audio;

public class ShooterTank : Shooter
{
    [SerializeField] AudioMixer mixer;

    [SerializeField] Controller owner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        owner = transform.GetComponent<Pawn>().controller;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fire()
    {
        if (Time.time > nextShootTime)
        {
            Fire(pawn.shootPower);
            nextShootTime = Time.time + (1/fireRate); //Inverses the formula from seconds per shot from shots per second
        }
        
    }

    public override void Fire(float power)
    {
        //Instantiate the bullet at the muzzle and rotation and the owner
        GameObject bullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
        bullet.GetComponent<DamageOtherOnOverlap>().owner = owner;
        
        //Push it forward
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(muzzleTransform.forward * power);

        //Play Audio
        if (owner.pawn != null) owner.pawn.audioSource.PlayRandomSoundFromArray(0, 0);
    }
}
