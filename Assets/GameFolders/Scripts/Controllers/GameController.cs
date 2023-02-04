using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class GameController : MonoSingleton<GameController>
{
    [SerializeField] private GameType gameType;


    [SerializeField][ShowIf("gameTypeIsNormal")] private GameObject plane;
    [SerializeField][ShowIf("gameTypeIsNormal")] private Material explodeMaterial;

    private EventData _eventData;
    private PlaneController currentMesh;

    bool gameTypeIsNormal;

    private void Awake()
    {
        Singleton();
        _eventData = Resources.Load("EventData") as EventData;
    }
    private void Start()
    {
        switch (gameType)
        {
            case GameType.Normal:
                FindToExplodeObject();
                break;
            case GameType.Hole:
                break;
            default:
                break;
        }
    }

    public void FindToExplodeObject()
    {
        List<PlaneController> meshes = plane.GetComponentsInChildren<PlaneController>().ToList();

        if (meshes.Count == 0) return;
        currentMesh = meshes.OrderByDescending(m => m.transform.position.magnitude).First(m=>m.gameObject.activeInHierarchy);
        currentMesh.ExplodeThisObject(explodeMaterial);
    }

    private void OnValidate()
    {
        switch (gameType)
        {
            case GameType.Normal:
                gameTypeIsNormal = true;
                break;
            case GameType.Hole:
                gameTypeIsNormal = false;
                break;
            default:
                break;
        }
    }
}
