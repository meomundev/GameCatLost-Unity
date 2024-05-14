using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{

    public Animator animator;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    public virtual void Move(float move)
    {
        animator.SetFloat("Move", Mathf.Abs(move));
    }

    public virtual void Jump(bool jumping, bool jumpingAgain)
    {
        animator.SetBool("Jump", jumping);
        animator.SetBool("JumpAgain", jumpingAgain);
    }

    public virtual void JumpAgain(bool jumpingAgain, bool jumping)
    {
        animator.SetBool("JumpAgain", jumpingAgain);
        animator.SetBool("Jump", jumping);
    }

    public virtual void Climping(bool climping, bool onStair, bool onGround)
    {
        animator.SetBool("Climping", climping);
        animator.SetBool("OnStair", onStair);
        animator.SetBool("OnGround", onGround);
    }

    public virtual void Climp(bool climp)
    {
        animator.SetBool("Climp", climp);
    }

    public virtual void OnGround(bool onGround)
    {
        animator.SetBool("OnGround", onGround);
    }

    public virtual void OnStair(bool onStair)
    {
        animator.SetBool("OnStair", onStair);
    }

    public virtual void Cling(bool cling)
    {
        animator.SetBool("Cling", cling);
    }
}
