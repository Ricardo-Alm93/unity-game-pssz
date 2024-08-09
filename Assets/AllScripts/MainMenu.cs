using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame_Action()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next Scene, in this case the first Map
    }
    public void Go_toSettingsMenu_Action() // in a case of create an settings menu_Scene
    {
        SceneManager.LoadScene("settingsMenu");
    }

    public void Go_toMainMenu_Action() // in a case of back to Main menu from an settings menu_Scene
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame_Action()
    {
        Application.Quit();

        // the next line also quit in the application if the editor is running

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
