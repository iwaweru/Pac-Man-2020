using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : ControllerNodes
{

    // Start is called before the first frame update
    // Time before ghosts leave jail;
    private float blueStartDelay = 0f;
    private float orangeStartDelay = 5f;
    private float redStartDelay = 10f;
    private float pinkStartDelay = 15f;

    public Animator animator;//This will need to be edited when each ghost is given a different start position. Appears in the GameBoard script.
    private Direction dirNum = Direction.Right;
    public enum GhostColor
    {
        Blue, //leaves first
        Orange, //leaves second
        Red, //leaves third
        Pink //leaves fourth
    }
    private Vector2[] startPositions = { new Vector2(11, 10), new Vector2(10, 10), new Vector2(9, 10), new Vector2(9,10)};//Corresponding Start Pos for ghost color.
    public GhostColor identity = GhostColor.Blue; //Which ghost is this?
    private float releaseTimer = 0f;
    private bool canLeave = false; //Determines if the ghost can leave.

    public override void Start()
    {
        startPosition = startPositions[(int)identity];//Set start position for the ghosts.
        dirNum = Direction.Right;//Reinitialize for refresh;
        this.canReverse = false;//Ghosts cannot move unless they are at an intersection.

<<<<<<< HEAD
=======
        transform.position = startPosition;//Ghost must start at node for now.


>>>>>>> 87996c1ca086ae2f24a4009ebfc9996f430a939e
        Node current = getNodeAtPosition(transform.position);//Get node at this position.
        if (current != null)
        {
            currentNode = current;
            Debug.Log(currentNode);
        }

<<<<<<< HEAD
        //direction = Vector2.up;//Auto start orangeGhost

        //ChangePosition(direction);
=======
 //     ChangePosition(direction);
>>>>>>> 87996c1ca086ae2f24a4009ebfc9996f430a939e
    }

    public override void Update() //Override to change behavior
    {
        releaseTimer += Time.deltaTime; //Increment the Ghost Timer.

        if(!canLeave) //Don't release if we already can leave (efficiency check only).
            releaseGhosts();

        randomInput();

        if(canLeave) //Don't leave unless your timer is up.
            Move();

        UpdateOrientation();
    }

<<<<<<< HEAD
    public void UpdateOrientation()
=======
    private void releaseGhosts()
    {
        if(identity == GhostColor.Blue && releaseTimer > blueStartDelay)
        {
            canLeave = true;
        }
        else if(identity == GhostColor.Pink && releaseTimer > pinkStartDelay)
        {
            canLeave = true;
        }
        else if(identity == GhostColor.Orange && releaseTimer > orangeStartDelay)
        {
            canLeave = true;
        }
        else if(identity == GhostColor.Red && releaseTimer > redStartDelay)
        {
            canLeave = true;
        }
    }

    private void UpdateOrientation()
>>>>>>> 87996c1ca086ae2f24a4009ebfc9996f430a939e
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
