using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4F;
    [SerializeField] private float playerLife = 250F;
    [SerializeField] private float playerMana = 400F;
    
    private bool isDead = false;

    public GameObject walkingParticle;
    private Rigidbody2D myRigidBody;
    private Inventory inventory;
    private Animator animator;

    enum States
    {
        iddle,
        run,
        attack,
        runAttack,
        stun,
        damage,
        dead
    }

    private States playerState;
    
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        InputAndMove();
        ParticleSpawn();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            ConsumeLifePotion(40F);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ConsumeManaPotion(50F);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(other.gameObject.GetComponent<ShadowEnemyController>().EnemyDamage);
        }
    }
    
    private void InputAndMove()
    {
        Vector2 inputVector = new Vector2(0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x += -1;
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        
        inputVector = inputVector.normalized;
        myRigidBody.position += inputVector * (Time.deltaTime * movementSpeed);
        
        if (inputVector == Vector2.zero) playerState = States.iddle;
        
        if (inputVector != Vector2.zero) playerState = States.run;

        if ((inputVector != Vector2.zero) && Input.GetKeyDown(KeyCode.Mouse0)) playerState = States.runAttack;
        
        if ((inputVector == Vector2.zero) && Input.GetKeyDown(KeyCode.Mouse0)) playerState = States.attack;
        
        if (isDead) playerState = States.dead;
        
        animator.SetInteger("animState", (int)playerState);
    }

    private void ParticleSpawn()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.D))
        {
            Instantiate(walkingParticle, transform.position, transform.rotation);
        }
    }
    
    private void ConsumeLifePotion(float amount)
    {
        if (inventory.LifePotionCount > 0)
        {
            inventory.LifePotionCount--;
            playerLife += amount;
        }
    }
    private void ConsumeManaPotion(float amount)
    {
        if (inventory.ManaPotionCount > 0)
        {
            inventory.ManaPotionCount--;
            playerMana += amount;
        }
    }

    public void TakeDamage(float amount)
    {
        playerLife -= amount;
    }
    
    public float PlayerLife
    {
        get { return playerLife; }
        set { playerLife = value; }
    }

    public float PlayerMana
    {
        get { return playerMana; }
        set { playerMana = value; }
    }
}