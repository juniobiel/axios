using System;
using UnityEngine;

public class MinotaurController : MonoBehaviour
{
    private const string ANIMATOR_PREPARE_VAR = "Preparing";
    private const string ANIMATOR_LAUNCH_VAR = "Launched";

    private Animator _animator;
    private float TimerCounter;

    public float TimeToCharge;
    private bool CanLaunchAxios;


    public static event Action<bool> OnAxiosLaunched;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        
        BarForce.OnForceSelected += BarForce_OnForceSelected;
    }

    private void OnDisable()
    {
        BarForce.OnForceSelected -= BarForce_OnForceSelected;
    }

    private void BarForce_OnForceSelected( double force )
    {
        _animator.InterruptMatchTarget(true);
        _animator.SetBool(ANIMATOR_PREPARE_VAR, true);
        _animator.SetBool(ANIMATOR_LAUNCH_VAR, false);
        CanLaunchAxios = true;
    }

    private void Start()
    {
        CanLaunchAxios = false;

        _animator.SetBool(ANIMATOR_PREPARE_VAR, false);
        _animator.SetBool(ANIMATOR_LAUNCH_VAR, false);
    }

    private void FixedUpdate()
    {
        if(CanLaunchAxios)
        {
            TimerCounter += Time.deltaTime;

            if(TimerCounter >= TimeToCharge)
            {
                OnAxiosLaunched(true);
                _animator.SetBool(ANIMATOR_PREPARE_VAR, false);
                _animator.SetBool(ANIMATOR_LAUNCH_VAR, true);
                CanLaunchAxios = false;
            }
        }
    }

}
