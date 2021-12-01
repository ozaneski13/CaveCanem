using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeathMenu : MonoBehaviour
{
    [SerializeField] private Image _playerDeathMenuImage = null;
    [SerializeField] private GameObject _tryAgainButton = null;
    [SerializeField] private GameObject _quitGameButton = null;

    private void Start()
    {
        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        Player.Instance.OnKill += OnPlayerKill;
    }

    private void UnregisterFromEvents()
    {
        if (Player.Instance != null)
            Player.Instance.OnKill -= OnPlayerKill;
    }

    public void LoadLevelAgain()
    {
        Debug.Log("Reloading current level.");

        Scene currentScene = SceneManager.GetActiveScene();

        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");

        Application.Quit();
    }

    private void OnPlayerKill()
    {
        Time.timeScale = 0f;

        _playerDeathMenuImage.enabled = true;
        _tryAgainButton.SetActive(true);
        _quitGameButton.SetActive(true);
    }
}