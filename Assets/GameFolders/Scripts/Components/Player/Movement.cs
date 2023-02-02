using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoSingleton<Movement>
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float turnSpeed;

    Rigidbody _rigidbody;

    float horizontal;
    float vertical;
    bool canRotate;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Singleton();
    }

    private void Update()
    {
        horizontal = UIController.Instance.GetHorizontal();
        vertical = UIController.Instance.GetVertical();

        if (Input.GetMouseButtonDown(0)) canRotate = true;
        if (Input.GetMouseButtonUp(0)) canRotate = false;
    }


    private void FixedUpdate()
    {
        // _rigidbody.AddRelativeForce(new Vector3(horizontal * forwardSpeed, _rigidbody.velocity.y, vertical * forwardSpeed));

        _rigidbody.velocity = new Vector3(horizontal * forwardSpeed, _rigidbody.velocity.y, vertical * forwardSpeed);
        if (!canRotate) return;

        Vector3 direction = Vector3.right * horizontal + Vector3.forward * vertical;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
            turnSpeed * Time.fixedDeltaTime);

        //_rigidbody.AddRelativeTorque(-direction*turnSpeed);
    }
}