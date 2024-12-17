using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public List<GameObject> players;

    public GameObject HomePanel, PlayPanle, GameOverPanel;//, sound_btn, mute_btn;
    string HOMEPANEL = "HOMEPANEL";
    string PLAYPANEL = "PLAYPANEL";
    public string OVERPANEL = "OVERPANEL";

    public Vector3 startPosition;

    private int score = 0;
    public static int highScore = 0;

    public TextMeshProUGUI scoreText, PlayerPanelscoreText;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("Best", 0);
        highScoreText.text = highScore.ToString();
        UpdateScoreText();
        panelmanage(HOMEPANEL);

    }
    public void panelmanage(string arg)
    {
        HomePanel.SetActive(false);
        PlayPanle.SetActive(false);
        GameOverPanel.SetActive(false);

        if (arg == HOMEPANEL)
        {
            HomePanel.SetActive(true);
        }
        else if (arg == PLAYPANEL)
        {
            PlayPanle.SetActive(true);
        }
        else if (arg == OVERPANEL)
        {
            GameOverPanel.SetActive(true);

            ClearGameObjects();
        }
    }

    void ClearGameObjects()
    {
        GameObject[] bottles = GameObject.FindGameObjectsWithTag("Bottles");
        foreach (GameObject bottle in bottles)
        {
            Destroy(bottle);
        }

        GameObject[] bottleSprite = GameObject.FindGameObjectsWithTag("BottleSprite");
        foreach (GameObject bottlesprit in bottleSprite)
        {
            Destroy(bottlesprit);
        }
    }
    public void StartGame()
    {
        panelmanage(PLAYPANEL);
        playersActive(true);
        //Debug.Log("Play");
    }

    public void Home_Btn()
    {
        panelmanage(HOMEPANEL);
        SceneManager.LoadScene(0);
    }

    private void HomeActive(bool v)
    {
        foreach (var item in players)
        {
            item.SetActive(v);
        }

    }
    public void playersActive(bool active)
    {
        foreach (var item in players)
        {
            item.SetActive(active);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = " " + highScore.ToString();
            PlayerPrefs.SetInt("Best", highScore);
        }
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        scoreText.text = "0" + score.ToString();
        PlayerPanelscoreText.text = "0" + score.ToString();
    }
    public void btnRestart()
    {
        ClearGameObjects();

        if (BottleFlip.Instance.bottle != null)
        {
            BottleFlip.Instance.bottle.position = startPosition;
            var rb = BottleFlip.Instance.bottle.GetComponent<Rigidbody2D>();


            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;

            }
        }
        else
        {
            Debug.LogWarning("Bottle has been destroyed. Spawning a new one.");
            //Spawnbottle.instance.SpawnBottle();
        }

        PlayPanle.SetActive(true);
        GameOverPanel.SetActive(false);
        ObstacleSpawner.instance.SpawnObstacle();
        //ObstacleSpawner.instance.SpawnRandomObject();
    }
    public void BackButton()
    {
        panelmanage(HOMEPANEL);
        HomeActive(false);
    }

}
