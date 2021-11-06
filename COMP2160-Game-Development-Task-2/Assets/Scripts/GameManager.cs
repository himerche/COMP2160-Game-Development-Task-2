using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] checkpoints;
    public Light[] checkpointIndicators;
    private float[] checkpointTimes;

    private int activeCheckpoint = -1;

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
        checkpointIndicators = new Light[checkpoints.Length];

        for(int i = 0; i < checkpointIndicators.Length; i++)
        {
            checkpointIndicators[i] = checkpoints[i].GetComponent<Light>();
            checkpointIndicators[i].enabled = false;
        }

        ActivateNextCheckpoint();
    }

    void Update()
    {
        timeSinceRaceStart = TimeExtensions.FormatTime(Time.timeSinceLevelLoad);
    }

    /* 
     * Draw lines between the checkpoints
     */
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
        UIManager.Instance.DisplayWinPanel(checkpointTimes);
        Time.timeScale = 0;
    }

    public void LoseGame()
    {
        UIManager.Instance.DisplayLosePanel(checkpointTimes);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        UIManager.Instance.HideGameOverPanel();
        Time.timeScale = 1;
    }

    public void UpdateHealth(float health)
    {
        UIManager.Instance.UpdateHealthBar(health);
    }

    public void ActivateNextCheckpoint()
    {
        if(activeCheckpoint > 0) {
            checkpointIndicators[activeCheckpoint].enabled = false;
            checkpoints[activeCheckpoint].tag = "Inactive";
        }

        activeCheckpoint = activeCheckpoint + 1;

        if(activeCheckpoint == checkpoints.Length) {
            WinGame();
        }
        else
        {
            checkpointIndicators[activeCheckpoint].enabled = true;
            checkpoints[activeCheckpoint].tag = "Active";
        }
    }
}
