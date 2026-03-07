using UnityEngine;

public class laser: MonoBehaviour
{
    public float despawnTimer = 0.1f;
    public float damage_amount = 1.0f; // amount of damage this bullet does to the player
    public bool piercing = false; // whether the bullet can pierce through enemies or not
    public int level = 0; // level of the laser, which determines its damage and size
    private Vector3 scale;
    void Start()
    {
        // set velocity to zero
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        AudioManager.Instance.Play(AudioManager.SoundType.Laser);
        // set the Y transform to 100 to make sure the laser stretches across the whole screen
        Vector3 newScale = transform.localScale;
        newScale.y = 100f*transform.localScale.y;
        newScale.x = transform.localScale.x*(1 + level);
        transform.localScale = newScale;
        scale = newScale;
    }
    private bool isGrowing = true; // whether the bullet is currently growing in size after hitting an enemy
    private float growTimer = 0f; // timer for the rotation effect after hitting an enemy
    private float growDuration = 0.1f; // duration of the rotation effect after hitting an enemy\
    

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
            transform.localScale = Vector3.Lerp(scale, new Vector3(0,500,0), t);
            if (t >= 1f)
            {
                isGrowing = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<IBaseEnemy>().TakeDamage(damage_amount);
            if (piercing == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
