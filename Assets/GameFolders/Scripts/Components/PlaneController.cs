using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoSingleton<PlaneController>
{
    Material _material;
    Material _currentMaterial;

    MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _currentMaterial = _meshRenderer.material;
    }

    private void Start()
    {
        Singleton();
    }

    public void ExplodeThisObject(Material material)
    {
        StartCoroutine(FindExplodeCoroutine(material));
    }

    private void ChangeColor(Material material)
    {
        _meshRenderer.material = material;
    }
    private void ResetColor()
    {
        _meshRenderer.material = _currentMaterial;
    }

    IEnumerator FindExplodeCoroutine(Material material)
    {
        ChangeColor(material);
        yield return new WaitForSeconds(0.5f);
        ResetColor();
        yield return new WaitForSeconds(0.5f);
        ChangeColor(material);
        yield return new WaitForSeconds(0.5f);
        ResetColor();
        yield return new WaitForSeconds(0.5f);
        ChangeColor(material);
        yield return new WaitForSeconds(0.5f);
        ResetColor();
        yield return new WaitForSeconds(0.5f);

        BroadcastMessage("Explode");
        gameObject.SetActive(false);
        GameController.Instance.FindToExplodeObject();
    }
}
