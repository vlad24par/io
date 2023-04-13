using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BadPlayer : MonoBehaviour
{
    [SerializeField] float speedMin;
    [SerializeField] float speedMax;
    
    private FoodSpawner spawner;
    private Pleyer player;

    public float weight = 1;

    private float speed;
    private Food target;
    private bool isOnTarget = false;
    private IEnumerator findFoodCorutine;
    private List<GameObject> triggeredObjects = new List<GameObject>();
    private bool playerInCollider;

    public event Action<BadPlayer> OnDie;

    public void Init(Pleyer player, FoodSpawner spawner)
    {
        this.player = player;
        this.spawner = spawner;
        speed = Random.Range(speedMin, speedMax);
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
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 
                    speed * Time.deltaTime);
            }
            
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food") || collision.CompareTag("Badfood"))
        {
            playerInCollider = false;
            triggeredObjects.Add(collision.gameObject);
            Eat(collision.GetComponent<Food>());
        }
        else if (collision.CompareTag("badPlayer"))
        {
            var badPlayer = collision.gameObject.GetComponent<BadPlayer>();
            if (badPlayer.weight < weight)
            {
                triggeredObjects.Add(collision.gameObject);
                playerInCollider = false;
                EatBadPlayer(badPlayer);
            }
            else
            {
                return;
            }
        }
        else if (collision.CompareTag("Player"))
        {
            if (player.Weight <= weight / 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
                    speed * Time.deltaTime);
                triggeredObjects.Add(collision.gameObject);
                playerInCollider = true;
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

    private void EatBadPlayer(BadPlayer badPlayer)
    {
        weight += badPlayer.weight / 4;
        badPlayer.Die();
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
        OnDie?.Invoke(this);
        Destroy(gameObject);
    }

    private void Eat(Food food)
    {
        weight += food.size;
        if (weight < 0)
        {
            Die();
        }

        if (food != null)
            Destroy(food.gameObject);
        isOnTarget = false;

        var weightInPercent = weight / MainMenu.GameParams.MaxScore;
        var scaleModificator = weightInPercent * MainMenu.GameParams.MaxScale + 1;
        transform.localScale = Vector3.one * scaleModificator;
    }

    private void EatPlayer()
    {
        weight += player._weight;
        player._weight = -1;
    }

}
