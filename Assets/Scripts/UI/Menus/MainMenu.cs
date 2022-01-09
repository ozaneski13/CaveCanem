using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Player player = new Player();

        player.Coin = 0;
        player.Level = 1;
        player.MaximumHealth = 100;
        player.HealthCount = 0;
        player.BoneCount = 0;
        player.FoodCount = 0;

        SaveSystem.SavePlayer(player);
    }

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