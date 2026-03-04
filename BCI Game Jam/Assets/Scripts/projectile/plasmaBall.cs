using UnityEngine;

public class plasmaBall: MonoBehaviour
{
    public float despawnTimer = 60.0f;
    public float damage_amount = 1.0f; // amount of damage this bullet does to the player
    public bool piercing = false; // whether the bullet can pierce through enemies or not
    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.transform.localScale *= 1.5f; // increase the size of the plasma ball on impact
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // stop the plasma ball's movement on impact
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale*1.5f, Vector3.one, Time.deltaTime * 5f);
            other.gameObject.GetComponent<enemy>().TakeDamage(damage_amount);

            if (piercing == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
