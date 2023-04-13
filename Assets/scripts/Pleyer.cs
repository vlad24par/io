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
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] GameObject _panel_Die;
    [SerializeField] GameObject _panel_Win;
    [SerializeField] GameObject _spawner;
    [SerializeField] Image _food_Image;
    [SerializeField] Collider2D _collider;
    [SerializeField] TextMeshProUGUI _rooting_time;

    private float _vertical = 1;
    public float _weight = 1;
    public float _weightgain;
    public float _scale_modificator = 10;
    public bool _normal_camera_move;
    public Transform _food;

    private bool _stop_coroutine = true;
    private float _time;

    public float Weight => _weight;

    public event Action<float> Weight_Change;

    private void Start()
    {
        _panel_Win.SetActive(false);
        _panel_Die.SetActive(false);
        Time.timeScale = 1;
        _food = this.gameObject.transform;
    }

    public void Move(Vector3 direction)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + direction * _speed, 0.1f);
        if (_weight <= 0 || _weight > GameConfig.MaxWeight)
        {
            DieEndWin?.Invoke(_weight);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var food = collision.GetComponent<Food>();
        if (collision.CompareTag("badPlayer") )
        {
            var enemy = collision.GetComponent<badPlayer>();
            
            if(enemy.Bad_weight >= _weight/3)
            {
                DieEndWin?.Invoke(_weight);
            }
            else
            {
                enemy.Die();
                AddWeight(enemy.Bad_weight / 3);
            }
            return;
        }
        if (food.size <= _weight/3)
        {
            _normal_camera_move = true;
            AddWeight(food.size);
            Destroy(collision.gameObject);
        }
        else
        {
            _normal_camera_move = false;
            food.size = food.size / 2;
            AddWeight(food.size);
        }

        var weightInPercent = _weight  / GameConfig.MaxWeight;
        var scaleModificator = weightInPercent * GameConfig.MaxScale + 1;
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
        var weightInPercent = _weight  / GameConfig.MaxWeight;
        _food_Image.fillAmount = weightInPercent;
    }
} 
