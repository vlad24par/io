using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UiManager : MonoBehaviour 
{
    [SerializeField] GameObject pause;
    [SerializeField] GameObject start;
    [SerializeField] GameObject ReplayButton;
    [SerializeField] GameObject ExitButton;
    [SerializeField] GameObject Die;
    [SerializeField] GameObject WIN;
    [SerializeField] private Pleyer _eventsSample;

    private void Start()
    {

    }
    private void OnEnable()
    {
        _eventsSample.DieEndWin += WinOrDie;
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Exit()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Pause()
    {
        ReplayButton.SetActive(true);
        ExitButton.SetActive(true);
        start.SetActive(true);
        pause.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void SetStart()
    {
        ExitButton.SetActive(false);
        ReplayButton.SetActive(false);
        start.SetActive(false);
        pause.SetActive(true);
        Time.timeScale = 1;
    }
    public void WinOrDie(float weighe) 
    {
        if (weighe > 200)
        {
            WIN.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Die.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
