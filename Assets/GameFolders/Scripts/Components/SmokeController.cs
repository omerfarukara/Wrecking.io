using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] float yPos;

    void Update()
    {
        if (!GameManager.Instance.Playability()) return;

        transform.position = new Vector3(ball.transform.position.x, yPos, ball.transform.position.z);
    }
}
