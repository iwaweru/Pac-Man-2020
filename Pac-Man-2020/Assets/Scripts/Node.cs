using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] neighbors;
    public Vector2[] validDir;
    public float[] neighborDistance;
    public Node predecessor = null;
    public float distance;
    public bool isEntryNode; //Determines if a node is an entrance to jail.
    // Start is called before the first frame update
    void Start()
    {
        validDir = new Vector2[neighbors.Length];
        neighborDistance = new float[neighbors.Length];
        for(int i = 0; i < neighbors.Length; i++)
        {
            Vector2 distanceVector = neighbors[i].transform.localPosition - transform.localPosition;
            validDir[i] = distanceVector.normalized;
            neighborDistance[i] = distanceVector.sqrMagnitude;
        }
    }
}
