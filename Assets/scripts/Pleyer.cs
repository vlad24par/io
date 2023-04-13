using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Pleyer : MonoBehaviour
{
    public event Action<float> DieEndWin;

    [SerializeField] float _speed;
    [SerializeField] GameObject _panel_Die;
    [SerializeField] GameObject _panel_Win;
    [SerializeField] Image _food_Image;
    [SerializeField] TextMeshProUGUI _rooting_time;

    private float _vertical = 1;
    public float _weight = 1;

    private bool _stop_coroutine = true;
    private float _time;

    public float Weight => _weight;

    public event Action<float> Weight_Change;

    private void Start()
    {
        _panel_Win.SetActive(false);
        _panel_Die.SetActive(false);
        Time.timeScale = 1;
        _speed = MainMenu.GameParams.Speed;
    }

    public void Move(Vector3 direction)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + direction * _speed, 0.1f);
        if (_weight <= 0 || _weight > MainMenu.GameParams.MaxScore)
        {
            DieEndWin?.Invoke(_weight);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var food = collision.GetComponent<Food>();
        if (collision.CompareTag("badPlayer") )
        {
            var enemy = collision.GetComponent<BadPlayer>();
            
            if(enemy.weight >= _weight/3)
            {
                DieEndWin?.Invoke(_weight);
            }
            else
            {
                enemy.Die();
                AddWeight(enemy.weight / 3);
            }
            return;
        }
        if (food.size <= _weight/3)
        {
            AddWeight(food.size);
            Destroy(collision.gameObject);
        }
        else
        {
            food.size = food.size / 2;
            AddWeight(food.size);
        }

        var weightInPercent = _weight  / MainMenu.GameParams.MaxScore;
        var scaleModificator = weightInPercent * MainMenu.GameParams.MaxScale + 1;
        transform.localScale = Vector3.one * scaleModificator;
    }
    private IEnumerator speed_up_coroutine()
    {
        _stop_coroutine = true;
        while (_stop_coroutine)
        {
            _weight = _weight - _speed;
            Weight_Change?.Invoke(_weight);
            _speed = _speed + 0.2f;
            for (int i = 0; i < _time*10; i++)
            {
                _rooting_time.text = " Rooting Time : " + (i - _time);
                yield return new WaitForSeconds(1f);
            }
            _speed = 0.2f;
            _stop_coroutine = false;
        }
        _rooting_time.text = " Rooting Time : nothing";
        _time = 0;
        StopCoroutine("speed_up_coroutine");
    }
    public void speed_up()
    {
        if (_weight >= 1)
        {
            _time = _time + _weight;
            StartCoroutine("speed_up_coroutine");
        }
    }

    private void AddWeight(float addAmount)
    {
        _weight += addAmount;
        Weight_Change?.Invoke(_weight);
        var weightInPercent = _weight  / MainMenu.GameParams.MaxScore;
        _food_Image.fillAmount = weightInPercent;
    }
} 
