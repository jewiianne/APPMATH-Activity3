using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject bulletsPrefab;
    public float speed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject rocket = Instantiate(bulletsPrefab, transform.position, Quaternion.identity);
        rb.linearVelocity = direction * speed;
    }
}
