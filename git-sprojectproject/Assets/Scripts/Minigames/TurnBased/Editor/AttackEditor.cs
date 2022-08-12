using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Attack), true)]
public class AttackEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Attack attack = target as Attack;

        if (!Application.isPlaying || attack == null)
            return;

        GUI.enabled = !attack.IsAttacking;
        if (GUILayout.Button("Attack"))
            attack.StartAttack(false);
        GUI.enabled = true;
    }

}
