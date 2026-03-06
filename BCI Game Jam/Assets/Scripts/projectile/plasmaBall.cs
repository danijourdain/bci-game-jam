using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class plasmaBall: MonoBehaviour
{
    public float despawnTimer = 60.0f;
    public float damage_amount = 1.0f; // amount of damage this bullet does to the player
    public bool piercing = false; // whether the bullet can pierce through enemies or not
    public bool hit = false; // whether the bullet has hit an enemy or not
    private bool isGrowing = false; // whether the bullet is currently growing in size after hitting an enemy
    private float growTimer = 0f; // timer for the rotation effect after hitting an enemy
    private float growDuration = 0.5f; // duration of the rotation effect after hitting an enemy
    public int level = 0; // level of the plasma ball, which determines its damage and size
    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }

        if (isGrowing)
        {
            growTimer += Time.deltaTime;
            float t = growTimer / growDuration;
            t = Mathf.Clamp01(t);
            transform.localScale = Vector3.Lerp(new Vector3(5,5,5), new Vector3(5,5,5) * (1 + level), t);
            if (t >= 1f)
            {
                isGrowing = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && hit == false)
        {
            StartCoroutine(hitEnemy(other));
        }
    }

    System.Collections.IEnumerator hitEnemy(Collider2D other)
    {
        hit = true;
        gameObject.transform.localScale *= 1.5f; // increase the size of the plasma ball on impact
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // stop the plasma ball's movement on impact
        growDuration = 0.5f*level;
        isGrowing = true;
        other.gameObject.GetComponent<IBaseEnemy>().TakeDamage(damage_amount);
        yield return new WaitForSeconds(0.5f*level); // wait for 0.5 seconds before destroying the plasma ball
        Destroy(gameObject);
    }
}


