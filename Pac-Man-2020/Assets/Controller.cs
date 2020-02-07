using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody2D character;
    public float speed;
    private float moveInputHor;
    private float moveInputVert;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody2D>();
        character.gravityScale = 0;
        character.transform.position = new Vector2(-10, 2);

    }

    // Update is called once per frame
    void Update()
    {
        moveInputHor = Input.GetAxis("Horizontal");
        moveInputVert = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        character.velocity = new Vector2(moveInputHor * speed, moveInputVert * speed);
    }
}
