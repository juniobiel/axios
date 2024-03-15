using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverBootHUD : MonoBehaviour
{
    //TODO: Remover esses textos que são de teste
    private const string SPEED_TEXT_PREFIX = "SPEED";
    private const string HOVER_TEXT_PREFIX = "HOVER";
    private string HOVER_ON_TEXT = "<color=#00CC00>ON</color>";
    private string HOVER_OFF_TEXT = "<color=#CC0000>OFF</color>";

    [SerializeField] private TMP_Text SpeedText;
    [SerializeField] private TMP_Text HovertText;
    [SerializeField] private Image HoverBootStaminaBar;

    [SerializeField] private GameObject HoverBootBar;

    private AxiosController Axios;

    private void OnEnable()
    {
        Axios = GameObject.FindGameObjectWithTag("Player").GetComponent<AxiosController>();
        MinotaurController.OnAxiosLaunched += MinotaurController_OnAxiosLaunched;
    }

    private void OnDisable()
    {
        MinotaurController.OnAxiosLaunched -= MinotaurController_OnAxiosLaunched;
    }

    private void MinotaurController_OnAxiosLaunched(bool obj)
    {
        HoverBootBar.SetActive(true);
    }

    private void Update()
    {
        SpeedText.text = $"{Axios.VelocityBuffer.y}:{SPEED_TEXT_PREFIX}";
        HovertText.text = $"{(Axios.HoverBootPressed ? HOVER_ON_TEXT : HOVER_OFF_TEXT)}:{HOVER_TEXT_PREFIX}";
        HoverBootStaminaBar.fillAmount = Axios.HoverBootStamina;
    }
}
