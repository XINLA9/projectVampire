using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfishWizardAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       GameObject portal = animator.gameObject.transform.GetChild(3).gameObject;
       portal.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       GameObject selfishWizard = animator.gameObject;
       ShootingBullet SB = selfishWizard.GetComponent<ShootingBullet>();
       GameObject portal = selfishWizard.transform.GetChild(3).gameObject;
       animator.ResetTrigger("Ready_fire");
       var newBullet = Instantiate(SB.bullet, selfishWizard.transform.GetChild(2).position, selfishWizard.transform.rotation);
       Rigidbody rb = newBullet.GetComponent<Rigidbody>();
       ParticleSystem PS = newBullet.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
       PS.Play();
       if (!SB.aggressive) {
         rb.AddForce((-SB.moveAway) * SB.GetAttributes().maxSpeed, ForceMode.Impulse);
       } else {
         ChaseArrow CA = newBullet.GetComponent<ChaseArrow>();
         CA.Target = SB.targetEnemy;
         Debug.Log("Here is the name" + newBullet.name);
       }
       portal.SetActive(false);
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
