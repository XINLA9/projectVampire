using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestResetTri : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ShootingBullet SB = animator.gameObject.GetComponent<ShootingBullet>();
        Instantiate(SB.healingCircle, SB.injuredAlly.transform.position, SB.healingCircle.transform.rotation);
       ParticleSystem healCircle = animator.gameObject.transform.GetChild(4).gameObject.GetComponent<ParticleSystem>();
       animator.gameObject.transform.GetChild(4).gameObject.SetActive(true);
       healCircle.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ParticleSystem healCircle = animator.gameObject.transform.GetChild(4).gameObject.GetComponent<ParticleSystem>();
        animator.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        healCircle.Stop();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
