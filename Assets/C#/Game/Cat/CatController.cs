using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private Rigidbody2D r2;
    public CatAnimation catAnimation;

    private float checkGround = 0.5f;
    private float checkWall = 0.1f;

    [SerializeField]
    public int cat_Hp = 5;
    public float cat_Speed = 2.0f;

    // idle
    public bool cat_OnGround = false;

    // move
    public float cat_Move = 0;
    public bool cat_Moving = false;

    // jump
    public float cat_JumpHeight = 8.0f;
    public bool cat_Jumping = false;
    public bool cat_JumpingAgain = false;
    public bool cat_AllowJump = true;
    public bool cat_AllowJumpAgain = false;

    // climp
    public float cat_Climp = 0;
    public bool cat_Climping = false;
    public bool cat_AllowClimp = false;

    // check wall collision
    public bool cat_OnCollisionWithWall = false;

    public Transform groundCheck;
    public LayerMask Ground;
    public Transform stairCheck;
    public LayerMask Stair;
    public Transform wallCheck;

    private void Awake()
    {
        this.r2 = GetComponent<Rigidbody2D>();
        this.catAnimation = GetComponent<CatAnimation>();
        this.groundCheck = GameObject.Find("GroundCheck").transform;
        this.stairCheck = GameObject.Find("StairCheck").transform;
        this.wallCheck = GameObject.Find("WallCheck").transform;
    }

    private void Update()
    {
        this.Move();
        this.Jump();
        this.Climp();
        this.CheckWallCollision();
    }

    private void Move()
    {
        this.cat_OnGround = Physics2D.OverlapCircle(groundCheck.position, this.checkGround, Ground);
        this.cat_Move = Input.GetAxisRaw("Horizontal");
        r2.velocity = new Vector2(this.cat_Speed * this.cat_Move, r2.velocity.y);

        if (this.cat_Move == -1)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            this.cat_Moving = true;
        }
        else if (this.cat_Move == 1)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            this.cat_Moving = true;
        }
        else
        {
            this.cat_Moving = false;
        }

        this.catAnimation.Move(this.cat_Move);
    }

    private void Jump()
    {
        this.cat_AllowJump = Physics2D.OverlapCircle(groundCheck.position, this.checkGround, Ground);

        if (this.cat_AllowJump == true)
        {
            this.cat_Jumping = false;
            this.cat_AllowJumpAgain = false;
            this.cat_JumpingAgain = false;
            this.cat_JumpHeight = 8.0f;
        }

        if (Input.GetKey(KeyCode.Z) && this.cat_AllowJump && !this.cat_AllowJumpAgain)
        {
            r2.velocity = new Vector2(r2.velocity.x, this.cat_JumpHeight);
            this.cat_Moving = false;
            this.cat_Jumping = true;
            this.cat_AllowJumpAgain = true;
            this.cat_JumpingAgain = false;
        }

        this.cat_JumpHeight = 6.0f;

        if (Input.GetKeyDown(KeyCode.Z) && this.cat_AllowJump == false && this.cat_AllowJumpAgain)
        {
            r2.velocity = new Vector2(r2.velocity.x, this.cat_JumpHeight);
            this.cat_Moving = false;
            this.cat_Jumping = true;
            this.cat_JumpingAgain = true;
            this.cat_AllowJumpAgain = false;
        }

        this.catAnimation.Jump(this.cat_Jumping, this.cat_JumpingAgain);
        this.catAnimation.JumpAgain(this.cat_JumpingAgain, this.cat_Jumping);
    }

    private void Climp()
    {
        this.cat_AllowClimp = Physics2D.OverlapCircle(stairCheck.position, 0.1f, Stair);
        this.cat_Climp = Input.GetAxisRaw("Vertical");
        this.cat_Move = Input.GetAxisRaw("Horizontal");

        if (cat_AllowClimp)
        {
            r2.gravityScale = 0;
            this.cat_AllowJumpAgain = true;
            this.cat_Jumping = false;
            this.cat_JumpingAgain = false;
            if (this.cat_Climp != 0 || this.cat_Move != 0)
            {
                r2.velocity = new Vector2(r2.velocity.x, this.cat_Speed * this.cat_Climp);
                this.cat_Climping = true;
            }
            else
            {
                r2.velocity = new Vector2(r2.velocity.x, 0);
                this.cat_Climping = false;
            }
        }
        else
        {
            r2.gravityScale = 1f;
        }

        this.cat_OnGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, Ground);

        this.catAnimation.Climping(this.cat_Climping, this.cat_AllowClimp, this.cat_AllowClimp);
        this.catAnimation.Climp(!this.cat_Climping);
        this.catAnimation.OnGround(this.cat_OnGround);
        this.catAnimation.OnStair(this.cat_AllowClimp);
    }

    private void CheckWallCollision()
    {
        this.cat_OnCollisionWithWall = Physics2D.OverlapCircle(wallCheck.position, this.checkWall, Ground);

        if (this.cat_OnCollisionWithWall)
        {
            r2.velocity = new Vector2(r2.velocity.x, -0.1f);
        }

        this.catAnimation.Cling(this.cat_OnCollisionWithWall);
    }
}
