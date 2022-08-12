using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareNextTargetState : StateMachineBehaviour
{

    private NewAttack attack = null;
    private int target = 0;

    public string idleStateName = "Idle";
    public string attackTrigger;
    public string returnTrigger;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = animator.GetComponent<NewAttack>();

        target = animator.GetInteger("Target") + 1;

        if(target >= attack.Targets.Length)
            attack.Attacker.Animator.SetTrigger(returnTrigger);
        else
            attack.Attacker.Animator.SetTrigger(attackTrigger);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attack.Attacker.Animator.GetCurrentAnimatorStateInfo(0).IsName(idleStateName))
        {
            animator.SetInteger("Target", target);
            animator.SetTrigger(target >= attack.Targets.Length ? "NoMoreTargets" : "StartAttack");
        }
    }

}
