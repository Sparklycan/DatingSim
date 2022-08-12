using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : StateMachineBehaviour
{

    private NewAttack attack = null;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startMoveTime;

    public string attackerTrigger;
    public float moveTime = 5.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = animator.GetComponent<NewAttack>();

        startMoveTime = Time.time;
        startPosition = attack.Attacker.transform.position;
        endPosition = animator.transform.position;

        attack.Attacker.Animator.SetTrigger(attackerTrigger);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int targetID = animator.GetInteger("Target");

        float elapsedTime = Time.time - startMoveTime;
        if(elapsedTime >= moveTime)
        {
            attack.Attacker.transform.position = endPosition;
            animator.SetTrigger("ArrivedAtTarget");
        }
        else
        {
            float t = elapsedTime / moveTime;
            attack.Attacker.transform.position = Vector2.Lerp(startPosition, endPosition, t);
        }
    }

}
