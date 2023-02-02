using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, ballTransform.position);
    }
}
