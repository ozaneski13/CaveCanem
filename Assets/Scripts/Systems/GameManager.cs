using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}