using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.Components.AI
{
    public class AIMovement : MonoSingleton<AIMovement>
    {
        [SerializeField] private Vector2 xAxis, zAxis;
        [SerializeField] private float speed, rotateSpeed;
        [SerializeField] private float distanceRange;
        [SerializeField] private float forcePower;

        private Vector3 _targetPosition;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            Singleton();
            SetTargetPosition();
        }

        void Update()
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, _targetPosition)) < distanceRange)
            {
                SetTargetPosition();
            }
        }

        void SetTargetPosition()
        {
            float x = Random.Range(xAxis.x, xAxis.y);
            float z = Random.Range(zAxis.x, zAxis.y);
            _targetPosition = new Vector3(x, 0.325f, z);
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.Playability()) return;

            Vector3 direction = (_targetPosition - transform.position).normalized;
            _rigidbody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.fixedDeltaTime);
        }


        [Button("JumpTest")]
        private void JumpTest()
        {
            _rigidbody.AddForce(Vector3.up * forcePower);
        }
    }
}