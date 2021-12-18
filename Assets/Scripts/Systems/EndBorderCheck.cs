using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBorderCheck : MonoBehaviour
{
    [SerializeField] private GameObject _levelEndUI = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player.Instance.gameObject)
            return;

        _levelEndUI.SetActive(true);
    }
}