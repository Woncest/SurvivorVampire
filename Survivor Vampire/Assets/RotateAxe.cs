using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxe : MonoBehaviour
{
    // Speed of the rotation
    [SerializeField] private float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the axe around the Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
