using System.Collections;
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
    string OVERPANEL = "OVERPANEL";

    /*private int score = 0;
    public static int highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;*/

    private void Awake()
    {

        Instance = this;
    }
    void Start()
    {
        /*highScore = PlayerPrefs.GetInt("Best", 0);
        highScoreText.text = "Best " + highScore.ToString();
        UpdateScoreText();*/
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
        }
    }
    public void StartGame()
    {
        panelmanage(PLAYPANEL);
        playersActive(true);
        Debug.Log("Play");
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

   /* public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "Best " + highScore.ToString();
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
        scoreText.text = "" + score.ToString();
    }*/
    public void btnContionue()
    {
        SceneManager.LoadScene(0);
    }
    public void BackButton()
    {
        panelmanage(HOMEPANEL);
        HomeActive(false);
    }

}
