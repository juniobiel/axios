using UnityEngine;

public class AxiosController : MonoBehaviour
{
    private bool CanPush;
    private double ForceToPush;

    private Rigidbody2D _rigidbody2D;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        CanPush = false;

        BarForce.OnForceSelected += BarForce_OnForceSelected;

    }

    private void BarForce_OnForceSelected( double forceSelected )
    {
        CanPush = true;
        ForceToPush = forceSelected;
    }

    void Update()
    {
        if (CanPush)
        {
            //_rigidbody2D.AddForce(new Vector2(1, 1) * (int) ForceToPush, ForceMode2D.Impulse);
            CanPush = false;
        }
    }
}
