using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DotweenTest : MonoBehaviour
{
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;
    [SerializeField] private Ease _ease;


    void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f);
            transform.DOMove(target1.transform.position, 1f).SetEase(_ease);
            yield return new WaitForSeconds(1);
            
            transform.DOScale(Vector3.one, 0.3f);
            transform.DOMove(target2.transform.position, 1f).SetEase(_ease);
            yield return new WaitForSeconds(1);
        }
    }
}
