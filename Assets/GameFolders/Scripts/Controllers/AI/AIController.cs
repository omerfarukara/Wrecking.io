using GameFolders.Scripts.Components.AI;
using System;
using UnityEngine;
using DG.Tweening;

namespace GameFolders.Scripts.Controllers.AI
{
    public class AIController : MonoSingleton<AIController>
    {
        [SerializeField] private GameObject lineRope, lineBody;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float onGroundHitDistance,ResetRotationHitDistance;
        [SerializeField] private TrailRenderer l_trail,r_trail;
        [SerializeField] private float collisionForce;

        private bool _onGround;

        private EventData _eventData;
        private Rigidbody _rigidbody;

        public bool OnGround
        {
            get => _onGround;
            set
            { 
                _onGround = value;
                if (value)
                {
                    l_trail.emitting = true;
                    r_trail.emitting = true;
                }
                else
                {
                    l_trail.emitting = false;
                    r_trail.emitting = false;
                }
            } 
        }

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _eventData.AIOnSkillHandler += LineControl;
        }

        private void Start()
        {
            Singleton();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag(Constants.Tags.PLAYER))
            {
                Debug.Log("Collision");

                var direction = (transform.position - collision.transform.position).normalized;
                _rigidbody.AddForce(direction * collisionForce);
            }
        }

        void Update()
        {
            #region OnGroundRaycast
            
            RaycastHit onGroundHit;
            if (Physics.Raycast(transform.position, Vector3.down, out onGroundHit, onGroundHitDistance, layerMask))
            {
                Debug.DrawRay(transform.position, Vector3.down * onGroundHit.distance, Color.yellow);
                OnGround = true;
            }
            else
            {
                Debug.DrawRay(transform.position, Vector3.down * onGroundHitDistance, Color.white);
                OnGround = false;
            }

            #endregion

            #region ResetRotationHit

            RaycastHit ResetRotationHit;
            if (Physics.Raycast(transform.position, Vector3.down, out ResetRotationHit, ResetRotationHitDistance, layerMask))
            {
                Debug.DrawRay(transform.position, Vector3.down * ResetRotationHit.distance, Color.red);
                if (_rigidbody.velocity.y < 0)
                {
                    Debug.Log("Çalýþtý");

                    transform.DOLocalRotate(Vector3.zero,0.5f);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, Vector3.down * ResetRotationHitDistance, Color.blue);
            }

            #endregion
        }

        private void OnDisable()
        {
            _eventData.AIOnSkillHandler -= LineControl;
        }

        private void LineControl(bool statu)
        {
            if (!statu)
            {
                lineBody.transform.position = transform.position - Vector3.forward * 3.25f;
            }
            lineRope.SetActive(!statu);
            lineBody.SetActive(!statu);
        }
    }
}
