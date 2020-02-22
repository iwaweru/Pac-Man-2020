using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNodes : MonoBehaviour
{

    private Vector2 input = new Vector2(0,0);
    public float speed = 5.5f;
    private int facing = 1; // 0 = left, 1 = right, 2 = down, 3 = up;
    private Node currentNode;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(6, 8);
        Node node = getNodeAtPosition(transform.position);

        if(node != null)
        {
            currentNode = node;
            Debug.Log("Current Node: " + currentNode);
        }
    }


    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector2[] validDirs = currentNode.validDir;
        for(int i = 0; i < validDirs.Length; i++)
        {
            if(validDirs[i] == direction)
            {
                
                input = currentNode.neighbors[i].transform.position;
                Move(input);
                Flip(direction);
                
            }
        }
        //disallow diagonal movement here.
        input = direction;
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Node current = getNodeAtPosition(transform.localPosition);
        if(current != null)
        {
            currentNode = current;
        }
    }

    void Move(Vector2 coordinates)
    {
        //transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
        transform.position = coordinates;
    }

    Node getNodeAtPosition(Vector2 pos)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<gameBoard>().board[(int)pos.x, (int)pos.y];
        if (tile != null)
        {
            Debug.Log(tile.name);
            return tile.GetComponent<Node>();//Node is a component of the pill objects.
        }
        return null;
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
