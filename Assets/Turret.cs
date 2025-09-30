using System.Net.Sockets;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float rotspeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        var direction = target.position - transform.position;
        direction.Normalize();

        var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var currentAngle = transform.eulerAngles.z;
        var newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotspeed * Time.deltaTime);

        this.transform.rotation = Quaternion.Euler(0, 0, newAngle);

        Debug.Log(targetAngle);
    }
}
