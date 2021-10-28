using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour
{
    public LayerMask layerMask;

    private PlayerHealth playerhealth;

    void Start()
    {
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(layerMask.Contains(collision.collider))
        {
            playerHealth.Health -= Mathf.Clamp01(collision.relativeVelocity.magnitude / 10);
        }
    }
}
