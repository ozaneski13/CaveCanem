using UnityEngine;
using UnityEngine.SceneManagement;

//Integrate option menu volume slider.

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Next scene loading.");

        SceneManager.LoadScene(SaveSystem.LoadPlayer()._level);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");

        Application.Quit();
    }
}