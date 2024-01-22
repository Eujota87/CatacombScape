using UnityEngine;

public class Inventory : MonoBehaviour
{

    private int keyCount = 0;
    private int manaPotionCount = 2;
    private int lifePotionCount = 3;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public int ManaPotionCount
    {
        get => manaPotionCount;
        set => manaPotionCount = value;
    }
    public int LifePotionCount
    {
        get => lifePotionCount;
        set => lifePotionCount = value;
    }
    public int KeyCount
    {
        get => keyCount;
        set => keyCount = value;
    }
    
    
}
