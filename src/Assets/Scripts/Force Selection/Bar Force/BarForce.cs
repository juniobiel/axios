using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarForce : MonoBehaviour
{
    [SerializeField]
    private TMP_Text PercentageText;

    private Image BarImage;

    private bool CanForceIncrease;
    private bool CanStopBar;

    private float TimeCount;

    private void OnEnable()
    {
        BarImage = GetComponent<Image>();
        CanForceIncrease = true;
        CanStopBar = false;
    }

    private void Start()
    {
        TimeCount = 0;
    }

    private void FixedUpdate()
    {
        TimeCount += Time.deltaTime;

        if(TimeCount >= 0.01f && !CanStopBar)
        {
            if (BarImage.fillAmount >= 1)
                CanForceIncrease = false;

            if (BarImage.fillAmount <= 0)
                CanForceIncrease = true;

            if(CanForceIncrease)
                BarImage.fillAmount += 0.01f;
            else
                BarImage.fillAmount -= 0.01f;

            TimeCount -= TimeCount;
        }

        PercentageText.text = ForcePercentage();
    }

    private string ForcePercentage()
    {

        return $"{ Math.Round(BarImage.fillAmount * 100, 0) }%";
    }


    public void OnButtonPushForcePressed()
    {
        CanStopBar = true;
    }
}
