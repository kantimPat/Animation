using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    private bool boost_mode = false;
    private int direction = 1;
    public float speed = 5f; 

    public GatherInput gather_input;
    public new Rigidbody2D rigidbody2D;
    public Animator animator;

    public float ray_length = 0;
    public LayerMask ground_layer;
    public Transform left_point;
    private bool on_ground = false;
    
    // Start is called before the first frame update
    void Start()
    {
       gather_input = GetComponent<GatherInput>();
       rigidbody2D = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
    }

    private void SetAnimatorValue()
    {
        animator.SetFloat("speed",Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("v_speed",rigidbody2D.velocity.y);
        animator.SetBool("grounded",on_ground);
    }

    private void CheckStatus() 
    {
        on_ground = Physics2D.Raycast(left_point.position,Vector2.down,ray_length,ground_layer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SetAnimatorValue();
        CheckStatus();
    
    }

    
    private void FixedUpdate()
    {
        Flip();
        JumpPlayer();
        
    }

    private void Move()
    {
        rigidbody2D.velocity = new Vector2(
            speed * gather_input.value_x ,
            rigidbody2D.velocity.y
        );
        
        
    }
    private void Flip()
    {
        if(gather_input.value_x  * direction < 0)
        {
            transform.localScale = new Vector3
            (
                -transform.localScale.x,1,1
            );
             direction *= -1;
        }
    }

    private void JumpPlayer()
    {
        if(gather_input.jump_input && on_ground)
        {
            rigidbody2D.velocity = new Vector2(gather_input.value_x * speed, gather_input.jump_force);
        }
        gather_input.jump_input = false;
    }
}
