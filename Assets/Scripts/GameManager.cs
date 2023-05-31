using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //const
    private static readonly string KEY_HIGHEST_SCORE = "HighestScore";

    public bool isGameOver { get; private set; }
    [Header("Audio")]
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource gameOverSfx;
    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private int highestScore;

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //score
        score = 0;
        highestScore = PlayerPrefs.GetInt(KEY_HIGHEST_SCORE);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            //increment
            score += Time.deltaTime;
            //update highest score
            if(GetScore() > GetHighestScore())
            {
                highestScore = GetScore();
            }
        }        
    }

    public int GetScore()
    {
        return (int) Mathf.Floor(score);
    }
    public int GetHighestScore()
    {
        return highestScore;
    }


    public void EndGame()
    {
        if (isGameOver) return;

        //set flag
        isGameOver = true;
        //stop music
        musicPlayer.Stop();
        //play sfx
        gameOverSfx.Play();

        //Save highest score
        PlayerPrefs.SetInt(KEY_HIGHEST_SCORE, GetHighestScore());
    }
}
