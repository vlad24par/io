using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMove : MonoBehaviour
{
    [SerializeField] float camera_scale;
    [SerializeField] Pleyer pleyer;
    [SerializeField] float camera_modifier;

    private Vector3 newPosition;

    private void Start()
    {
        camera_modifier = 2;
    }
    private void Update()
    {
        newPosition.z = (-pleyer.transform.localScale.z - camera_scale - (pleyer.Weight - pleyer.food.position.z) / camera_modifier) / camera_modifier;
        newPosition.z = Mathf.Round(newPosition.z);
        newPosition.x = pleyer.transform.position.x;
        newPosition.y = pleyer.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
    }
}
