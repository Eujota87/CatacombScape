using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private string chestItem;
    private bool isOpen = false;

    public GameObject particle;
    private Animator chestAnimation;
    private Inventory inventory;
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        chestAnimation = GetComponentInChildren<Animator>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isOpen) spriteRenderer.color = Color.gray;
                    
            if (Input.GetKeyDown(KeyCode.F) && !isOpen)
            {
                GetItem(chestItem);
                Instantiate(particle, transform.position, Quaternion.Euler(0, 0, transform.rotation.z));
                chestAnimation.SetInteger("animState", 1);
                isOpen = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void GetItem(string item)
    {
        switch (item)
        {
            case "key":
                inventory.KeyCount++;
                break;
            case "mana":
                inventory.ManaPotionCount++;
                break;
            case "life":
                inventory.LifePotionCount++;
                break;
        }
    }
}
