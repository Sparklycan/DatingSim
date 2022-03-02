using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenPage(GameObject page)
    {
        page.SetActive(true);
    }

    public void ClosePage(GameObject page)
    {
        page.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
