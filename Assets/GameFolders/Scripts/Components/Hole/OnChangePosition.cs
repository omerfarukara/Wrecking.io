using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    [SerializeField] float yLockedPosition;

    public PolygonCollider2D hole2DCollider;
    public PolygonCollider2D ground2DCollider;
    public MeshCollider GeneratedMeshCollider;
    public float initialScale = 0.5f;
    Mesh GeneratedMesh;

    private void FixedUpdate()
    {
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector3(transform.position.x, transform.position.z);
            hole2DCollider.transform.localScale = transform.localScale * initialScale;
            MakeHole2D();
            MakeMeshCollider();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tags.CUBE))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.down * 2);
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.Playability()) return;

        transform.localPosition = new Vector3(transform.localPosition.x, yLockedPosition, transform.localPosition.z);
    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DCollider.GetPath(0);
        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2DCollider.transform.TransformPoint(PointPositions[i]);
        }

        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, PointPositions);
    }

    private void MakeMeshCollider()
    {
        if (GeneratedMesh != null) Destroy(GeneratedMesh);
        GeneratedMesh = ground2DCollider.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = GeneratedMesh;
    }
}
