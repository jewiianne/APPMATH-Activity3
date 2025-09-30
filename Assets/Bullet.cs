using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed;
    private Turret turret;

    public void SetDirection(Vector3 dir, float spd, float lifetime)
    {
        moveDirection = dir.normalized;
        speed = spd;
        Destroy(gameObject, lifetime);
    }

    public void SetTurret(Turret t)
    {
        turret = t;
    }
    
    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            if (turret != null)
            {
                turret.OnPlayerHit();
                turret.GameOver();
            }
            Destroy(gameObject);
        }
    }
}

