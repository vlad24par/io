
using UnityEngine;

[CreateAssetMenu(fileName = "Game Params", menuName = "ScriptableObjects/GameParams")]
public class GameParams : ScriptableObject
{
    public float Speed = 10;
    public int MaxScore = 10000;
    public int Enemies = 2;
    public int MaxScale = 60;
}