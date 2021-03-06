﻿using UnityEngine;
using System.Collections;

public class HoboController : MonoBehaviour {

    [SerializeField]    private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField]    private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField]    private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField]    private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .4f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    //private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    private bool m_Jump;

	private float xLimitLeft = -108f;
	private float xLimightRight = 105f;
	private float ylimit = -3.700002f;

    public bool m_isInHouse;

    public bool m_isDying = false;

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_GroundCheck = transform.Find("GroundCheck");       
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {       

		if (this.m_Rigidbody2D.velocity.y > 0) {
			this.GetComponent<BoxCollider2D> ().enabled = false;
		} else {
			this.GetComponent<BoxCollider2D> ().enabled = true;
		}

		
		if(this.transform.position.x > xLimightRight)
			this.transform.position = new Vector3(xLimightRight,this.transform.position.y,this.transform.position.z);
		
		if(this.transform.position.x < xLimitLeft)
			this.transform.position = new Vector3(xLimitLeft,this.transform.position.y,this.transform.position.z);

        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetKeyDown(KeyCode.Space);
        }	

		if (this.transform.position.y < ylimit)
			this.transform.position = new Vector3 (this.transform.position.x, ylimit, this.transform.position.z);
	}

    void FixedUpdate(){

        float movement = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            movement = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement = 1;
        }


        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }                
        }

        Move(movement, m_Jump);
        m_Jump = false;


        /*
        if(Input.GetKey(KeyCode.A))
        {
            m_Rigidbody2D.AddForce(acceleration * new Vector2(-1f, 0f));
        }
        if(Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(acceleration * new Vector2(-1f, 0f));
        }
       
        if(Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(acceleration * new Vector2(-1f, 0f));
        }
        

        if (Input.GetKey(KeyCode.D))
        {
            m_Rigidbody2D.AddForce(acceleration * new Vector2(1f, 0f));
        }
        if(Input.GetKey(KeyCode.Space))
        {
            m_Rigidbody2D.AddForce(acceleration * new Vector2(0f, 5f));
        }
        */
    }

    public void Move(float move, bool jump)
    {


        //only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl) 
        {
			// Move the character
            m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
		{
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SetIsInHouse(bool b)
    {
        m_isInHouse = b;
    }
	/*
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "UpwardJump") {
			if(this.m_Rigidbody2D.velocity.y>=0.0f){
				Physics2D.IgnoreCollision(col.collider,this.GetComponent<BoxCollider2D>());
			}
		
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "UpwardJump") {
				Physics2D.IgnoreCollision(col.collider,this.GetComponent<BoxCollider2D>(),false);

			
		}
	}*/
}
