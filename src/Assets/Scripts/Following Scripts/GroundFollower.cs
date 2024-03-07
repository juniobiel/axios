using UnityEngine;

public class GroundFollower : FollowScript
{
    //Dependency Injection at Unity
    private void OnEnable()
    {
        AxiosTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LockHeight = true;
    }
}
