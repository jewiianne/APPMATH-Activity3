using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotSpeed;

    public Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime;

        if (target == null) return;

        var direction = target.position - transform.position;
        direction.Normalize();
        var targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        var rot = Quaternion.Euler(0, 0, -targetAngle);
        var currentAngle = this.transform.rotation.z;
        var newAngle = Mathf.LerpAngle(currentAngle, targetAngle, speed * Time.deltaTime);
        this.transform.rotation = Quaternion.Euler(0, 0, newAngle);

        Debug.Log(targetAngle);
    }
}
