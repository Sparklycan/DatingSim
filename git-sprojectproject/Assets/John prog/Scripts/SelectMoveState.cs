using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMoveState : StateMachineBehaviour
{

    private NewAttack attack;
    private Fungus.Flowchart flowchart;

    public string attackerTrigger = "Idle";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = animator.GetComponent<NewAttack>();
        attack.Attacker.Animator.SetTrigger(attackerTrigger);

        flowchart = attack.GetComponent<Fungus.Flowchart>();
        flowchart.SendFungusMessage("SelectMove");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (flowchart.GetExecutingBlocks().Count == 0)
            animator.SetTrigger("MoveSelected");
    }
}
