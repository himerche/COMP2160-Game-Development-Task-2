using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] checkpoints;
    private float[] checkpointTimes;

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

    void Start()
    {
        checkpointTimes = new float[checkpoints.Length];
    }

    void Update()
    {
        timeSinceRaceStart = FormatTime(Time.timeSinceLevelLoad);
    }

    void OnDrawGizmos()
    {
        if(checkpoints.Length > 1)
        {
            Gizmos.color = Color.red;

            for(int i = 0; i < checkpoints.Length - 1; i++)
            {
                Vector3 c0 = checkpoints[i].position;
                Vector3 c1 = checkpoints[i + 1].position;
                
                Gizmos.DrawLine(c0, c1);
            }
        }
    }

    public void WinGame()
    {
        UIManager.Instance.DisplayWinPanel();
        Time.timeScale = 0;
    }

    public void LoseGame()
    {
        UIManager.Instance.DisplayLosePanel();
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        UIManager.Instance.HideGameOverPanel();
        Time.timeScale = 1;
    }

    public void UpdateHealth(float health)
    {
        UIManager.Instance.UpdateHealthBar(health);
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - minutes;
        float hundredths = (time - minutes - seconds) * 100;

        return string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }
}
