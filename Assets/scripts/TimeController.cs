using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float timeScale = 1;
    
    void Start()
    {
        Time.timeScale = timeScale;
    }
}
