using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressUi : MonoBehaviour
{
    [SerializeField] Pleyer player;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] Image image;

    private void OnEnable()
    {
        player.WeightChange += OnWeightChange;
    }

    private void OnDisable()
    {
        player.WeightChange -= OnWeightChange;
    }

    private void OnWeightChange(float weight)
    {
        textMeshPro.text = weight.ToString();
        var weightInPercent = weight  / GameConfig.MaxWeight;
        image.fillAmount = Mathf.Clamp01(weightInPercent);
    }
}
