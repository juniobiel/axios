using UnityEngine;

public class FollowScript : MonoBehaviour
{
    [SerializeField]
    protected bool LockHeight;

    private Vector3 Offset = Vector3.zero;
    private Vector3 NewPosition = Vector3.zero;

    protected Transform AxiosTransform;

    protected void Start()
    {
        Offset = transform.position - AxiosTransform.position;
    }
    protected void Update()
    {
        NewPosition = transform.position;

        NewPosition.x = AxiosTransform.position.x + Offset.x;
        if (!LockHeight) NewPosition.y = AxiosTransform.position.y + Offset.y;

        transform.position = NewPosition;
    }
}
