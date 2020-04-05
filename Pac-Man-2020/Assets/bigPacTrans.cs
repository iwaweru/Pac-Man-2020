using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigPacTrans : MonoBehaviour
{
  protected Vector2 bigPacBegin = new Vector2(-22, 10);
  protected Vector2 bigPacEnd = new Vector2(25, 10);
  //protected Vector3 removeSprite = new Vector3(-14,10,0);
  public float timeForMove = 6.0f;
  //public float endTime = 0.0f;
  float timer;
  public string BigPac = "PacBig";

  //public Vector3 StopPos =  GameObject.Find("New Pikel-26").transform.position;




  void Start()
  {

      transform.position = bigPacBegin;
      timer = timeForMove;

    }


    /*void delSprite(){
       GameObject BigPac = GameObject.Find(BigPac);
      if (Pac.transform.position == StopPos){

      //Pac.GetComponent<SpriteRenderer>().enabled = false;
      }
    }*/


  // Update is called once per frame
  void Update()
  {


    timer -= Time.deltaTime;
    if (timer > 0) {
         Vector2 distance =  bigPacBegin - bigPacEnd;
                     float degreeOfMovement = (timeForMove - timer) / timeForMove;
                     transform.position = new Vector2 (bigPacBegin.x - (distance.x * degreeOfMovement), bigPacBegin.y );
                   }

      //delSprite();

    }




}
