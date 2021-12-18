using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBorderCheck : MonoBehaviour
{
    [SerializeField] private GameObject _levelEndUI = null;

    private bool _isTutorialCompleted = true;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
            _isTutorialCompleted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player.Instance.gameObject || !_isTutorialCompleted)
            return;

        _levelEndUI.SetActive(true);
    }

    private void TutorialCompleted() => _isTutorialCompleted = true;
}