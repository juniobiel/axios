using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarForce : MonoBehaviour
{
    [SerializeField]
    private TMP_Text PercentageText;

    private Image BarImage;

    private bool CanStopBar;

    private float TimeCount;
    private double ForceSelected;

    public static event Action<double> OnForceSelected;

    private void OnEnable()
    {
        BarImage = GetComponent<Image>();
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
                BarImage.fillAmount = 0;

            BarImage.fillAmount += 0.025f;

            TimeCount -= TimeCount;
        }

        PercentageText.text = ForcePercentage();
    }

    private string ForcePercentage()
    {
        ForceSelected = Math.Round(BarImage.fillAmount * 100, 0);

        return $"{ ForceSelected }%";
    }


    public void OnButtonPushForcePressed()
    {
        CanStopBar = true;
        OnForceSelected(ForceSelected);
    }
}
