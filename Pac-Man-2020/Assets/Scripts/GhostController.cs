using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : ControllerNodes
{

    // Start is called before the first frame update
    


    void Start()
    {
        this.canReverse = false;//Ghosts cannot move unless they are at an intersection.

        //redGhost = GameObject.FindGameObjectWithTag("Blinky");
        //blueGhost = GameObject.FindGameObjectWithTag("Inky");

        transform.position = new Vector2(10, 10);//Ghost must start at node for now.
        //redGhost.transform.position = new Vector2(11, 10);
        //blueGhost.transform.position =  new Vector(9,10);

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

    }

}
