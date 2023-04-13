using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UiManager : MonoBehaviour 
{
    [SerializeField] GameObject _pause;
    [SerializeField] GameObject _start;
    [SerializeField] GameObject _Replay_Button;
    [SerializeField] GameObject _Exit_Button;
    [SerializeField] GameObject _Die;
    [SerializeField] GameObject _Win;
    [SerializeField] public Pleyer _pleyer;
    [SerializeField] GameObject _SpeedUp;
    [SerializeField] GameObject _PanelLNH;
    [SerializeField] GameObject _Leicht;
    [SerializeField] GameObject _Normal;
    [SerializeField] GameObject _Hard;

    private void Start()
    {
        _PanelLNH.SetActive(false);
    }
    private void OnEnable()
    {
        _pleyer.DieEndWin += WinOrDie;
    }

    private void OnDisable()
    {
        _pleyer.DieEndWin -= WinOrDie;
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void pley()
    {
        _PanelLNH.SetActive(true);

    }
    public void leicht()
    {
        SceneManager.LoadScene(1);
    }
    public void normal()
    {
        SceneManager.LoadScene(1);
    }
    public void hard()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        if (_pleyer.Weight >= 100)
        {
            _pleyer._weight = _pleyer._weight - 99;
            _Die.SetActive(false);
            _Win.SetActive(false);
            _pause.SetActive(false);
            _SpeedUp.SetActive(false);
            Time.timeScale = 1;
        }
    }


    public void Pause()
    {
        _Replay_Button.SetActive(true);
        _Exit_Button.SetActive(true);
        _start.SetActive(true);
        _pause.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void SetStart()
    {
        _Exit_Button.SetActive(false);
        _Replay_Button.SetActive(false);
        _start.SetActive(false);
        _pause.SetActive(true);
        Time.timeScale = 1;
    }
    
    public void WinOrDie(float weight) 
    {
        if (weight > 200)
        {
            _Win.SetActive(true);
            Time.timeScale = 0;
            _SpeedUp.SetActive(false);

        }
        else
        {
            _Die.SetActive(true);
            Time.timeScale = 0;
            _SpeedUp.SetActive(false);

        }
    }
}
