using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNodes : MonoBehaviour
{

    private Vector2 input = new Vector2(0,0);
    public float speed = 5.5f;
    private int facing = 1; // 0 = left, 1 = right, 2 = down, 3 = up;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        //disallow diagonal movement here.
        if (input != direction && direction != new Vector2(0, 0))
        {
            input = direction;
            Flip(input);
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(input);
    }

    void Move(Vector2 direction)
    {
        transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
    }

    void Flip(Vector2 direction) // We are using Quaternions as a very temporary solution -- later, we will use animation frames instead of actually modifying the transform.
    {
        Quaternion rotater = transform.localRotation;
        switch (direction.normalized.x) // Using the unit vector so I can switch on exact cases.
        {
            case -1: // velocity is to the left
                if (facing != 0)
                {
                    rotater.eulerAngles = new Vector3(0, 0, 180);
                    facing = 0;
                }
                break;
            case 1: // velocity is to the right
                if (facing != 1)
                {
                    rotater.eulerAngles = new Vector3(0, 0, 0);
                    facing = 1;
                }
                break;
        }
        switch (direction.normalized.y)
        {
            case -1: // velocity is down.
                if (facing != 2)
                {
                    rotater.eulerAngles = new Vector3(0, 0, 270);
                    facing = 2;
                }
                break;
            case 1: // velocity is up.
                if (facing != 3)
                {
                    rotater.eulerAngles = new Vector3(0, 0, 90);
                    facing = 3;
                }
                break;
        }
        transform.localRotation = rotater;
    }
}
