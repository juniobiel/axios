using System;
using UnityEngine;

public class AxiosController : MonoBehaviour
{
    private const string ANIMATOR_PREPARE_VAR = "Preparing";
    private const string ANIMATOR_LAUNCH_VAR = "Launched";
    private const string ANIMATOR_FALL_VAR = "Falling";
    private const string ANIMATOR_HITGROUND_VAR = "HitGround";

    private bool HasBeenLaunched;
    private double ForceToPush;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        HasBeenLaunched = false;

        BarForce.OnForceSelected += BarForce_OnForceSelected;
        MinotaurController.OnAxiosLaunched += MinotaurController_OnAxiosLaunched;
    }

    private void MinotaurController_OnAxiosLaunched(bool obj)
    {
        _animator.SetBool(ANIMATOR_LAUNCH_VAR, true);
        _rigidbody2D.AddForce(new Vector2(1, 1) * (int)ForceToPush, ForceMode2D.Impulse); // initial impulse
        HasBeenLaunched = true;
    }

    private void BarForce_OnForceSelected( double forceSelected )
    {
        _animator.SetBool(ANIMATOR_PREPARE_VAR, true);

        ForceToPush = forceSelected;
    }

    private void Start()
    {
        _animator.SetBool(ANIMATOR_PREPARE_VAR, false);
        _animator.SetBool(ANIMATOR_LAUNCH_VAR, false);
        _animator.SetBool(ANIMATOR_FALL_VAR, false);
        _animator.SetBool(ANIMATOR_HITGROUND_VAR, false);
    }

    void Update()
    {
        if (HasBeenLaunched)
        {
            // more stuff here later
        }
    }
}
