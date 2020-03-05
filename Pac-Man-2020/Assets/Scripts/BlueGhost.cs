using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGhost : GhostController
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject blueGhost = GameObject.Find("Inky"); //variable now attached to gameObject
        
        blueGhost.transform.position = new Vector2(11, 10);//Ghost must start at node for now.

        Node current = getNodeAtPosition(transform.position);//Get node at this position.
        if (current != null)
        {
            currentNode = current;
            Debug.Log(currentNode);
        }

        StartCoroutine(Wait()); // start delay

        ChangePosition(direction);
    }

    // Update is called once per frame
    public override void Update() //Override to change behavior
    {

        randomInput();

        Move();

        UpdateOrientation();
    }

    IEnumerator Wait() //delay start of ghost movement
    {
        yield return new WaitForSeconds(10);
        direction = Vector2.left;
    }
}
