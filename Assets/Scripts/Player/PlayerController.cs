using System;
using System.Collections;
using Unity.VisualScripting;
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
    private ShadowEnemyController shadowEnemyController;
    private SpriteRenderer spriteRenderer;

    enum States
    {
        iddle,
        run,
        attack,
        runAttack,
        temp,
        damage,
        dead
    }
    private States playerState;
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (playerLife <= 0)
        {
            Die();
        }
        if (!isDead && playerState != States.damage)
        {
            InputAndMove();
            RunParticleSpawn();

            if (Input.GetKeyDown(KeyCode.E))
            {
                ConsumeLifePotion(40F);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ConsumeManaPotion(50F);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) //change this tag to shadow enemy later
        {
            shadowEnemyController = other.gameObject.GetComponent<ShadowEnemyController>();
            StartCoroutine(TakeDamage(shadowEnemyController.EnemyDamage, shadowEnemyController.EnemyFacingDirection, 200F, 0.3F));
        }
    }
    private void InputAndMove()
    {
        Vector2 inputVector = Vector2.zero;
        
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
            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x += -1;
            spriteRenderer.flipX = true;
        }
        
        inputVector = inputVector.normalized;
        myRigidBody.position += inputVector * (Time.deltaTime * movementSpeed);

        MoveStateUpdate(inputVector);
    }
    private void MoveStateUpdate(Vector2 inputVector)
    {
        if (inputVector == Vector2.zero) playerState = States.iddle;
        
        if (inputVector != Vector2.zero) playerState = States.run;

        if ((inputVector != Vector2.zero) && Input.GetKeyDown(KeyCode.Mouse0)) playerState = States.runAttack;
        
        if ((inputVector == Vector2.zero) && Input.GetKeyDown(KeyCode.Mouse0)) playerState = States.attack;
        
        animator.SetInteger("animState", (int)playerState);
    }
    IEnumerator TakeDamage(float damageAmount, Vector2 stunDirection, float stunAmount, float stunTime)
    {
        playerState = States.damage;
        playerLife -= damageAmount;
        myRigidBody.AddForce(stunDirection * stunAmount);
        animator.SetInteger("animState", (int)States.damage);
        yield return new WaitForSeconds(stunTime);
        
        playerState = States.iddle;
        myRigidBody.velocity = Vector2.zero;
    }
    private void Die()
    {
        playerState = States.dead;
        isDead = true;
        myRigidBody.simulated = false;
        animator.SetInteger("animState", (int)playerState);
    }
    private void RunParticleSpawn()
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