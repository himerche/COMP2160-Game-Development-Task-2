using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour
{
    public LayerMask layerMask;
    public int damageMustExceed = 5;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(layerMask.Contains(collision.gameObject))
        {
            int damage = (int)Mathf.Clamp01(collision.relativeVelocity.magnitude / 10);

            if(damage > damageMustExceed)
            {
                playerHealth.Health -= damage;
            }
        }
    }
}
