using System;
using UnityEngine;

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

    public float shootDelay = 0.2f; // delay before shooting after rotation
    public float shootInterval = 0.5f; // interval for constant shooting
    private float shootTimer = 0f;

    public float damage = 1f; // damage dealt by the projectile

    private InputSystem_Actions controls;
    public int quadrant = 1; // 1: leftmost, 2: left middle, 3: right middle, 4: rightmost
    private Vector2 forward;

    public float XP;

    public float XP_threshold = 100f; // XP needed to level up
    public int level = 1; // current player level
    private void Awake()
    {
        controls = new InputSystem_Actions();
        defaultRotation = transform.rotation;
    }

    public void Stop()
    {
        shouldShoot = false;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    public void Start()
    {
        shouldShoot = true;
        transform.rotation = defaultRotation;

        startRotation = transform.rotation;
        targetRotation = startRotation;
        forward = transform.up;

        controls.Player.pos1.performed += ctx => RotateAndScheduleShoot(59f);
        controls.Player.pos2.performed += ctx => RotateAndScheduleShoot(16f);
        controls.Player.pos3.performed += ctx => RotateAndScheduleShoot(-16f);
        controls.Player.pos4.performed += ctx => RotateAndScheduleShoot(-59f);
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
            }
        }
        else if (shouldShoot)
        {
          
            // If not rotating, ensure the player is shooting constantly
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                shoot_and_turn.Shoot(transform, quadrant, speed, damage, projectilePrefab);
                shootTimer = 0f;
            }
        }
        if (XP>= XP_threshold)
        {
            level++;
            XP -= XP_threshold; // reset XP but keep overflow
            XP_threshold *= 1.5f; // increase threshold for next level
            // TODO: add level up effects, stat increases, etc.
            Debug.Log("Level Up! Current Level: " + level);
        }
    }

    public void RotateAndScheduleShoot(float zAngle)
    {
        switch (zAngle)
        {
            case 59f:
                quadrant = 1;
                break;
            case 16f:
                quadrant = 2;
                break;
            case -16f:
                quadrant = 3;
                break;
            case -59f:
                quadrant = 4;
                break;
            default:
                quadrant = 0;
                break;
        }
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, zAngle);
        rotationTimer = 0f;
        isRotating = true;
        pendingShoot = true; // mark that we want to shoot after rotation
    }

    public static void Shoot(Transform transform, int quadrant, float speed = 10f, float damage = 1f, GameObject projectilePrefab = null)
    {
        Transform target;
        switch (quadrant)
        {
            case 1:
                target = shoot_and_turn.FindClosestEnemyInQuadrant("Enemy", 34f, 90f, transform);
                break;
            case 2:
                target = shoot_and_turn.FindClosestEnemyInQuadrant("Enemy", 0f, 30f, transform);
                break;
            case 3:
                target = shoot_and_turn.FindClosestEnemyInQuadrant("Enemy", -30f, 0f, transform);
                break;
            case 4:
                target = shoot_and_turn.FindClosestEnemyInQuadrant("Enemy", -90f, -34f, transform);
                break;
            default:
                target = null;
                break;
        }
        if (target != null)        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        GameObject projectile = Instantiate(projectilePrefab, transform.parent, false);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Rigidbody2D>().linearVelocity = transform.up * speed;
        // Set damage on the projectile
        bullet projScript = projectile.GetComponent<bullet>();
        projScript.damage_amount = damage;
    }
    public static Transform FindClosestEnemyInQuadrant(string tagName, float minAngle, float maxAngle, Transform transform = null)
{
    Vector2 forward = new Vector2(0, 1); // default forward direction (up)
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagName);

    Transform closest = null;
    float closestDist = Mathf.Infinity;


    foreach (GameObject enemy in enemies)
    {
        Vector2 dir = enemy.transform.position - transform.position;

        float angle = Vector2.SignedAngle(forward, dir);

        // Check if enemy is inside selected quadrant
        if (angle >= minAngle && angle <= maxAngle)
        {
            float dist = dir.sqrMagnitude;

            if (dist < closestDist)
            {
                closestDist = dist;
                closest = enemy.transform;
            }
        }
    }

    return closest;
}

    internal void Resume()
    {
        shouldShoot = true;
    }
}