using System;
using System.Collections;
using UnityEngine;

namespace GameFolders.Scripts.Controllers.Car
{
    public class CarController : MonoSingleton<CarController>
    {
        [SerializeField] private GameObject lineRope, lineBody;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float onGroundDistance, collisionForce;
        [SerializeField] private GameObject trailRenderer;

        private bool _onGround;

        EventData _eventData;
        Rigidbody _rigidbody;

        public bool OnGround
        {
            get => _onGround;
            set
            {
                _onGround = value;
                if (value)
                {
                    trailRenderer.SetActive(true);
                }
                else
                {
                    trailRenderer.SetActive(false);
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
            _eventData.OnSkillHandler += LineControl;
        }

        private void Start()
        {
            Singleton();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag(Constants.Tags.AI))
            {
                var direction = (transform.position - collision.transform.position).normalized;
                _rigidbody.AddForce(direction * collisionForce);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tags.DEADAREA))
            {
                Debug.Log("Collider");
                _eventData.OnFinish?.Invoke(false);
            }

            if (other.CompareTag(Constants.Tags.FIREOBJ))
            {
                Debug.Log("asd");
                _eventData.OnSkillHandler?.Invoke(true);
                other.GetComponentInChildren<MeshExploder>().Explode();
                other.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            if (!GameManager.Instance.Playability()) return;

            #region OnGroundRaycast

            RaycastHit onGroundHit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out onGroundHit, onGroundDistance, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * onGroundHit.distance, Color.yellow);
                OnGround = true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                OnGround = false;
            }

            #endregion
        }

        private void OnDisable()
        {
            _eventData.OnSkillHandler -= LineControl;
        }

        private void LineControl(bool statu)
        {
            if (!statu)
            {
                lineBody.transform.position = transform.position - Vector3.forward * -5.6f;
            }
            lineRope.SetActive(!statu);
            lineBody.SetActive(!statu);
        }
    }
}