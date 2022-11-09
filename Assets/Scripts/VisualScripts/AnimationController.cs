using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    BaseCharacterObject target;
    Animator animator;
    bool walking = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //dead
        if (target.CurrentHP <= 0)
        {
            animator.SetBool("IsDead", true);
        }
        else
        {
            animator.SetBool("IsDead", false);
        }

    }
    public void SetTarget(BaseCharacterObject character)
    {
        target = character;
    }

    public void SetWalking()
    {
        if (walking == false)
        {
            walking = true;
            animator.SetBool("IsWalking", true);
        }
        else
        {
            walking = false;
            animator.SetBool("IsWalking", false);
        }
    }

}
