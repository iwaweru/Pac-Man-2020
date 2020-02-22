using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNodes : MonoBehaviour
{

    private Vector2 direction = new Vector2(0,0);
    private Vector2 queuedDirection;

    public float speed = 5.5f;
    private int facing = 1; // 0 = left, 1 = right, 2 = down, 3 = up;
    private Node currentNode;
    private Node previousNode;
    private Node targetNode;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(6, 10);
        Node current = getNodeAtPosition(transform.position);
        if (current != null)
        {
            currentNode = current;
            Debug.Log(currentNode);
        }

        direction = Vector2.left;
        ChangePosition(direction);
    }


    void Update()
    {
        CheckInput();

        Move();

        Flip();
        
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangePosition (Vector2.left);
            //MoveToNode(direction);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangePosition(Vector2.down);
            //MoveToNode(direction);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ChangePosition(Vector2.up);
            //MoveToNode(direction);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangePosition(Vector2.right);
            //MoveToNode(direction);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }
    Node CanMove(Vector2 d)
    {
        Node moveToNode = null;
        Vector2[] validDirs = currentNode.validDir;
        for (int i = 0; i < validDirs.Length; i++)
        {
            if(currentNode.validDir[i] == d)
            {
                moveToNode = currentNode.neighbors[i];
                break;
            }
        }
        //disallow diagonal movement here.
        return moveToNode;
    }

    void ChangePosition(Vector2 d)
    {
        if(d != direction)
        {
            queuedDirection = d;
        }

        if(currentNode != null)
        {
            Node moveToNode = CanMove(d);

            if(moveToNode != null)
            {
                direction = d;
                targetNode = moveToNode;
                previousNode = currentNode;
                currentNode = null;

            }
        }

    }


    void Move()
    {

        if(targetNode != currentNode && targetNode != null)
        {
            if (OverShotTarget())
            {
                currentNode = targetNode;
                transform.localPosition = currentNode.transform.position;

                Node moveToNode = CanMove(queuedDirection);

                if(moveToNode != null)
                {
                    direction = queuedDirection;
                }
                if(moveToNode == null)
                {
                    moveToNode = CanMove(direction);
                }
                if(moveToNode != null)
                {
                    targetNode = moveToNode;
                    previousNode = currentNode;
                    currentNode = null;
                }
                else
                {
                    direction = Vector2.zero;
                }
            }
            else
            {
                transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
            }
        }


        
    }

    void MoveToNode(Vector2 d)
    {
        Node moveToNode = CanMove(d);

        if(moveToNode != null)
        {
            transform.position = moveToNode.transform.position;
            currentNode = moveToNode;
        }
    }

    Node getNodeAtPosition(Vector2 pos)
    {

        Debug.Log("Entered!");
        Debug.Log("Current Position: X: " + (int)pos.x + " Y: " + (int)pos.y);
        GameObject tile = GameObject.Find("Game").GetComponent<gameBoard>().board[(int)pos.x, (int)pos.y];
        if (tile != null)
        {
            Debug.Log("Not Null");
            return tile.GetComponent<Node>();//Node is a component of the pill objects.
        }
        return null;
    }

    void Flip() // We are using Quaternions as a very temporary solution -- later, we will use animation frames instead of actually modifying the transform.
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

    bool OverShotTarget()
    {
        float previousToTarget = LengthFromNode(targetNode.transform.position);
        float previousToSelf = LengthFromNode(transform.position);

        return previousToSelf > previousToTarget;
    }

    float LengthFromNode(Vector2 target)
    {
        Vector2 vect = target - (Vector2)previousNode.transform.position;
        
        return vect.sqrMagnitude;
    }
}
