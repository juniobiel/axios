using UnityEngine;
using Assets.Scripts.Scene_Manager;

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

    private Vector2 _velocityBuffer = Vector2.zero;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        HasBeenLaunched = false;

        BarForce.OnForceSelected += BarForce_OnForceSelected;
        MinotaurController.OnAxiosLaunched += MinotaurController_OnAxiosLaunched;
    }

    private void OnDisable()
    {
        BarForce.OnForceSelected -= BarForce_OnForceSelected;
        MinotaurController.OnAxiosLaunched -= MinotaurController_OnAxiosLaunched;
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

    private void Update()
    {
        if (!HasBeenLaunched) return;
        if (_velocityBuffer.y >= 0f && _rigidbody2D.velocity.y < 0f)
        {
            _animator.SetBool(ANIMATOR_LAUNCH_VAR, false);
            _animator.SetBool(ANIMATOR_FALL_VAR, true);
        }
        if (_velocityBuffer.y <= 0f && _rigidbody2D.velocity.y > 0f)
        {
            _animator.SetBool(ANIMATOR_LAUNCH_VAR, true);
            _animator.SetBool(ANIMATOR_FALL_VAR, false);
        }
        _velocityBuffer = _rigidbody2D.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            _rigidbody2D.velocity = Vector2.zero;
            _animator.SetBool(ANIMATOR_HITGROUND_VAR, true);
            
            StartCoroutine(SceneManagerObject.GameOverSceneOpen());
        }
    }
}
