using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameBoard : MonoBehaviour
{
    // board dimensions
    private static int boardWidth = 19; 
    private static int boardHeight = 22;

    //Array of type GameObject initialized with board width and height
    //These are the locations that will be stored
    //We are getting the positions of the game objects and then storing them at that position in this array.
    public GameObject[,] board = new GameObject[boardWidth, boardHeight];

    // Start is called before the first frame update
    void Start()
    {
        //Create an array of objects containing every objects in the scene
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));

        //Then iterate over that array
        //Assign each object to the variable "o"
        foreach (GameObject o in objects)
		{
            //Get the positions: 
            Vector2 pos = o.transform.position; // we use "position" (instead of "localposition") which is in the global space of Unity. 

            //Sanity check: we only want to store the objects in the array (pills, walls, etc.) not PacMan itself. 
            if (o.name != "Pac-Man")
			{
                //store the object o in the board array
                board[(int)pos.x, (int)pos.y] = o;
			} else
			{
                //just print this in case PacMan is found. 
                Debug.Log("Found Pac-Man at:" + pos);
			}
		}
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
