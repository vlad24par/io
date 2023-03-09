using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private int numEnemies;
    [SerializeField] private badPlayer enemyPrefab;
    [SerializeField] private Pleyer player;
    [SerializeField] private FoodSpawner spawner;

    private List<badPlayer> badPlayers = new List<badPlayer>();

    void Start()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            SpawnNewEnemy();
        }
    }

    public void OnEnemyDie(badPlayer enemy)
    {
        enemy.OnDie -= OnEnemyDie;
        badPlayers.Remove(enemy);
        SpawnNewEnemy();
    }

    private void SpawnNewEnemy()
    {
        var position = new Vector3(GetRandomX(), GetRandomY());
        var newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity, transform);
        newEnemy.Init(player, spawner);
        newEnemy.OnDie += OnEnemyDie; 
        badPlayers.Add(newEnemy);
    }

    private float GetRandomX()
    {
        return Random.Range(-30 + player.transform.position.x - player.transform.localScale.x * 2,
            30 + player.transform.position.x + player.transform.localScale.x * 2);
    }
    
    private float GetRandomY()
    {
        return Random.Range(30 + player.transform.position.y + player.transform.localScale.y*2, 
            -30 + player.transform.position.y - player.transform.localScale.y);
    }
}
