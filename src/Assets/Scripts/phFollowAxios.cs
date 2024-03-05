using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phFollowAxios : MonoBehaviour
{
    public bool lockHeight = false;
    private Vector3 offset = Vector3.zero;
    private Vector3 newPosition = Vector3.zero;

    [SerializeField] private Transform axios;

    private void Start()
    {
        offset = this.transform.position - axios.position;
    }

    private void Update()
    {
        newPosition = this.transform.position;

        newPosition.x = axios.position.x + offset.x;
        if (!lockHeight) newPosition.y = axios.position.y + offset.y;

        this.transform.position = newPosition;
    }
}
