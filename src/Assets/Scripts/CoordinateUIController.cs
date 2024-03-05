using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CoordinateUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text heightText;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null) Debug.Log("CoordinateUIController.target NOT FOUND");
    }
    
    private void Update()
    {
        if (target == null) return; // fail check
        scoreText.text = ((int)target.position.x).ToString();
        heightText.text = Mathf.Max(0, (int)target.position.y).ToString();
    }
}
