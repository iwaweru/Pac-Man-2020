using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : ControllerNodes
{
    private Vector2[] dirs = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
    // Start is called before the first frame update

    void Start()
    {
        this.canReverse = false;//Ghosts cannot move unless they are at an intersection.

        transform.position = new Vector2(7, 10);//Ghost must start at node for now.

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

        CheckInput();

        Move();

    }
    override public void CheckInput() //CheckInput Method is used differently
    {
        ChangePosition(dirs[RandomNumber()]);
    }

    int RandomNumber() //Random Number Generator for Ghost to use as move input.
    {
        return Random.Range(0, 4);
    }
}
