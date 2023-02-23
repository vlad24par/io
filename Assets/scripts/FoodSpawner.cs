using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] Food foodPrefab;
    [SerializeField] float RandX;
    [SerializeField] Vector3 position;
    [SerializeField] float RandY;
    [SerializeField] BadFood badFoodPrefab;
    [SerializeField] Transform playersTransform;
    [SerializeField] Transform goodFoodParent;
    [SerializeField] Transform badFoodParent;
    
    private float spawnRateGood = 0.1f;
    private float spawnRateBad = 0.2f;

    private List<Food> _goodFood = new List<Food>();
    private List<BadFood> _badFood = new List<BadFood>();

    public int GoodFoodCount => _goodFood.Count;

    private void Start()
    {
        StartCoroutine(SpawnGoodFoodCoroutine());
        StartCoroutine(SpawnBadFoodCoroutine());
    }

    private IEnumerator SpawnGoodFoodCoroutine()
    {
        while (true)
        {
            RandX = GetRandomX();
            RandY = GetRandomY();
            
            position = new Vector3(RandX, RandY);
            var newFood = Instantiate(foodPrefab, position, Quaternion.identity, goodFoodParent);
            _goodFood.Add(newFood);
            newFood.Die += OnGoodFoodDie;

            yield return new WaitForSeconds(spawnRateGood);
        }
    }

    private IEnumerator SpawnBadFoodCoroutine()
    {
        while (true)
        {
            RandX = GetRandomX();
            RandY = GetRandomY();
            
            position = new Vector3(RandX, RandY);
            var badFood = Instantiate(badFoodPrefab, position, Quaternion.identity, badFoodParent);
            _badFood.Add(badFood);
            badFood.Die += OnBadFoodDie;

            yield return new WaitForSeconds(spawnRateBad);
        }
    }

    public Food GetNearestFood(Vector3 pos)
    {
        var nearest = _goodFood[0];
        foreach (var value in _goodFood)
        {
            var currentNearestDistance = Vector3.Distance(pos, nearest.transform.position);
            var nextNearestDistance = Vector3.Distance(pos, value.transform.position);
            
            if (nextNearestDistance < currentNearestDistance)
                nearest = value;
        }

        return nearest;
    }

    private float GetRandomX()
    {
        return Random.Range(-30 + playersTransform.position.x - playersTransform.localScale.x * 2,
            30 + playersTransform.position.x + playersTransform.localScale.x * 2);
    }
    
    private float GetRandomY()
    {
        return Random.Range(30 + playersTransform.position.y + playersTransform.localScale.y*2, 
            -30 + playersTransform.position.y - playersTransform.localScale.y);
    }

    private void OnGoodFoodDie(Food food)
    {
        _goodFood.Remove(food);
        food.Die -= OnGoodFoodDie;
    }
    
    private void OnBadFoodDie(Food food)
    {
        if (food is BadFood badFood)
        {
            _badFood.Remove(badFood);
            badFood.Die -= OnBadFoodDie;
        }
    }
}
