using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private BadPlayer enemyPrefab;
    [SerializeField] private Pleyer player;
    [SerializeField] private FoodSpawner spawner;

    private List<BadPlayer> badPlayers = new List<BadPlayer>();

    private void Start()
    {
        for (int i = 0; i < MainMenu.GameParams.Enemies; i++)
        {
            SpawnNewEnemy();
        }
    }

    public void OnEnemyDie(BadPlayer enemy)
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
