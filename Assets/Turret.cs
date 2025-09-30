using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  

public class Turret : MonoBehaviour
{
    public Transform target;
    public float rotSpeed = 5f;

    public float range = 10f;
    public float fireAngleThreshold = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 1f;
    private float fireTimer = 0f;

    public float bulletSpeed = 10f;
    public float bulletLifetime = 5f;

    private bool playerHit = false;

    public GameObject gameOverScreen;

    void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (target == null || playerHit) return;

        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        fireTimer -= Time.deltaTime;

        if (distance <= range && fireTimer <= 0f)
        {
            float angleDiff = Mathf.Abs(Mathf.DeltaAngle(newAngle, targetAngle));
            if (angleDiff <= fireAngleThreshold)
            {
                Fire(direction);
                fireTimer = fireCooldown;
            }
        }
    }

    void Fire(Vector3 direction)
    {
        GameObject proj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = proj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.SetDirection(direction, bulletSpeed, bulletLifetime);
            bullet.SetTurret(this);
        }
    }

    public void OnPlayerHit()
    {
        playerHit = true;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



