using System;
using System.Collections;
using UnityEngine;

public class ShadowEnemyController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float enemyLife = 200F;
    [SerializeField] private float enemyDamage = 50F;

    public GameObject hitParticle;
    public GameObject walkingParticle;
    private Vector2 chaseDirectionVector;
    Vector2 facingDirection;
    private bool isDead = false;
    private bool isHit = false;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D myRigidBody;
    private Animator enemyAnimator;

    private enum States
    {
        iddle,
        run,
        attack,
        runAttack,
        chase,
        damage,
        dead
    }

    private States enemyState;
    
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        MovementCheck();
        
        if (enemyLife <= 0F)
        {
            StartCoroutine(Die());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            isHit = true;
            
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            
            TakeDamage(projectile.ProjectileDamage);
            Debug.Log("Enemy life: " + enemyLife);
            
            Instantiate(hitParticle, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) Instantiate(walkingParticle, transform.position, transform.rotation);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && enemyState != States.dead)
        {
            Chase(other);
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            chaseDirectionVector = Vector2.zero;
        }
    }

    private void MovementCheck()
    {
        if (chaseDirectionVector == Vector2.zero) enemyState = States.iddle;

        if (chaseDirectionVector != Vector2.zero) enemyState = States.chase;

        enemyAnimator.SetInteger("animState", (int)enemyState);
        
        if (isHit)
        {
            enemyAnimator.SetInteger("animState", 5);
            isHit = false;
        }

        if (chaseDirectionVector != Vector2.zero) facingDirection = chaseDirectionVector;
        if (facingDirection.x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }
    
    private void Chase(Collider2D other)
    {
        chaseDirectionVector = (Vector2)other.gameObject.transform.position - (Vector2)this.transform.position;
        chaseDirectionVector = chaseDirectionVector.normalized;
        myRigidBody.position += chaseDirectionVector * (movementSpeed * Time.deltaTime);
    }

    private void TakeDamage(float amount)
    {
        isHit = true;
        enemyLife -= amount;
    }

    IEnumerator Die()
    {
        enemyState = States.dead;
        enemyAnimator.SetInteger("animState", (int)enemyState);
        myRigidBody.simulated = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public float EnemyDamage
    {
        get => enemyDamage;
    }
    
}
