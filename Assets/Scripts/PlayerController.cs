using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player variables
    [SerializeField] private float movementSpeed = 4F;
    [SerializeField] private float playerLife = 250F;
    [SerializeField] private float playerMana = 400F;
    
    private bool isDead = false;
    
    private Rigidbody2D myRigidBody;
    private Inventory inventory;
    private Animator animator;

    //those are used to update the player state and tell the animator which animation to apply
    enum States
    {
        iddle,
        run,
        attack,
        stun,
        dead
    }

    private States playerState;
    
    private void Start()
    {
        //initializing/referencing components from other scripts
        myRigidBody = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        InputAndMove();
        
        //this is for checking the inputs and using potions to heal life and mana
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
    
    //Function responsible to check player input and apply movement to character
    private void InputAndMove()
    {
        Vector2 inputVector = new Vector2(0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
            playerState = States.run;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y += -1;
            playerState = States.run;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
            playerState = States.run;
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x += -1;
            playerState = States.run;
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        
        //this is basically a vector(line) that points to where the pressed keys direct the player to move
        inputVector = inputVector.normalized;
        myRigidBody.position += inputVector * (Time.deltaTime * movementSpeed);
        
        //this is temporary need to find a better way to update states based on animation end/transitions
        if (inputVector == Vector2.zero) playerState = States.iddle;
        if (Input.GetKeyDown(KeyCode.Mouse0)) playerState = States.attack;
        if (isDead) playerState = States.dead;
        
        animator.SetInteger("animState", (int)playerState);
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