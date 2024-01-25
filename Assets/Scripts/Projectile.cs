using UnityEngine;

public class Projectile : MonoBehaviour
{


    [SerializeField] private float projectileSpeed = 850F;
    [SerializeField] private float projectileDamage = 50F;
    public GameObject particle;
    
    private WandController wandController;
    private Rigidbody2D myRigidBody;
    
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        wandController = this.GetComponentInParent<WandController>();
        
        myRigidBody.AddForce(wandController.AimVector * projectileSpeed);
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(particle, transform.position, Quaternion.Euler(0, 0, transform.rotation.z));
        Destroy(gameObject);
    }

    public float ProjectileDamage
    {
        get => projectileDamage;
    }
    
}
