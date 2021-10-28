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
            if(value < 0)
            {
                value = 0;
            } else if(value > 100)
            {
                value = 100;
            }

            health = value;
        }
    }

    [Range(0, 1)]
    public int smokeThreshold = 25;
    public int damageMustExceed = 5;
}
