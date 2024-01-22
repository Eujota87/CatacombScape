using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{

    private TextMeshProUGUI textMeshPro;
    private Inventory inventory;
    
    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textMeshPro.SetText("{0}\n \n{1}\n \n{2}", inventory.LifePotionCount, inventory.ManaPotionCount, inventory.KeyCount);
    }
}
