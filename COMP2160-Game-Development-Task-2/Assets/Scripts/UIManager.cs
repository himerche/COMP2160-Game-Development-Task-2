using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
        healthBar = transform.Find("UI/Canvas/Panel/Health Bar").GetComponent<RectTransform>();
        healthBarWidth = Mathf.Abs(healthBar.rect.width);

        timer = transform.Find("UI/Canvas/Timer").GetComponent<Text>();

        gameOverPanel = transform.Find("UI/Canvas/Game Over").gameObject;
        message = transform.Find("UI/Canvas/Game Over/Message").GetComponent<Text>();
        checkpoints = transform.Find("UI/Canvas/Game Over/Checkpoints").GetComponent<Text>();

        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        timer.text = GameManager.Instance.TimeSinceRaceStart;
    }

    public void DisplayWinPanel(float[] checkpointTimes)
    {
        gameOverPanel.SetActive(true);
        message.text = "You win!";
        
        checkpoints.text = FormatCheckpoints(checkpointTimes);
    }

    public void DisplayLosePanel(float[] checkpointTimes)
    {
        gameOverPanel.SetActive(true);
        message.text = "You died!";
        
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
                string time = TimeExtensions.FormatTime(checkpointTimes[i]);
                checkpointText += $"Checkpoint {i + 1}: {time}";
                checkpointText += "\n";
            }
        }

        return checkpointText;
    }
}
