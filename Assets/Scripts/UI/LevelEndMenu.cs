using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndMenu : MonoBehaviour
{
    //[SerializeField] private GameObject _marketUI = null;

    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private float _timeScale = 0.2f;

    private GameObject _levelEndUI = null;

    private void Awake()
    {
        _levelEndUI = gameObject;

        Time.timeScale = _timeScale;

        UpdatePlayer();
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading next level.");

        SceneManager.LoadScene(Player.Instance.Level);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");

        Application.Quit();
    }

    public void OpenMarket()
    {
        _levelEndUI.SetActive(false);
        //_marketUI.SetActive(true);
    }

    private void UpdatePlayer()
    {
        Player.Instance.Level++;
        Player.Instance.Coin += _collectableListener.CollectableCount;

        SaveSystem.SavePlayer(Player.Instance);
    }
}