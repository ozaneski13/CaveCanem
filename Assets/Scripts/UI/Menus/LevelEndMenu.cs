using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelEndMenu : MonoBehaviour
{
    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private GameObject _tutorials = null;

    [SerializeField] private float _timeScale = 0.2f;

    private Player _player = null;

    public Action OnLevelEnded;

    public static LevelEndMenu Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        OnLevelEnded?.Invoke();

        _tutorials.SetActive(false);

        Time.timeScale = _timeScale;

        _player = Player.Instance;

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

    private void UpdatePlayer()
    {
        _player.Level++;
        _player.Coin += _collectableListener.CoinCount;

        SaveSystem.SavePlayer(Player.Instance);
    }
}