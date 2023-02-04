using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMask : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private int rendererQueue;

    void Start()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.sharedMaterial.renderQueue = rendererQueue;
        }
    }
}
