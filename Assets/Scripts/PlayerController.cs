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
    bool touchingAnything = false;
    public Animator anim;

    public Rigidbody2D rb;
    public Transform startPos;
    InputAction moveAction;
    InputAction jumpAction;
    void Start()
    {
        startPos = transform;
        anim = gameObject.GetComponent<Animator>();
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
            rb.linearVelocityY = (30);
            
            anim.SetTrigger("Jump");
            anim.ResetTrigger("exitJump");
            anim.ResetTrigger("exitFall");
           
            touchingGnd = false;
            touchingAnything = false;
        }
        else if(rb.linearVelocityY < 0.05f) {
            anim.ResetTrigger("exitFall");
            anim.SetTrigger("beginFall");
            anim.SetTrigger("exitJump");
            
            //anim.ResetTrigger("Jump");
        }
        else {
            anim.ResetTrigger("Jump");
            anim.ResetTrigger("exitJump");
            //anim.SetTrigger("exitJump");
        }

        if(rb.linearVelocityY >= -0.05f) {
            anim.ResetTrigger("beginFall");
        }


        if(touchingAnything) {
            anim.SetTrigger("exitJump");
            anim.SetTrigger("exitFall");
        }
        
        //Debug.Log(touchingAnything);
       if(velocityX < 0) {
            
            transform.eulerAngles = new Vector3(0, 180, 0);
       }
       else if(velocityX > 0) {
           transform.eulerAngles = new Vector3(0, 0, 0);
       }
	  
        //rb.MovePosition(rb.position + new Vector2(velocityX * speedMultiplier, velocityY));
        rb.linearVelocityX = velocityX * speedMultiplier;
        if(velocityX > 0.00f || velocityX < -0.00f) {
            anim.SetTrigger("Running");
            anim.ResetTrigger("Idle");
        }
        else {
            anim.ResetTrigger("Running");
            anim.SetTrigger("Idle");
        }
        if((velocityX < 0.05f && velocityX > -0.05f) && !jumpAction.IsPressed()) {
            //anim.SetTrigger("Idle");
        }
        //rb.AddForce(transform.right * velocityX);
     
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        //Debug.Log("COLLIDED");
        touchingAnything = true;
        if (collision.gameObject.CompareTag("Ground")) 
        { 
            touchingGnd = true;
        } 
    } 
    void OnCollisionExit2D(Collision2D collision) 
    { 
        //Debug.Log("COLLIDED");
        touchingAnything = false;
        
    } 
}
