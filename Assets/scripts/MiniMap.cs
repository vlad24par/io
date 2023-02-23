using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] Pleyer pleyer;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -pleyer.Weight * 10);
    }
}
