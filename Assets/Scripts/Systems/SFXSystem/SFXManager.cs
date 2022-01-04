using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources = null;

    public static SFXManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    public AudioSource GetDogBark()
    {
        return _audioSources[0];
    }

    public AudioSource GetDogBreath()
    {
        return _audioSources[1];
    }

    public AudioSource GetDogEat()
    {
        return _audioSources[2];
    }

    public AudioSource GetDogGrowl()
    {
        return _audioSources[3];
    }

    public AudioSource GetFlashlightClick()
    {
        return _audioSources[4];
    }

    public AudioSource GetHumanRun()
    {
        return _audioSources[5];
    }

    public AudioSource GetHumanScream()
    {
        int rand = Random.Range(6, 10);

        return _audioSources[rand];
    }

    public AudioSource GetHumanWalk()
    {
        int rand = Random.Range(10, 12);

        return _audioSources[rand];
    }

    public AudioSource GetMoneySpend()
    {
        return _audioSources[12];
    }
}