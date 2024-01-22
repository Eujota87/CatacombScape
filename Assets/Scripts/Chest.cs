using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private string chestItem;
    private bool chestOpen = false;
    
    private Inventory inventory;
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
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
            spriteRenderer.color = Color.gray;
                    
            if (Input.GetKeyDown(KeyCode.F) && !chestOpen)
            {
                GetItem(chestItem);
                chestOpen = true;
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
