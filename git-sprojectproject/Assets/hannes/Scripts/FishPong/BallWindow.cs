using UnityEngine;
using UnityEditor;


public class BallWindow : EditorWindow
{
    public Ballhandler ballhandler;

    float minSize, maxSize;




    [MenuItem("Window/Balls")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<BallWindow>("BallHandler");
    }



private void OnGUI()
    {

        // window code
        ballhandler = GameObject.FindGameObjectWithTag("BallHandler").GetComponent<Ballhandler>();


        GUILayout.Label("Window for FishingMinigame", EditorStyles.whiteLargeLabel);

        GUILayout.Space(20);



        GUILayout.Label("Balls", EditorStyles.boldLabel);
        ballhandler.ballAmount =  EditorGUILayout.IntSlider(ballhandler.ballAmount, 1, 20);

        if (GUILayout.Button("Change!"))
        {
               ballhandler.BallCheck();
        }


        GUILayout.Space(40);
        /*
        GUILayout.Label("MinSize");
        minSize = EditorGUILayout.Slider( minSize, 1 , 10);

        GUILayout.Space(5);

        GUILayout.Label("MaxSize");
        maxSize = EditorGUILayout.Slider(maxSize, 1, 10);
        if (maxSize < minSize)
        {
            minSize = maxSize;
        }

        GUILayout.Space(20);
        */

        /*
        BallType ball = null;

        if (Selection.activeGameObject.GetComponent<BallType>() != null)
        {
            ball = Selection.activeGameObject.GetComponent<BallType>();
        }


        if (ball)
        {
            ball.dropDown = (BallType.Value)EditorGUILayout.EnumPopup(ball.dropDown);


            ball.Sus = EditorGUILayout.ColorField("Sus", ball.Sus);
            ball.Lust = EditorGUILayout.ColorField("Lust", ball.Lust);
            ball.Love = EditorGUILayout.ColorField("Love", ball.Love);
            ball.Neutral = EditorGUILayout.ColorField("Neutral", ball.Neutral);


            if (GUILayout.Button("Change!"))
            {
                ball.Change(ball.dropDown);
            }
        }

        */
    }


    /*
    public override void OnInspectorGUI()
    {


        // calling the original GUI
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Change Type"))
        {
            Debug.Log("buttonPress");
        }
        


        GUILayout.EndHorizontal();

    }
    */

}
