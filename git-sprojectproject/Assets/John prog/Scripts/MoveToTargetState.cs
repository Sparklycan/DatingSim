using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetState : StateMachineBehaviour
{

    private NewAttack attack = null;
    private Vector2 startPosition;
    private float startMoveTime;

    public string attackerTrigger;
    public float moveTime = 5.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = animator.GetComponent<NewAttack>();

        startMoveTime = Time.time;
        startPosition = attack.Attacker.transform.position;

        int targetID = animator.GetInteger("Target");
        attack.Attacker.Animator.SetTrigger(attackerTrigger);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int targetID = animator.GetInteger("Target");
        Vector2 targetPosition = GetTargetPosition(targetID);

        float elapsedTime = Time.time - startMoveTime;
        if(elapsedTime >= moveTime)
        {
            attack.Attacker.transform.position = targetPosition;
            animator.SetTrigger("ArrivedAtTarget");
        }
        else
        {
            float t = elapsedTime / moveTime;
            attack.Attacker.transform.position = Vector2.Lerp(startPosition, targetPosition, t);
        }
    }

    private CharacterClass GetTarget(int targetID)
    {
        if (targetID >= attack.Targets.Length)
            return null;

        return attack.Targets[targetID];
    }

    private Vector2 GetTargetPosition(int targetID)
    {
        CharacterClass target = GetTarget(targetID);
        if (target == null)
            return (Vector2)startPosition;

        return (Vector2)target.transform.position - attack.Attacker.TargetOffset;
    }

}
