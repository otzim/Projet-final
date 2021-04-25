using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{

    public Animator _anim;


    private void Start()
    {
        _anim = GetComponent<Animator>();
    }


    private void Update()
    {
        //if (Input.GetKey("space"))
        //{
        //    _anim.SetBool(_isJumpingId, true);
        //}
        //else
        //{
        //    _anim.SetBool(_isJumpingId, false);
        //}

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }

    }

    public void AnimationWalk()
    {
        _anim.SetBool(_isWalkingId, true);
        _anim.SetBool(_isIdleId, false);
        //_anim.SetBool(_isSneakingId, false);
        _anim.SetBool(_isRunningId, false);
        
    }

    public void AnimationRun()
    {
        _anim.SetBool(_isRunningId, true);
        _anim.SetBool(_isIdleId, false);
        _anim.SetBool(_isWalkingId, false);
        //_anim.SetBool(_isSneakingId, false);
        
    }

    public void AnimationIdle()
    {
        _anim.SetBool(_isIdleId, true);
        _anim.SetBool(_isRunningId, false);
        //_anim.SetBool(_isSneakingId, false);
        _anim.SetBool(_isWalkingId, false);
        
    }

    //public void AnimationSneak()
    //{
    //    _anim.SetBool(_isSneakingId, true);
    //    _anim.SetBool(_isIdleId, false);
    //    _anim.SetBool(_isRunningId, false);
    //    _anim.SetBool(_isWalkingId, false);

    //}



    //public void AnimationJump(float velocityY)
    //{
    //    _anim.SetBool("_isJumpingId", true);
    //    _anim.SetFloat("_velocityYId", velocityY);
    //}
    //public void AnimationJumpLand()
    //{
    //    _anim.SetBool(_isJumpingId, false);
    //    _anim.SetFloat(_velocityYId, 0f);
    //}


    public void AnimationVelocity(Vector3 localmovement)
    {
        _anim.SetFloat(_velocityXId, localmovement.x);
        _anim.SetFloat(_velocityZId, localmovement.z);
    }

    //private readonly int _velocityYId = Animator.StringToHash("VelocityY");
    private readonly int _velocityXId = Animator.StringToHash("VelocityX");
    private readonly int _velocityZId = Animator.StringToHash("VelocityZ");
    private readonly int _isWalkingId = Animator.StringToHash("IsWalking");
    private readonly int _isRunningId = Animator.StringToHash("IsRunning");
    private readonly int _isIdleId = Animator.StringToHash("IsIdle");
    //private readonly int _isSneakingId = Animator.StringToHash("IsSneaking");
    //private readonly int _isJumpingId = Animator.StringToHash("IsJumping");

    private IEnumerator Attack()
    {
        _anim.SetLayerWeight(_anim.GetLayerIndex("Attack"), 1);
        _anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.9f);
        _anim.SetLayerWeight(_anim.GetLayerIndex("Attack"), 0);
    }

}

