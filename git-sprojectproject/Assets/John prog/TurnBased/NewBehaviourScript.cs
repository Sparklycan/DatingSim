using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public void Execute()
    {
		Debug.Log(MoonSharpFactorial());
    }

	double MoonSharpFactorial()
	{
		string scriptCode = @"    
		-- defines a factorial function
		function fact (n)
			if (n == 0) then
				return 1
			else
				return n*fact(n - 1)
			end
		end";

		Script script = new Script();

		script.DoString(scriptCode);

		DynValue res = script.Call(script.Globals["fact"], 4);

		return res.Number;
	}

}
