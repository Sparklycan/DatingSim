using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    // <summary>
    /// Get or Set a property of a Move
    /// </summary>
    [CommandInfo("Property",
                 "Move",
                 "Get or Set a property of a Move")]
    [AddComponentMenu("")]
    public class MoveProperty : BaseVariableProperty
    {
        //generated property
        public enum Property
        {
            Character,
            WaitTurns,
            Ability
        }


        [SerializeField]
        protected Property property;

        [SerializeField]
        [VariableProperty(typeof(MoveVariable))]
        protected MoveVariable moveVar;

        [SerializeField]
        [VariableProperty(typeof(CharacterClassVariable),
                          typeof(IntegerVariable),
                          typeof(AbilityVariable))]
        protected Variable inOutVar;

        public override void OnEnter()
        {
            var ioc = inOutVar as CharacterClassVariable;
            var ioi = inOutVar as IntegerVariable;
            var ioa = inOutVar as AbilityVariable;


            var target = moveVar.Value;

            switch (getOrSet)
            {
                case GetSet.Get:
                    switch (property)
                    {
                        case Property.Character:
                            ioc.Value = target.character;
                            break;
                        case Property.WaitTurns:
                            ioi.Value = target.waitTurns;
                            break;
                        case Property.Ability:
                            ioa.Value = target.ability;
                            break;
                        default:
                            Debug.Log("Unsupported get or set attempted");
                            break;
                    }

                    break;
                case GetSet.Set:
                    switch (property)
                    {
                        case Property.WaitTurns:
                            target.waitTurns = ioi.Value;
                            break;
                        default:
                            Debug.Log("Unsupported get or set attempted");
                            break;
                    }

                    break;
                default:
                    break;
            }

            Continue();
        }

        public override string GetSummary()
        {
            if (moveVar == null)
            {
                return "Error: no moveVar set";
            }
            if (inOutVar == null)
            {
                return "Error: no variable set to push or pull data to or from";
            }

            return getOrSet.ToString() + " " + property.ToString();
        }

        public override Color GetButtonColor()
        {
            return new Color32(235, 191, 217, 255);
        }

        public override bool HasReference(Variable variable)
        {
            if (moveVar == variable || inOutVar == variable)
                return true;

            return false;
        }

    }
}