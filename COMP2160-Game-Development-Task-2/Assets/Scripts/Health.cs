using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float health = 1.0f;

    [Range(0, 1)]
    public float smokeThreshold = 0.25f;
    public float healthRestoredAtCheckpoint = 0.1f;
    public float damageMustExceed = 0.05f;
}
