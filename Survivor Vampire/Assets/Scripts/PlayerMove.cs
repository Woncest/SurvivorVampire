using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    Vector3 movementVector;
    [HideInInspector]
    public float lastHorizontalVector = 0;
    [HideInInspector]
    public float lastVerticalVector = 0;

    [SerializeField] float speed = 3f;

    Animate animate;

    // Start is called before the first frame update
    void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
        lastHorizontalVector = -1f;
        lastVerticalVector = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        if(movementVector.x != 0){
            lastHorizontalVector = movementVector.x;
        }
        if(movementVector.y != 0){
            lastVerticalVector = movementVector.y;
        }

        animate.horizontal = movementVector.x;

        if (movementVector.sqrMagnitude > 1)
        {
            movementVector.Normalize();
        }

        movementVector *= speed;

        rgbd2d.velocity = movementVector;
    }
}
