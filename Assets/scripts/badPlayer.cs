using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class badPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Collider2D collider;
    [SerializeField] float maxDistans;
    [SerializeField] FoodSpawner spawner;
    [SerializeField] GameObject Player;

    public float Bad_weight = 1;
    public float weightgain;
    public float scaleModificator = 10;
    public bool NormalCameraMove;
    public Transform food;
    public LayerMask layer;

    private Food target;
    private bool isOnTarget = false;
    private IEnumerator findFoodCorutine;
    private List<GameObject> triggeredObjects = new List<GameObject>();


    private void Start()
    {
        Time.timeScale = 1;
        food = gameObject.transform;
        
        findFoodCorutine = FindFood();
        StartCoroutine(findFoodCorutine);
    }
    private void Update()
    {
        // Vector3 direction = Vector3.zero;
        //
        // if (Physics2D.Raycast(transform.position,Vector2.up, maxDistans))
        // {
        //     direction.y += 1;
        // }
        // if (Physics2D.Raycast(transform.position, Vector2.right, maxDistans)) 
        // {
        //     direction.x -= 1;
        // }
        // if (Physics2D.Raycast(transform.position, Vector2.left, maxDistans))
        // {
        //     direction.x += 1;
        // }
        // if (Physics2D.Raycast(transform.position, Vector2.down, maxDistans))
        // {
        //     direction.y -= 1;
        // }
        //
        // transform.position = Vector3.Lerp(transform.position, transform.position + direction * speed, 0.1f);
    }

    private IEnumerator FindFood()
    {
        while (spawner.GoodFoodCount < 10)
        {
            yield return null;
        }
        
        while (true)
        {
            if (target == null)
            {
                target = spawner.GetNearestFood(transform.position);
                if (triggeredObjects.Contains(target.gameObject))
                {
                    triggeredObjects.Remove(target.gameObject);
                    Eat();
                }
            }

            if (!isOnTarget)
            {
                transform.position = Vector3.Lerp(transform.position, target.transform.position, 1f * Time.deltaTime);
            }
            
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            triggeredObjects.Add(collision.gameObject);
            Eat();
        }
        else if (collision.CompareTag("badfood"))
        {
            return;
        }
        else if (collision.CompareTag("Player"))
        {
            //if (Player.weight <= Bad_weight/3)
            {
                Eat();
            }
            //else
            {
                ran();
            }
        }
        isOnTarget = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggeredObjects.Contains(collision.gameObject))
        {
            triggeredObjects.Remove(collision.gameObject);
        }
        if (target.gameObject == null)
        {
            isOnTarget = false;
            return;
        }
        if (collision.gameObject == target.gameObject)
        {
            isOnTarget = false;
        }
    }

    private void Eat()
    {
        Bad_weight += target.size;
        Destroy(target.gameObject);
        isOnTarget = false;

        var weightInPercent = Bad_weight / GameConfig.MaxWeight;
        var scaleModificator = weightInPercent * GameConfig.MaxScale + 1;
        transform.localScale = Vector3.one * scaleModificator;
    }
    private void ran()
    {
        
    }
}
