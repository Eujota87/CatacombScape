using UnityEngine;

public class WandController : MonoBehaviour
{

    [SerializeField] private float manaCost = 20;
    private Vector2 aimVector;

    public GameObject projectile1;
    private PlayerController playerController;
    
    private void Start()
    {
        playerController = this.GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Aim();
            if (playerController.PlayerMana >= manaCost)
            {
                shootProjectile();
            }
        }
    }

    public Vector2 AimVector
    {
        get { return aimVector; }
    }
    
    private void Aim()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 WandToMouse = mousePos - (Vector2)transform.position;
        aimVector = WandToMouse.normalized;
    }

    private void shootProjectile()
    {
        playerController.PlayerMana -= manaCost;
        
        float aimAngle = Vector2.SignedAngle(Vector2.right, aimVector);
        Instantiate(projectile1, transform.position, Quaternion.Euler(0, 0, aimAngle - 90), transform);
        //Debug.Log("Vetor: " + aimVector);
        //Debug.Log("Angulo: " + aimAngle);
    }
}
