using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Controllers.AI;
using GameFolders.Scripts.Controllers.Car;
using UnityEngine;

public class CarSkillController : MonoBehaviour
{
    [SerializeField] private GameObject skillObject;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int skillDuration;

    EventData _eventData;

    private bool _skillStatu = false;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _eventData.OnSkillHandler?.Invoke(true);
        }
    }

    private void OnEnable()
    {
        _eventData.OnSkillHandler += Skill;
    }

    private void OnDisable()
    {
        _eventData.OnSkillHandler -= Skill;
    }

    private void Skill(bool statu)
    {
        StartCoroutine(SkillCoroutine(statu));
        StartCoroutine(TimerSkillCoroutine(statu));
    }

    IEnumerator SkillCoroutine(bool statu)
    {
        skillObject.SetActive(statu);
        _skillStatu = statu;

        while (_skillStatu)
        {
            transform.position = CarController.Instance.transform.position;
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
            yield return null;
        }
    }

    IEnumerator TimerSkillCoroutine(bool statu)
    {
        if (!statu) yield break;
        yield return new WaitForSeconds(skillDuration);
        _skillStatu = false;
        _eventData.OnSkillHandler?.Invoke(false);
    }
}