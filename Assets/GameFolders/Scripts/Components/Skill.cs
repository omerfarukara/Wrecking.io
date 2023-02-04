using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] float collisionForce;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag(Constants.Tags.OBSTACLE))
        {
            Debug.Log("Obstacle");
            var direction = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce((direction + new Vector3(direction.z * 0.25f, 0.75f, direction.z * 0.25f)) * collisionForce);
        }
    }

}
