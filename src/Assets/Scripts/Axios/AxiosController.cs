using UnityEngine;
using Assets.Scripts.Scene_Manager;
using System.Collections;
using System.Runtime.Remoting.Messaging;

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

    private Vector2 _axiosVelocity;
    private Vector2 _velocityBuffer = Vector2.zero;
    public Vector2 VelocityBuffer { get => _velocityBuffer; }

    private bool _hoverBootPressed;
    public bool HoverBootPressed { get => _hoverBootPressed; }
    public float HoverBootFallSpeed = 10f;
    private float _hoverBootStamina = 1f;
    public float HoverBootStamina { get => _hoverBootStamina; }

    private const float HoverBootUseRate = 5f; // how long it takes to deplete the whole bar, in seconds
    private const float HoverBootRechargeRate = 10f; // how long it takes to rechare the whole bar, in seconds

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
        
        TouchInputHandler();

        // change animation sometime after started falling
        _axiosVelocity = _rigidbody2D.velocity;
        if (_velocityBuffer.y >= 0f && _axiosVelocity.y < 0f)
        {
            _animator.SetBool(ANIMATOR_LAUNCH_VAR, false);
            _animator.SetBool(ANIMATOR_FALL_VAR, true);
        }
        if (_velocityBuffer.y <= 0f && _axiosVelocity.y > 0f)
        {
            _animator.SetBool(ANIMATOR_LAUNCH_VAR, true);
            _animator.SetBool(ANIMATOR_FALL_VAR, false);
        }

        HoverBootHandler();

        _rigidbody2D.velocity = _axiosVelocity;
        _velocityBuffer = _axiosVelocity;
    }

    private void HoverBootHandler()
    {
        if (_hoverBootStamina > 0f && _hoverBootPressed && _axiosVelocity.y <= -HoverBootFallSpeed)
        {
            _axiosVelocity.y = -HoverBootFallSpeed;
            _hoverBootStamina -= Time.deltaTime / HoverBootUseRate;
            if (_hoverBootStamina < 0) _hoverBootStamina = 0; // cap
            return;
        }

        if (!_hoverBootPressed && _hoverBootStamina < 1f)
        {
            _hoverBootStamina += Time.deltaTime / HoverBootRechargeRate;
            if (_hoverBootStamina > 1f) _hoverBootStamina = 1f; // cap
        }
    }

    private void TouchInputHandler()
    {
        _hoverBootPressed = false;
        
        if (Input.GetKey(KeyCode.Mouse0) && Input.mousePosition.x > Screen.width / 2) _hoverBootPressed = true; // placeholder
        
        if (Input.touchCount <= 0) return;
        foreach (Touch currentTouch in Input.touches)
        {
            if (currentTouch.position.x > Screen.width / 2) _hoverBootPressed = true; // placeholder
        }
        
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _rigidbody2D.velocity = Vector2.zero;
            _animator.SetBool(ANIMATOR_HITGROUND_VAR, true);

            yield return new WaitForSeconds(1.5f);

            StartCoroutine(SceneManagerObject.GameOverSceneOpen());
        }
    }
}
