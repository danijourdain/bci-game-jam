using UnityEngine;


public class ability : MonoBehaviour
{
    public float damage = 1;
    public float attackSpeed = 1;
    public float accuracy = 0f; //1 = will always miss, 0 = will always hit
    public float shootTimer = 0f;

    public bool is_available = true; // if the ability is unlocked or not

    public GameObject projectilePrefab;
    public GameObject player;
    public float speed = 10f;
    public int quadrant = 0; // 1 = top right, 2 = top left, 3 = bottom left, 4 = bottom right
    public shoot_and_turn attack_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
            if (shootTimer >= attackSpeed)
            {
                quadrant = player.GetComponent<shoot_and_turn>().quadrant;
                shoot_and_turn.Shoot(transform, quadrant, speed, damage, projectilePrefab);
                shootTimer = 0f;
            }
    }
}
