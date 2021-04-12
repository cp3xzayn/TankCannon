using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float m_speed = 2;
    [SerializeField] FloatingJoystick m_floatingJoystick = null;
    Rigidbody rb;
    private Vector3 m_direction;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (m_direction != Vector3.zero)
        {
            transform.localRotation = Quaternion.LookRotation(m_direction);
        }
    }

    void FixedUpdate()
    {
        m_direction = Vector3.forward * m_floatingJoystick.Vertical + Vector3.right * m_floatingJoystick.Horizontal;
        rb.velocity = m_direction * m_speed;
    }
}
