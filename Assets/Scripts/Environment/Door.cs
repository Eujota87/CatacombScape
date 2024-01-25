using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Door : MonoBehaviour
{
    private Light2D doorLight;
    private Inventory inventory;
    
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        doorLight = GetComponentInChildren<Light2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (inventory.KeyCount > 0)
            {
                inventory.KeyCount--;
                GetComponentInParent<BoxCollider2D>().enabled = false;
                doorLight.enabled = true;
            }
        }
    }
}
