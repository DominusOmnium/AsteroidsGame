using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameClass : MonoBehaviour
{
    public enum Bonuses { Shield, Heal, DoubleDamage, ThroughBullets }
    public enum MooveType { SimpleForward, ZigZag, ChasePlayer }

    public static GameObject Objects;
    public static GameObject LostPanel;
    public static GameObject Player;
    public static Text FinalScoreText;
    public static Text ScoreText;
    public static Text HighScoreText;
    public static Text CountDownText;
    public static bool Play;

    private static int score = 0;
    private static int highScore = 0;

    public int Score {
        get { return score; }
        set
        {
            score = value;
            ScoreText.text = Convert.ToString(value);
        }
    }

    public int HighScore
    {
        get { return highScore; }
        set
        {
            highScore = value;
            PlayerPrefs.SetInt("HighScore", value);
            HighScoreText.text = "Лучший результат: " + value;
        }
    }


    protected void DestroyObjects()
    {
        for (int i = 0; i < Objects.transform.childCount; i++)
        {
            Destroy(Objects.transform.GetChild(i).gameObject);
        }
    }

    protected void PlayerKilled()
    {
        Play = false;
        LostPanel.SetActive(true);
        FinalScoreText.text = "Результат: " + Score;
        if (Score > HighScore)
            HighScore = Score;
        Time.timeScale = 0;
        DestroyObjects();
    }

    public IEnumerator CountDown()
    {
        Time.timeScale = 1;
        CountDownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            CountDownText.text = "" + i;
            yield return new WaitForSeconds(1f);
        }
        CountDownText.gameObject.SetActive(false);
        Play = true;
    }
}
