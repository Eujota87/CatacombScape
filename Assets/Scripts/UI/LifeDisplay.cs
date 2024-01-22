using UnityEngine;
using TMPro;

public class LifeDisplay : MonoBehaviour
{

    private TextMeshProUGUI textMeshPro;
    private PlayerController playerController;
    
    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        textMeshPro.SetText("{0}", playerController.PlayerLife);
    }
}
