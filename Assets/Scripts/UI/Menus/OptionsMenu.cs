using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider _slider = null;

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("volume", _slider.value);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}