using System;
using System.Collections;
using UnityEngine;

namespace GameFolders.Scripts.Controllers.Car
{
    public class CarController : MonoSingleton<CarController>
    {
        [SerializeField] private GameObject lineRope, lineBody;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float onGroundDistance;
        [SerializeField] private GameObject trailRenderer;

        private bool _onGround;
        
        EventData _eventData;

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
        }

        private void Start()
        {
            Singleton();
        }

        private void OnEnable()
        {
            _eventData.OnSkillHandler += LineControl;
        }
        
        void Update()
        {
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
                lineBody.transform.position = transform.position - Vector3.forward * 3.25f;
            }
            lineRope.SetActive(!statu);
            lineBody.SetActive(!statu);
        }
    }
}