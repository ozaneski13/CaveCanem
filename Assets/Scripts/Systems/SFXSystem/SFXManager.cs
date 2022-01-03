using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _auidoSources = null;

    public static SFXManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    public AudioSource GetBark()
    {
        return _auidoSources[0];
    }

    public AudioSource GetBreathing()
    {
        return _auidoSources[1];
    }

    public AudioSource GetGrowling()
    {
        return _auidoSources[2];
    }

    public AudioSource GetHumanRun()
    {
        return _auidoSources[3];
    }

    public AudioSource GetHumanScream()
    {
        int rand = Random.Range(4, 8);

        return _auidoSources[rand];
    }

    public AudioSource GetHumanWalk()
    {
        int rand = Random.Range(8, 10);

        return _auidoSources[rand];
    }
}