using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform UI;
    private RectTransform healthBar;
    private GameObject gameOverPanel;
    private Text message;
    private Text checkpoints;

    private Text timer;
    private float healthBarWidth;

    static private UIManager instance;
    static public UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no UI Manager in the scene.");
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
        healthBar = UI.Find("Canvas/Panel/Health Bar").GetComponent<RectTransform>();
        healthBarWidth = Mathf.Abs(healthBar.rect.width);

        timer = UI.Find("Canvas/Timer").GetComponent<Text>();

        gameOverPanel = UI.Find("Canvas/Game Over").gameObject;
        message = UI.Find("Canvas/Game Over/Message").GetComponent<Text>();
        checkpoints = UI.Find("Canvas/Game Over/Checkpoints").GetComponent<Text>();
    }

    void Update()
    {
        timer.text = GameManager.Instance.TimeSinceRaceStart;
    }

    public void DisplayWinPanel()
    {
        gameOverPanel.SetActive(true);
        message.text = "You win!";

        float[] checkpointTimes = new float[5] {1.0f, 0, 3.0f, 4.0f, 5.0f};
        
        checkpoints.text = FormatCheckpoints(checkpointTimes);
    }

    public void DisplayLosePanel()
    {
        gameOverPanel.SetActive(true);
        message.text = "You died!";

        float[] checkpointTimes = new float[5] {1.0f, 0, 3.0f, 4.0f, 5.0f};
        
        checkpoints.text = FormatCheckpoints(checkpointTimes);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void UpdateHealthBar(float health)
    {
        float xDistance = healthBarWidth - (health * healthBarWidth);
        float yDistance = healthBar.rect.height;

        Vector2 healthVector = new Vector2(-xDistance, yDistance);

        healthBar.sizeDelta = healthVector;
    }

    public string FormatCheckpoints(float[] checkpointTimes)
    {
        string checkpointText = "";

        for(int i = 0; i < checkpointTimes.Length; i++)
        {
            if(checkpointTimes[i] == 0)
            {
                checkpointText += $"Checkpoint {i + 1}: Incomplete";;
                checkpointText += "\n";
            }
            else
            {
                string time = FormatTime(checkpointTimes[i]);
                checkpointText += $"Checkpoint {i + 1}: {time}";
                checkpointText += "\n";
            }
        }

        return checkpointText;
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - minutes;
        float hundredths = (time - minutes - seconds) * 100;

        return string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }
}
