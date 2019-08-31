using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEvents : BaseGameClass
{
    public GameObject MenuCanvas, HUDCanvas, Generator, PausePanel;

    private void Start()
    {
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        FinalScoreText = GameObject.Find("FinalScoreText").GetComponent<Text>();
        HighScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        CountDownText = GameObject.Find("CountDownText").GetComponent<Text>();
        Objects = GameObject.Find("Objects");
        LostPanel = GameObject.Find("Lost");
        Player = GameObject.Find("Player");
        Player.SetActive(false);
        LostPanel.SetActive(false);
        HUDCanvas.SetActive(false);
        CountDownText.gameObject.SetActive(false);
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        Play = false;
    }

    public void ExitClick(BaseEventData data)
    {
        Application.Quit();
    }

    public void StartClick(BaseEventData data)
    {
        MenuCanvas.SetActive(false);
        HUDCanvas.SetActive(true);
        Player.SetActive(true);
        Generator.SetActive(true);
        Score = 0;
        Player.GetComponent<Player>().Prepare();
        StartCoroutine("CountDown");
    }

    public void PauseClick(BaseEventData data)
    {
        if (!Play)
            return;
        Time.timeScale = 0;
        Play = false;
        PausePanel.SetActive(true);
    }

    public void PlayAgainClick()
    {
        Score = 0;
        LostPanel.SetActive(false);
        Player.GetComponent<Player>().Prepare();
        StartCoroutine("CountDown");
    }

    public void ContinueClick()
    {
        PausePanel.SetActive(false);
        StartCoroutine("CountDown");
    }

    public void ExitToMenuClick()
    {
        Time.timeScale = 1;
        DestroyObjects();
        PausePanel.SetActive(false);
        MenuCanvas.SetActive(true);
        HUDCanvas.SetActive(false);
        LostPanel.SetActive(false);
        Player.SetActive(false);
        Generator.SetActive(false);
    }
}
