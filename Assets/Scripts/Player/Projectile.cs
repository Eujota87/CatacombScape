using UnityEngine;

public class Projectile : MonoBehaviour
{


    [SerializeField] private float projectileSpeed = 850F;
    [SerializeField] private float projectileDamage = 50F;
    
    public GameObject particleHit;
    public GameObject particleTrail;
    private WandController wandController;
    private Rigidbody2D myRigidBody;
    private GameObject trailObj;
    private ParticleSystem trailObjParticle;
    
    private void Start()
    {

        myRigidBody = GetComponent<Rigidbody2D>();
        wandController = this.GetComponentInParent<WandController>();
        
        myRigidBody.AddForce(wandController.AimVector * projectileSpeed);
        
        trailObj = Instantiate(particleTrail, transform.position, Quaternion.Euler(0, 0, transform.rotation.z));
        trailObjParticle = trailObj.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        trailObj.transform.position = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        trailObjParticle.Stop();
        Instantiate(particleHit, transform.position, Quaternion.Euler(0, 0, transform.rotation.z));
        Destroy(gameObject);
    }

    public float ProjectileDamage
    {
        get => projectileDamage;
    }
    
}
