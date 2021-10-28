using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 100;
    public int Health {
        get 
        {
            return health;
        }
        set
        {
            health = (int)Mathf.Clamp(value, 0, 100);
        }
    }

    [Range(0, 1)]
    public int smokeThreshold = 25;
    public int damageMustExceed = 5;

    private ParticleSystem smoke;
    private ParticleSystem explosion;

    void Start()
    {
        smoke = transform.Find("Smoke").GetComponent<ParticleSystem>();
        explosion = transform.Find("Explosion").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(health <= smokeThreshold && health > 0)
        {
            smoke.Play();
        } else
        {
            smoke.Stop();
        }
        
        if(health <= 0)
        {
            smoke.Stop();
            explosion.Play();
        }
    }
}
