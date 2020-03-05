﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : ControllerNodes
{

    // Start is called before the first frame update

    public Animator animator;//This will need to be edited when each ghost is given a different start position. Appears in the GameBoard script.
    private Direction dirNum = Direction.Left;

    public override void Start()
    {
        startPosition = new Vector2(10, 10);
        dirNum = Direction.Left;//Reinitialize for refresh;
        this.canReverse = false;//Ghosts cannot move unless they are at an intersection.

        //redGhost = GameObject.FindGameObjectWithTag("Blinky");
        //blueGhost = GameObject.FindGameObjectWithTag("Inky");
        //redGhost.transform.position = new Vector2(11, 10);
        //blueGhost.transform.position =  new Vector(9,10);

        transform.position = startPosition;//Ghost must start at node for now.


        Node current = getNodeAtPosition(transform.position);//Get node at this position.
        if (current != null)
        {
            currentNode = current;
            Debug.Log(currentNode);
        }
    
        direction = Vector2.left;//Auto start.
        ChangePosition(direction);
    }

    public override void Update() //Override to change behavior
    {

        randomInput();

        Move();

        UpdateOrientation();
    }

    private void UpdateOrientation()
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
