using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarForce : MonoBehaviour
{
    private Image BarImage;

    private bool CanStopBar;

    private float TimeCount;
    private int ForceSelected;

    public static event Action<double> OnForceSelected;

    [SerializeField]
    private GameObject BarForceObject;

    private void OnEnable()
    {
        BarImage = GetComponent<Image>();
        CanStopBar = false;
    }

    private void Start()
    {
        TimeCount = 0;
    }

    private void Update()
    {
        TimeCount += Time.deltaTime;

        if(TimeCount >= 0.01f && !CanStopBar)
        {
            if (BarImage.fillAmount >= 1)
                BarImage.fillAmount = 0;

            BarImage.fillAmount += 0.025f;

            TimeCount -= TimeCount;
        }
    }

    private int ForcePercentage()
    {
        ForceSelected = (int)Math.Round(BarImage.fillAmount * 100, 0);
        return ForceSelected;
    }
    public void OnButtonPushForcePressed()
    {
        CanStopBar = true;
        OnForceSelected(ForcePercentage());
        StartCoroutine(WaitToDestroyBarForce(1.5f));
    }

    IEnumerator WaitToDestroyBarForce(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(BarForceObject);
    }
}
