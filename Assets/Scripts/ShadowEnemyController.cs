using UnityEngine;

public class ShadowEnemyController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D myRigidBody;
    private float enemyLife = 100F;
    private float enemyDamage = 50F;
    
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (enemyLife <= 0F)
        {
            //Unity.Mathematics.Random random = new Unity.Mathematics.Random();
            //int drop = random.NextInt(1,2);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            
            TakeDamage(projectile.ProjectileDamage);
            Debug.Log("Enemy life: " + enemyLife);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Chase(other);
        } 
    }

    private void TakeDamage(float amount)
    {
        enemyLife -= amount;
        spriteRenderer.color = Color.gray;
    }
    
    private void Chase(Collider2D other)
    {
        Vector2 chaseDirectionVector = (Vector2)other.gameObject.transform.position - (Vector2)this.transform.position;
        chaseDirectionVector = chaseDirectionVector.normalized;
        myRigidBody.position += chaseDirectionVector * (movementSpeed * Time.deltaTime);
        if (chaseDirectionVector.x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    public float EnemyDamage
    {
        get => enemyDamage;
    }
    
}
