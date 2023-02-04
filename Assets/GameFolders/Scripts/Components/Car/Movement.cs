using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Controllers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoSingleton<Movement>
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] private float maxSpeed;

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
        if (!GameManager.Instance.Playability()) return;

        if (_rigidbody.velocity.magnitude < maxSpeed)
        {
            _rigidbody.AddRelativeForce(0, 0, forwardSpeed);
        }

        if (!canRotate) return;

        Vector3 direction = Vector3.right * horizontal + Vector3.forward * vertical;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
            turnSpeed * Time.fixedDeltaTime);
    }
}