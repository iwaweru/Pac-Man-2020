using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : ControllerNodes
{

    // Start is called before the first frame update

    public Animator animator;

    private Direction dirNum = Direction.Left;

    void Start()
    {
        this.canReverse = false;//Ghosts cannot move unless they are at an intersection.

        Node current = getNodeAtPosition(transform.position);//Get node at this position.
        if (current != null)
        {
            currentNode = current;
            Debug.Log(currentNode);
        }

        //direction = Vector2.up;//Auto start orangeGhost

        //ChangePosition(direction);
    }

    public override void Update() //Override to change behavior
    {

        randomInput();

        Move();

        UpdateOrientation();
    }

    public void UpdateOrientation()
    {
        if (direction == Vector2.left)
        {
            dirNum = Direction.Left;
        }
        else if(direction == Vector2.right)
        {
            dirNum = Direction.Right;
        }
        else if(direction == Vector2.up)
        {
            dirNum = Direction.Up;
        }
        else if(direction == Vector2.down)
        {
            dirNum = Direction.Down;
        }
        animator.SetInteger("orientation", (int)dirNum);

    }

}
