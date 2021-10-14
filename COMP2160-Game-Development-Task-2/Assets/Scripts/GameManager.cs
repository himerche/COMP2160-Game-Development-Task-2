using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string timeSinceRaceStart;
    public string TimeSinceRaceStart
    {
        get
        {
            return timeSinceRaceStart;
        }
    }

    private GameObject player;

    static private GameManager instance;
    static public GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no Game Manager in the scene.");
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        timeSinceRaceStart = FormatTime(Time.timeSinceLevelLoad);
    }

    public void UpdateHealth(float health)
    {
        UIManager.Instance.UpdateHealthBar(health);
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60000;
        int seconds = (int)time / 1000 - 60 * minutes;
        int milliseconds = (int)time - minutes * 60000 - 1000 * seconds;

        return string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
