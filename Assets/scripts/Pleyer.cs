using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Pleyer : MonoBehaviour
{
    public event Action<float> DieEndWin;

    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] GameObject PanelDie;
    [SerializeField] GameObject PanelWin;
    [SerializeField] GameObject spawner;
    [SerializeField] Image FoodImage;
    [SerializeField] Collider2D collider;
    [SerializeField] TextMeshProUGUI rooting_time;

    private float vertical = 1;
    public float weight = 1;
    public float weightgain;
    public float scaleModificator = 10;
    public bool NormalCameraMove;
    public Transform food;

    private bool stop_coroutine = true;
    private float time;

    public float Weight => weight;

    public event Action<float> WeightChange;

    private void Start()
    {
        PanelWin.SetActive(false);
        PanelDie.SetActive(false);
        Time.timeScale = 1;
        food = this.gameObject.transform;
    }
    private void Update()
    {
        Vector3 direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            direction.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y -= 1;
        }

        transform.position = Vector3.Lerp(transform.position, transform.position + direction * speed, 0.1f);
        
        if (weight <= 0 || weight > GameConfig.MaxWeight)
        {
            DieEndWin?.Invoke(weight);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var food = collision.GetComponent<Food>();
        if (food.size <= weight/3)
        {
            NormalCameraMove = true;
            weight = weight + food.size;
            WeightChange?.Invoke(weight);
            FoodImage.fillAmount += food.size /100;
            Destroy(collision.gameObject);
        }
        else
        {
            NormalCameraMove = false;
            food.size = food.size / 2;
            weight = weight + food.size;
            WeightChange?.Invoke(weight);
            FoodImage.fillAmount += food.size / 200;
        }

        var weightInPercent = weight  / GameConfig.MaxWeight;
        var scaleModificator = weightInPercent * GameConfig.MaxScale + 1;
        transform.localScale = Vector3.one * scaleModificator;
    }
    private IEnumerator speed_up_coroutine()
    {
        stop_coroutine = true;
        while (stop_coroutine)
        {
            weight = weight - speed;
            WeightChange?.Invoke(weight);
            speed = speed + 0.2f;
            for (int i = 0; i < time*10; i++)
            {
                rooting_time.text = " Rooting Time : " + (i - time);
                yield return new WaitForSeconds(1f);
            }
            speed = 0.2f;
            stop_coroutine = false;
        }
        rooting_time.text = " Rooting Time : nothing";
        time = 0;
        StopCoroutine("speed_up_coroutine");
    }
    public void speed_up()
    {
        if (weight >= 1)
        {
            time = time + weight;
            StartCoroutine("speed_up_coroutine");
        }
    }
} 
