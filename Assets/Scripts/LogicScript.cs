using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private int playerScore;
    [SerializeField] private Text scoreText;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject bird;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip dingSfx;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private float dingPitch = 2.5f;
    [SerializeField] private float deathPitch = 1.0f;

    private bool IsBirdAlive = true;
    private int HighScore;

    private void Start()
    {
        BirdScript birdScript = bird.GetComponent<BirdScript>();
        birdScript.OnDead += GameOver;
        birdScript.OnPassedPipe += AddScore;

        audioSource = GetComponent<AudioSource>();

        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = HighScore.ToString();
    }

    private void AddScore(object sender, BirdScript.OnPassedPipeEventArgs e)
    {
        if (IsBirdAlive)
        {
            playerScore += e.scoreToAdd;
            scoreText.text = playerScore.ToString();
            audioSource.pitch = dingPitch;
            audioSource.PlayOneShot(dingSfx);

            if (playerScore > HighScore)
            {
                HighScore = playerScore;
                HighScoreText.text = HighScore.ToString();
            }
        }
    }

    private void GameOver(object sender, System.EventArgs e)
    {
        if (IsBirdAlive)
        {
            audioSource.pitch = deathPitch;
            audioSource.PlayOneShot(deathSfx);

            if(playerScore >= HighScore)
            {
                PlayerPrefs.SetInt("HighScore", HighScore);
                PlayerPrefs.Save();
            }
        }

        IsBirdAlive = false;
        gameOverScreen.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
