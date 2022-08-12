using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareMoveState : StateMachineBehaviour
{

    private NewAttack attack = null;

    public string idleStateName = "Idle";
    public string attackerTrigger;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = animator.GetComponent<NewAttack>();
        animator.SetInteger("Target", 0);
        animator.transform.position = attack.Attacker.transform.position;
        if (!string.IsNullOrWhiteSpace(attackerTrigger))
            attack.Attacker.Animator.SetTrigger(attackerTrigger);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attack.Attacker.Animator.GetCurrentAnimatorStateInfo(0).IsName(idleStateName))
            animator.SetTrigger("StartAttack");
    }

}
