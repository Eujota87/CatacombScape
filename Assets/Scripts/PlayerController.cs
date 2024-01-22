using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4F;

    private float playerLife = 250F;
    private float playerMana = 400F;
    
    private Rigidbody2D myRigidBody;
    private Inventory inventory;
    
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
        InputAndMove();
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