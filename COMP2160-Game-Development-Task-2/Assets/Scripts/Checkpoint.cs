using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Range(0,100)]
    public int healthRestoredAtCheckpoint = 10;
    public float radius;

    void Start()
    {
        SphereCollider collider = transform.GetComponent<SphereCollider>();
        collider.radius = radius;
    }

    void OnTriggerEnter(Collider other)
    {
        if(transform.tag == "Active")
        {
            GameManager.Instance.ActivateNextCheckpoint();

            PlayerHealth player = other.GetComponent<PlayerHealth>();
            player.Health += healthRestoredAtCheckpoint;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
