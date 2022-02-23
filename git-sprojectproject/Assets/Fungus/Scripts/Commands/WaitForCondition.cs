using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    public abstract class WaitForCondition : Condition
    {
        protected override void EvaluateAndContinue()
        {
            StartCoroutine("WaitForSelection");
        }

        protected virtual IEnumerator WaitForSelection()
        {
            PreEvaluate();

            bool? condition;
            do
            {
                yield return null;
                condition = EvaluateCondition();
            } while (condition == null);

            if (condition == true)
                OnTrue();
            else
                OnFalse();

            yield return null;
        }
    }

}