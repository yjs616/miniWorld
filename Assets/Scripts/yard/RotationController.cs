using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private bool rotating = false;

    void Update()
    {
        if (rotating)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    public void StartRotation()
    {
        rotating = true;
    }
}
