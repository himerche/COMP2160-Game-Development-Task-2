using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour
{
    public LayerMask layerMask;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(layerMask.Contains(collision.gameObject))
        {
            playerHealth.Health -= (int)Mathf.Clamp01(collision.relativeVelocity.magnitude / 10);
        }
    }
}
