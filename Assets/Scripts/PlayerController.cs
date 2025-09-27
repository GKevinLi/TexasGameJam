using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float velocityX = 0;
    float velocityY = 0;
    public float acceleration = 10.0f;
    public float speedMultiplier = 2f;
    public float deceleration = 0.01f;
    bool touchingGnd = false;

    public Rigidbody2D rb;
    InputAction moveAction;
    InputAction jumpAction;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        bool leftPressed = (moveValue.x == -1.00f);
	    bool rightPressed = (moveValue.x == 1.00f);

        if(leftPressed && velocityX > -0.5f) {
		    velocityX -= Time.deltaTime * acceleration;
		
	    }
	    if(rightPressed && velocityX < 0.5f) {
		    velocityX += Time.deltaTime * acceleration;
	    } 
        if(!rightPressed && velocityX > 0.0f) {
		    velocityX -= Time.deltaTime * deceleration;
	    }
	    if(!leftPressed && velocityX < 0.0f) {
		    velocityX += Time.deltaTime * deceleration;
	    }
	    if(!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)) {
		    velocityX = 0.0f;
	    }

        if (jumpAction.IsPressed() && touchingGnd)
        {
            //rb.AddForce(transform.up * 50, ForceMode2D.Impulse);
            rb.linearVelocityY = (25);

            touchingGnd = false;
        }
        else {
            
            
        }

       
	  
        //rb.MovePosition(rb.position + new Vector2(velocityX * speedMultiplier, velocityY));
        rb.linearVelocityX = velocityX * speedMultiplier;
        //rb.AddForce(transform.right * velocityX);
     
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        //Debug.Log("COLLIDED");
        if (collision.gameObject.CompareTag("Ground")) 
        { 
            touchingGnd = true;
        } 
    } 
}
