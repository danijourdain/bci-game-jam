using UnityEngine;
using UnityEngine.InputSystem;
public class shoot_and_turn : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float speed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Shoot()
    {
       GameObject projectile = Instantiate(projectilePrefab, transform.parent,false);
       projectile.transform.position = transform.position;
       projectile.transform.rotation = transform.rotation;
       projectile.GetComponent<Rigidbody2D>().linearVelocity = transform.up * speed; 
    }


    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }
    private InputSystem_Actions controls;
    void Start()
    {
        controls.Player.pos1.performed += ctx =>
        {
            Debug.Log("pos1");
            transform.rotation = Quaternion.Euler(0, 0, 60);
            Shoot();
        };

        controls.Player.pos2.performed += ctx =>
        {
            transform.rotation = Quaternion.Euler(0, 0, 30);
            Shoot();
        };

        controls.Player.pos3.performed += ctx =>
        {
            transform.rotation = Quaternion.Euler(0, 0, -30);
            Shoot();
        };

        controls.Player.pos4.performed += ctx =>
        {
            transform.rotation = Quaternion.Euler(0, 0, -60);
            Shoot();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}