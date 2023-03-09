using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class badPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] FoodSpawner spawner;
    [SerializeField] Pleyer Player;

    public float Bad_weight = 1;

    private Food target;
    private bool isOnTarget = false;
    private IEnumerator findFoodCorutine;
    private List<GameObject> triggeredObjects = new List<GameObject>();
    private bool player_in_collider;


    private void Start()
    {
        findFoodCorutine = FindFood();
        StartCoroutine(findFoodCorutine);
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
                    Eat(target);
                }

                isOnTarget = false;
            }

            if (!isOnTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food") || collision.CompareTag("Badfood"))
        {
            player_in_collider = false;
            triggeredObjects.Add(collision.gameObject);
            Eat(collision.GetComponent<Food>());
        }
        else if (collision.CompareTag("Player"))
        {
            if (Player.Weight <= Bad_weight / 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                player_in_collider = true;
                EatPlayer();
            }
            else
            {
                return;
            }
        }

        if (collision.gameObject == target.gameObject)
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
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void Eat(Food food)
    {
        Bad_weight += food.size;

        if (food != null)
            Destroy(food.gameObject);
        isOnTarget = false;

        var weightInPercent = Bad_weight / GameConfig.MaxWeight;
        var scaleModificator = weightInPercent * GameConfig.MaxScale + 1;
        transform.localScale = Vector3.one * scaleModificator;
    }

    private void EatPlayer()
    {
        Bad_weight += Player.weight;
        Player.weight = -1;
    }
}
