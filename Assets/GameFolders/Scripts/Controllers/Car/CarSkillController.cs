using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Controllers.AI;
using GameFolders.Scripts.Controllers.Car;
using UnityEngine;

public class CarSkillController : MonoBehaviour
{
    [SerializeField] private GameObject skillObject;
    [SerializeField] private GameObject vfx;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int skillDuration;

    EventData _eventData;

    private bool _skillStatu = false;
    private float skillTimer;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
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
        if (!_skillStatu)
        {
            StartCoroutine(SkillCoroutine(statu));
        }
        else
        {
            skillTimer += skillDuration;
        }
    }

    IEnumerator SkillCoroutine(bool statu)
    {
        vfx.SetActive(statu);
        skillObject.SetActive(statu);
        if (!statu) yield break;

        _skillStatu = statu;
        skillTimer = skillDuration;

        while (_skillStatu)
        {
            while (skillTimer > 0)
            {
                skillTimer -= Time.deltaTime;
                transform.position = CarController.Instance.transform.position;
                transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);

                if (skillTimer <= 0)
                {
                    _skillStatu = false;
                    _eventData.OnSkillHandler?.Invoke(false);
                }
                yield return null;
            }
            yield return null;
        }
        yield break;
    }
}