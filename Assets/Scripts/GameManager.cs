using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private static int score;
    [SerializeField]
    private static int highScore;
    static GameManager gM;
    public int showScore, showHighScore;

    public GameObject gameOverScreen, winScreen;
    public TextMeshProUGUI scoreTxt, highScoreTxt;


    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        showHighScore = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        showHighScore = PlayerPrefs.GetInt("HighScore", 0);
        gM = this;
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);

        SetUi();

    }

    // Update is called once per frame
    void Update()
    {
        /*showHighScore = PlayerPrefs.GetInt("HighScore", 0);
        showScore = score;*/
    }
    public void SetUi()
    {
        scoreTxt.text = "Score: " + score;
        highScoreTxt.text = "High Score: " + highScore;
    }
    public static void GameOver()
    {

        PlayerController player = FindObjectOfType<PlayerController>();
        player.enabled = false;

        if (GameManager.gM.gameOverScreen != null)
            GameManager.gM.gameOverScreen.SetActive(true);

        Debug.Log("Game Over");
    }

    public static void Win()
    {


        PlayerController player = FindObjectOfType<PlayerController>();
        PlayerPrefs.Save();
        player.enabled = false;

        if (GameManager.gM.winScreen != null)
            GameManager.gM.winScreen.SetActive(true);

        Debug.Log("You Win");
    }

    public static void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        GameManager.gM.showScore = score;
        if (score > highScore)
        {
            GameManager.gM.showHighScore = score;

            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }

        GameManager.gM.SetUi();

    }
}
