using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndMenu : MonoBehaviour
{
    //[SerializeField] private GameObject _marketUI = null;

    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private GameObject _tutorials = null;

    [SerializeField] private float _timeScale = 0.2f;

    private void Awake()
    {
        _tutorials.SetActive(false);

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
        //_marketUI.SetActive(true);
        gameObject.SetActive(false);
    }

    private void UpdatePlayer()
    {
        Player.Instance.Level++;
        Player.Instance.Coin += _collectableListener.CoinCount;

        SaveSystem.SavePlayer(Player.Instance);
    }
}