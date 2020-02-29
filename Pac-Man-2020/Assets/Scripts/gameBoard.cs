using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameBoard : MonoBehaviour
{
    // board dimensions
    private static int boardWidth = 30; 
    private static int boardHeight = 30;
    public static int MULTIPLIER = 10; //Score added per pill.
    private static float time = 0;

    public int points = 0;

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
            if (o.name != "Clyde" && o.name != "Pac-Man-Node" && o.name != "Game" && o.name != "Maze" && o.name != "Pills" && o.name != "Nodes" && o.name != "Background" &&  o.name != "NonNodes" && o.name != "Overlay" && o.name != "Blinky" && o.name != "Inky")
			{
                //if (o.GetComponent<Pills>() != null) {
                //    if (o.GetComponent<Pills>().isPellet || o.GetComponent<Pills>().isLargePellet) {
                //        totalPellets++;
                //    }
                //}
                //store the object o in the board array
                Debug.Log("X: " + (int)pos.x + " Y: " + (int)pos.y + " " + o.name);
                board[(int)pos.x, (int)pos.y] = o;
                //Debug.Log(board[(int)pos.x, (int)pos.y]);
			} else
			{
                //just print this in case PacMan is found. 
                Debug.Log("Found " + o.name + " at " + pos);
			}
		}
    }
    public void score()
    {
        points += MULTIPLIER;
    }

    public void addTime(float seconds)
    {
        if (time != 0)
        {
            time += seconds;
        }
        else time += .5f;//Prevents glitching.

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Time: " + time);
        if(time != 0)
        {
            time -= (float)(1.0/24.0);
            if(time < 0)
            {
                time = 0;
            }
        }
        if (time == 0)
        {
            GetComponent<AudioSource>().Stop();
        }
    }

    private void Update()
    {

    }
}
