using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody2D character;
    public float speed;
    private Vector2 moveVelocity;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody2D>();
        character.gravityScale = 0;
        character.transform.position = new Vector2(-0.46f, 1.6f);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        character.MovePosition(character.position + moveVelocity * Time.deltaTime);
    }
}
