using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovementView : MonoBehaviour
{
    public Animator Animator;

    public BaseMovementModel BaseMovementModel;

    private void Awake()
    {
        if (Animator == null)
        {
            Debug.LogError("Character Animator not setup!");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        Animator.SetBool("isMoving", BaseMovementModel.IsMoving());
    }

    public virtual void DoAttack()
    {
        Animator.SetTrigger("doAttack");
    }

}
