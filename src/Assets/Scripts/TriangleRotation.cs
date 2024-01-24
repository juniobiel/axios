using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.one * Time.deltaTime * 50);
    }
}
