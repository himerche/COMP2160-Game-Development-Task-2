using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform UI;
    private RectTransform healthBar;
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
    }

    void Update()
    {
        timer.text = GameManager.Instance.TimeSinceRaceStart;
    }

    public void UpdateHealthBar(float health)
    {
        float xDistance = healthBarWidth - (health * healthBarWidth);
        float yDistance = healthBar.rect.height;

        Vector2 healthVector = new Vector2(-xDistance, yDistance);

        healthBar.sizeDelta = healthVector;
    }
}
