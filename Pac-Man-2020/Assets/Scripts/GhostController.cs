using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Vector2 direction = new Vector2(0, 0);
    private Vector2 queuedDirection;

    public float speed = 3f;

    private Node currentNode;
    private Node previousNode;
    private Node targetNode;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(10, 0);//GHOST MUST START ON A NODE FOR NOW.

        Node current = getNodeAtPosition(transform.position);//Get node at this position.
        if (current != null)
        {
            currentNode = current;
            Debug.Log(currentNode);
        }

        direction = Vector2.right; //Auto start.
        ChangePosition(direction);

    }

    // Update is called once per frame
    void Update()
    {
        CheckNumGen();//Disallows diagonal or conflicting movements. Check the randomly generated number. 

        Move();//Move, or act on gathered user  input.
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }

    void CheckNumGen()//Check random number and update current direction.
    {
        int randNum = (int)Random.Range(1, 4);

        if (randNum == 1)
        {
            ChangePosition(Vector2.right);
            //MoveToNode(direction);
        }
        else if (randNum == 2)
        {
            ChangePosition(Vector2.up);
            //MoveToNode(direction);
        }
        else if (randNum == 3)
        {
            ChangePosition(Vector2.left);
            //MoveToNode(direction);
        }
        else if (randNum == 4)
        {
            ChangePosition(Vector2.down);
            //MoveToNode(direction);
        }

    }

    Node CanMove(Vector2 d)
    {
        Node moveToNode = null;
        Vector2[] validDirs = currentNode.validDir;
        for (int i = 0; i < validDirs.Length; i++)
        {
            if (currentNode.validDir[i] == d)
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
        if (d != direction) //If the direction is different from current direction, store it for next intersection.
        {
            queuedDirection = d;
        }
        if (currentNode != null) //If we are at an intersection
        {
            Node moveToNode = CanMove(d);//Check if input direction is a valid move.

            if (moveToNode != null)//If it's a valid move.
            {
                direction = d;//Start moving in the direction of that node.
                targetNode = moveToNode;//Update target node to node that we just checked.
                previousNode = currentNode;//We are leaving current node, so it becomes previous node.
                currentNode = null;//We are no longer on a node.

            }
        }
    }

    void Move()
    {

        if (targetNode != currentNode && targetNode != null)
        {
            if (OverShotTarget())
            {
                currentNode = targetNode;
                transform.localPosition = currentNode.transform.position;

                GameObject otherPortal = GetPortal(currentNode.transform.position);

                if (otherPortal != null)
                { //Is it a portal
                    transform.localPosition = otherPortal.transform.position;
                    currentNode = otherPortal.GetComponent<Node>();
                }

                Node moveToNode = CanMove(queuedDirection);

                if (moveToNode != null)//Does the queued direction actually exist at the intersection?
                {
                    direction = queuedDirection;
                }
                if (moveToNode == null)//Does the queued direction not exist?
                {
                    moveToNode = CanMove(direction);//Try to move using existing direction -- don't update.
                }
                if (moveToNode != null)//If we can move using existing direction, update node targets.
                {
                    targetNode = moveToNode;
                    previousNode = currentNode;
                    currentNode = null;
                }
                else//We can't move on existing direction OR queued direction, so we stop.
                {
                    direction = Vector2.zero;
                }
            }
            else
            {
                transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;//Move.
            }
        }
    }

    void MoveToNode(Vector2 d)
    {
        Node moveToNode = CanMove(d);

        if (moveToNode != null)
        {
            transform.position = moveToNode.transform.position;
            currentNode = moveToNode;
        }
    }

    Node getNodeAtPosition(Vector2 pos) //Get the intersection at this position.
    {
        GameObject tile = GameObject.Find("Game").GetComponent<gameBoard>().board[(int)pos.x, (int)pos.y];
        Debug.Log(GameObject.Find("Game").GetComponent<gameBoard>().board[(int)pos.x, (int)pos.y]);
        if (tile != null)
        {
            Debug.Log("Not Null");
            return tile.GetComponent<Node>(); //Node is a component of the pill objects.
        }
        return null;
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

    GameObject GetPortal(Vector2 pos)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<gameBoard>().board[(int)pos.x, (int)pos.y];
        if (tile != null)
        {
            if (tile.GetComponent<Pills>() != null)
            {
                if (tile.GetComponent<Pills>().isPortal)
                {
                    GameObject otherPortal = tile.GetComponent<Pills>().portalReceiver;
                    return otherPortal;
                }
            }
        }
        return null;
    }

}
