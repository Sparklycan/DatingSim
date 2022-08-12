using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackTargetState : StateMachineBehaviour
{

    private bool hasStarted = false;

    private NewAttack attack;
    private CharacterClass attacker;
    private CharacterClass target;

    public bool damageAllTargets = false;

    public string damageTargetEvent = "DamageTarget";
    public string damageAttackerEvent = "DamageAttacker";

    public string healTargetEvent = "HealTarget";
    public string healAttackerEvent = "HealAttacker";

    [Space]
    public bool stunAttacker = false;
    public bool stunTarget = false;

    [Space]
    public string attackerIdleStateName = "Idle";
    public string targetIdleStateName = "Idle";
    public string attackerTrigger;
    public string targetTrigger;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasStarted = false;
        attack = animator.GetComponent<NewAttack>();
        int targetID = animator.GetInteger("Target");

        attacker = attack.Attacker;
        target = attack.Targets[targetID];

        Damages damages = attack.GetComponent<Damages>();

        AnimationUtilities utilities = attacker.GetComponent<AnimationUtilities>();
        IEnumerable<CharacterClass> attackers = Enumerable.Repeat(attacker, 1);
        IEnumerable<CharacterClass> targets = damageAllTargets ? attack.Targets : Enumerable.Repeat(target, 1);
        RegisterDamageEvent(attacker, attackers, damages, utilities, damageAttackerEvent, stunAttacker, null, attack.targetDeathState);
        RegisterDamageEvent(attacker, targets, damages, utilities, damageTargetEvent, stunTarget, null, attack.targetDeathState);
        RegisterHealEvent(attackers, damages, utilities, healAttackerEvent);
        RegisterHealEvent(targets, damages, utilities, healTargetEvent);

        for (int i = damageAllTargets ? 0 : targetID; i < (damageAllTargets ? attack.Targets.Length : targetID + 1); i++)
        {
            target = attack.Targets[i];
            targets = damageAllTargets ? attack.Targets : Enumerable.Repeat(target, 1);

            utilities = target.GetComponent<AnimationUtilities>();
            RegisterDamageEvent(attacker, targets, damages, utilities, damageTargetEvent, stunTarget, null, attack.targetDeathState);
            RegisterDamageEvent(attacker, attackers, damages, utilities, damageAttackerEvent, stunAttacker, null, attack.targetDeathState);
            RegisterHealEvent(targets, damages, utilities, healTargetEvent);
            RegisterHealEvent(attackers, damages, utilities, healAttackerEvent);

            target.Animator.SetTrigger(targetTrigger);
        }

        attacker.Animator.SetTrigger(attackerTrigger);
    }

    private void RegisterDamageEvent(CharacterClass attacker, IEnumerable<CharacterClass> targets, Damages damages, AnimationUtilities utilities, string eventName, bool stun, string hurtAnimation, string deathAnimation)
    {
        if (string.IsNullOrWhiteSpace(eventName))
            return;

        utilities.RegisterEvent(eventName, () =>
        {
            foreach (CharacterClass target in targets)
            {
                if (stun)
                {
                    Stunned stunned = target.gameObject.AddComponent<Stunned>();
                    if (stunned != null)
                        stunned.moves = attack.Moves;
                }
                else
                {
                    Stunned stunned = target.GetComponent<Stunned>();
                    if (stunned != null)
                        Destroy(stunned);
                }

                target.DoDamage(attacker, damages.GetDamage(target), hurtAnimation, deathAnimation);
            }
            utilities.UnregisterEvent(eventName);
        });
    }

    private void RegisterHealEvent(IEnumerable<CharacterClass> targets, Damages damages, AnimationUtilities utilities, string eventName)
    {
        if (string.IsNullOrWhiteSpace(eventName))
            return;

        utilities.RegisterEvent(eventName, () =>
        {
            foreach (CharacterClass target in targets)
                target.Heal(damages.GetHeal(target));
            utilities.UnregisterEvent(eventName);
        });
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!hasStarted)
        {
            if (attacker == null)
                hasStarted = true;
            else if (!string.IsNullOrWhiteSpace(attackerIdleStateName) && !attacker.Animator.GetCurrentAnimatorStateInfo(0).IsName(attackerIdleStateName))
                hasStarted = true;
            else 
                return;
        }

        if (attacker != null && !string.IsNullOrWhiteSpace(attackerIdleStateName) && !attacker.Animator.GetCurrentAnimatorStateInfo(0).IsName(attackerIdleStateName))
            return;
        
        int targetID = animator.GetInteger("Target");
        for (int i = damageAllTargets ? 0 : targetID; i < (damageAllTargets ? attack.Targets.Length : targetID + 1); i++)
        {
            target = attack.Targets[i];

            if (target == null)
                continue;

            if (string.IsNullOrWhiteSpace(targetIdleStateName))
                continue;

            if (!target.Animator.GetCurrentAnimatorStateInfo(0).IsName(targetIdleStateName))
                return;
        }


        AnimationUtilities utilities = attacker.GetComponent<AnimationUtilities>();
        utilities.UnregisterEvent(damageTargetEvent);
        utilities.UnregisterEvent(damageAttackerEvent);
        utilities.UnregisterEvent(healTargetEvent);
        utilities.UnregisterEvent(healAttackerEvent);

        for (int i = damageAllTargets ? 0 : targetID; i < (damageAllTargets ? attack.Targets.Length : targetID + 1); i++)
        {
            target = attack.Targets[i];
            if (target == null)
                continue;

            utilities = target.GetComponent<AnimationUtilities>();
            utilities.UnregisterEvent(damageTargetEvent);
            utilities.UnregisterEvent(damageAttackerEvent);
            utilities.UnregisterEvent(healTargetEvent);
            utilities.UnregisterEvent(healAttackerEvent);
        }

        animator.SetTrigger("DoneAttacking");
    }

}
