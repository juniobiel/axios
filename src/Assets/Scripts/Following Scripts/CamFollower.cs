using UnityEngine;

public class CamFollower : FollowScript
{
    //Dependency Injection at Unity
    private void OnEnable()
    {
        AxiosTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LockHeight = false;
    }
}
