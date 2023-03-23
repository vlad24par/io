using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMove : MonoBehaviour
{
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;
    [SerializeField] Pleyer pleyer;
    [SerializeField] AnimationCurve curve;
    [SerializeField] ProgressUi progressUi;

    private Vector3 newPosition;

    private void Update()
    {
        // newPosition.z = (-pleyer.transform.localScale.z - camera_scale - (pleyer.Weight - pleyer.food.position.z) / 
        //     camera_modifier) / camera_modifier;

        var curveValue = curve.Evaluate(progressUi.WeightPercent);
        newPosition.z = (maxZoom - minZoom) * curveValue + minZoom;
            
        newPosition.z = Mathf.Round(newPosition.z);
        newPosition.x = pleyer.transform.position.x;
        newPosition.y = pleyer.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
    }
}
