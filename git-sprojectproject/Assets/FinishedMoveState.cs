using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedMoveState : StateMachineBehaviour
{
    private NewAttack attack = null;
    public string idleStateName = "Idle";
    public string attackerTrigger = "Idle";

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = animator.GetComponent<NewAttack>();
        animator.transform.position = attack.Attacker.transform.position;
        attack.Attacker.Animator.SetTrigger(attackerTrigger);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attack.Attacker.Animator.GetCurrentAnimatorStateInfo(0).IsName(idleStateName))
        {
            animator.SetTrigger("DoneAttacking");
            attack.FinishAttack();
        }
    }

}
