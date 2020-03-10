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
    private Vector2[] startPositions = { new Vector2(10, 12), new Vector2(11, 10), new Vector2(10, 10), new Vector2(9,10)};//Corresponding Start Pos for ghost color.
    public GhostColor identity = GhostColor.Blue; //Which ghost is this?
    private float releaseTimer = 0f;
    private bool canLeave = false; //Determines if the ghost can leave.

    public override void Start()
    {
        startPosition = startPositions[(int)identity];//Set start position for the ghosts.
        dirNum = Direction.Right;//Reinitialize for refresh;
        this.canReverse = false;//Ghosts cannot move unless they are at an intersection.

        transform.position = startPosition;//Ghost must start at node for now.


        Node current = getNodeAtPosition(transform.position);//Get node at this position.
        if (current != null)
        {
            currentNode = current;
            Debug.Log(currentNode);
        }

 //     ChangePosition(direction);
    }

    public override void Update() //Override to change behavior
    {
        releaseTimer += Time.deltaTime; //Increment the Ghost Timer.

        if(!canLeave) //Don't release if we already can leave (efficiency check only).
            releaseGhosts();

        //        randomInput();
        if (identity == GhostColor.Pink || identity == GhostColor.Red)
            shortestPathToPacMan();
        else
            randomInput();

        if(canLeave) //Don't leave unless your timer is up.
            Move();

        UpdateOrientation();
    }

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

    private void shortestPathToPacMan() //Decision making algorithm
    {
        GameObject Pac = GameObject.FindGameObjectWithTag("PacMan");
        if(getNodeAtPosition(transform.position) != null)
        {
            float minDistance = 9999;
            Vector2 tempDirection = Vector2.zero;
            Node currentPosition = getNodeAtPosition(transform.position);
            Node[] myNeighbors = currentPosition.neighbors;

            for (int i = 0; i < myNeighbors.Length; i++)
            {
                Node neighborNode = myNeighbors[i];

                Vector2 nodePos = neighborNode.transform.position;
                Vector2 pacPos = Pac.transform.position;

                float tempDistance = (pacPos - nodePos).sqrMagnitude;
                //if the vector distance between the neighbor is the min, set Ghost to go towards that Node
                if (tempDistance < minDistance)
                {
                    //Access the valid directions of the node we are currently on.
                    minDistance = tempDistance;
                    tempDirection = currentPosition.validDir[i];
                }
            }
            ChangePosition(tempDirection);
        }
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
