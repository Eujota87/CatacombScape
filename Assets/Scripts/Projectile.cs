using UnityEngine;

public class Projectile : MonoBehaviour
{


    [SerializeField] private float projectileSpeed = 850F;
    [SerializeField] private float projectileDamage = 50F;
    
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
        Destroy(gameObject);
    }

    public float ProjectileDamage
    {
        get => projectileDamage;
    }
    
}
