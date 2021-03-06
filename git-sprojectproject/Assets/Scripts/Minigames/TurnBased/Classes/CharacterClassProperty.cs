using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    // <summary>
    /// Get or Set a property of a CharacterClass component
    /// </summary>
    [CommandInfo("Property",
                 "CharacterClass",
                 "Get or Set a property of a CharacterClass component")]
    [AddComponentMenu("")]
    public class CharacterClassProperty : BaseVariableProperty
    {
        //generated property
        public enum Property
        {
            Name,
            ClassName,
            CurrentHealth,
            MaxHealth,
            GameObject,
            Animator,
            AttackBuff,
            DefenseBuff
        }


        [SerializeField]
        protected Property property;

        [SerializeField]
        [VariableProperty(typeof(CharacterClassVariable))]
        protected CharacterClassVariable characterClassVar;

        [SerializeField]
        [VariableProperty(typeof(StringVariable),
                          typeof(IntegerVariable),
                          typeof(FloatVariable),
                          typeof(AnimatorVariable),
                          typeof(GameObjectVariable))]
        protected Variable inOutVar;

        public override void OnEnter()
        {
            var ios = inOutVar as StringVariable;
            var ioi = inOutVar as IntegerVariable;
            var iof = inOutVar as FloatVariable;
            var iogo = inOutVar as GameObjectVariable;
            var ioa = inOutVar as AnimatorVariable;


            var target = characterClassVar.Value;

            switch (getOrSet)
            {
                case GetSet.Get:
                    switch (property)
                    {
                        case Property.Name:
                            ios.Value = target == null ? "CharacterClass does not exist" : target.Name;
                            break;
                        case Property.ClassName:
                            ios.Value = target == null ? "CharacterClass does not exist" : target.ClassName;
                            break;
                        case Property.CurrentHealth:
                            ioi.Value = target == null ? 0 : target.CurrentHealth;
                            break;
                        case Property.MaxHealth:
                            ioi.Value = target == null ? 0 : target.MaxHealth;
                            break;
                        case Property.GameObject:
                            iogo.Value = target == null ? null : target.gameObject;
                            break;
                        case Property.Animator:
                            ioa.Value = target == null ? null : target.Animator;
                            break;
                        case Property.AttackBuff:
                            iof.Value = target == null ? 0 : target.AttackBuff;
                            break;
                        case Property.DefenseBuff:
                            iof.Value = target == null ? 0 : target.DefenseBuff;
                            break;
                        default:
                            Debug.Log("Unsupported get or set attempted");
                            break;
                    }

                    break;
                case GetSet.Set:
                    switch (property)
                    {
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
            if (characterClassVar == null)
            {
                return "Error: no characterClassVar set";
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
            if (characterClassVar == variable || inOutVar == variable)
                return true;

            return false;
        }

    }
}