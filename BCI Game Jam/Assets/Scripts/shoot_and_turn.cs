using UnityEngine;
using UnityEngine.InputSystem;

public class shoot_and_turn : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float speed = 10f;
    public float rotationDuration = 0.2f; // rotate over 0.2 seconds

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationTimer = 0f;
    private bool isRotating = false;
    private bool pendingShoot = false; // flag to shoot after rotation

    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void Start()
    {
        startRotation = transform.rotation;
        targetRotation = transform.rotation;

        controls.Player.pos1.performed += ctx => RotateAndScheduleShoot(60f);
        controls.Player.pos2.performed += ctx => RotateAndScheduleShoot(30f);
        controls.Player.pos3.performed += ctx => RotateAndScheduleShoot(-30f);
        controls.Player.pos4.performed += ctx => RotateAndScheduleShoot(-60f);
    }

    void Update()
    {
        if (isRotating)
        {
            rotationTimer += Time.deltaTime;
            float t = rotationTimer / rotationDuration;
            t = Mathf.Clamp01(t);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            if (t >= 1f)
            {
                isRotating = false;
                if (pendingShoot)
                {
                    Shoot();
                    pendingShoot = false;
                }
            }
        }
    }

    void RotateAndScheduleShoot(float zAngle)
    {
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, zAngle);
        rotationTimer = 0f;
        isRotating = true;
        pendingShoot = true; // mark that we want to shoot after rotation
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.parent, false);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Rigidbody2D>().linearVelocity = transform.up * speed;
    }
}