using UnityEngine;
using Assets.Scripts.Scene_Manager;
using System.Collections;
using Assets.Scripts.Utils;

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

    public Vector2 VelocityBuffer { private set; get; }

    public bool HoverBootPressed { private set; get; }
    
    public float HoverBootFallSpeed = 10f;

    public float HoverBootStamina { private set; get; }

    private const float HoverBootUseRate = 5f; // how long it takes to deplete the whole bar, in seconds
    private const float HoverBootRechargeRate = 10f; // how long it takes to rechare the whole bar, in seconds

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

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
        
        VelocityBuffer = Vector2.zero;
        HasBeenLaunched = false;

    }

    private void Update()
    {
        if (!HasBeenLaunched) return;

        TouchInputHandler();

        //change animation sometime after started falling
        _axiosVelocity = _rigidbody2D.velocity;
        
        //TODO: Incluir as animações de planagem
        PlayerAnimation();

        HoverBootHandler();

        //_rigidbody2D.velocity = _axiosVelocity;

        VelocityBuffer = _axiosVelocity;
    }

    /// <summary>
    /// Controls all Player Animation States
    /// </summary>
    private void PlayerAnimation()
    {
        if (VelocityBuffer.y >= 0f && _axiosVelocity.y < 0f)
        {
            _animator.SetBool(ANIMATOR_LAUNCH_VAR, false);
            _animator.SetBool(ANIMATOR_FALL_VAR, true);
        }

        if (VelocityBuffer.y <= 0f && _axiosVelocity.y > 0f)
        {
            _animator.SetBool(ANIMATOR_LAUNCH_VAR, true);
            _animator.SetBool(ANIMATOR_FALL_VAR, false);
        }
    }

    private void HoverBootHandler()
    {
        if (HoverBootStamina > 0f && HoverBootPressed)
        {

            _rigidbody2D.AddForce(Vector2.one * 1.5f);

            HoverBootStamina -= Utility.ConsumeRateBySeconds(HoverBootUseRate);

            if (HoverBootStamina < 0)
                HoverBootStamina = 0; // cap

            return;
        }

        if (!HoverBootPressed && HoverBootStamina < 1f)
        {
            HoverBootStamina += Utility.ConsumeRateBySeconds(HoverBootRechargeRate);

            if (HoverBootStamina > 1f)
                HoverBootStamina = 1f; // cap
        }
    }

    //TODO: Refazer utilizando o novo input system orientado a eventos.
    private void TouchInputHandler()
    {
        HoverBootPressed = false;
        
        if (Input.GetKey(KeyCode.Mouse0) && Input.mousePosition.x > Screen.width / 2) HoverBootPressed = true; // placeholder
        
        if (Input.touchCount <= 0) return;
        
        foreach (Touch currentTouch in Input.touches)
        {
            if (currentTouch.position.x > Screen.width / 2) HoverBootPressed = true; // placeholder
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
