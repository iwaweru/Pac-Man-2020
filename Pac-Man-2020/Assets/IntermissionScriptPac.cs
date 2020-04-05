using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionScriptPac : MonoBehaviour
{
  protected Vector2 pacTransBegin = new Vector2(22, 10);
  protected Vector2 pacTransEnd = new Vector2(-17, 10);
  protected Vector3 removeSprite = new Vector3(-14,10,0);
  public float timeForMove = 6.0f;
  //public float endTime = 0.0f;
  float timer;
  public string Pacc = "Pac-Man1";

  public Vector3 StopPos =  GameObject.Find("New Pikel-26").transform.position;




  void Start()
  {

      transform.position = pacTransBegin;
      timer = timeForMove;

    }


    void delSprite(){
       GameObject Pac = GameObject.Find(Pacc);
      if (Pac.transform.position == StopPos){

      //Pac.GetComponent<SpriteRenderer>().enabled = false;
      }
    }


  // Update is called once per frame
  void Update()
  {


    timer -= Time.deltaTime;
    if (timer > 0) {
         Vector2 distance =  pacTransEnd - pacTransBegin;
                     float degreeOfMovement = (timeForMove - timer) / timeForMove;
                     transform.position = new Vector2 (pacTransBegin.x + (distance.x * degreeOfMovement), pacTransBegin.y );
                   }

      delSprite();

    }




}
