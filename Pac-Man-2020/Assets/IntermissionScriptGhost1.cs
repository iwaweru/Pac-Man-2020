using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionScriptGhost1 : MonoBehaviour
{
    // Start is called before the first frame update
    protected Vector2 ghostTransBegin = new Vector2(23, 10);
    protected Vector2 ghostTransEnd = new Vector2(-10, 10);
    public float timeForMove = 20.0f;
    float timer;



    void Start()
    {
        transform.position = ghostTransBegin;
        timer = timeForMove;


    }

    // Update is called once per frame
    void Update()
    {
      timer -= Time.deltaTime;
      if (timer > 0) {
           Vector2 distance =  ghostTransEnd - ghostTransBegin;
                       float degreeOfMovement = (timeForMove - timer) / timeForMove;
                       transform.position = new Vector2 (ghostTransBegin.x + (distance.x * degreeOfMovement), ghostTransBegin.y + (distance.y * degreeOfMovement));
               }


    }
}
