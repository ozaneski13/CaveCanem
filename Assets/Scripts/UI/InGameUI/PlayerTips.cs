using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTips : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController = null;

    [SerializeField] private Player_Feed _playerFeed = null;

    [SerializeField] private Text _tipImageText = null;

    private IEnumerator _tipRoutine = null;

    private void Awake()
    {
        RegisterToEvents();
    }

    private void OnDestroy()
    {
        if (_tipRoutine != null)
            StopCoroutine(_tipRoutine);

        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _enemyController.OnWrongFeed += WrongFeedGivenTip;
        _playerFeed.OnUnnecessaryFoodUsed += OnUnnecessaryFoodUsed;
        _playerFeed.OnInsufficientFeed += OnInsufficientFeed;
    }

    private void UnregisterFromEvents()
    {
        _enemyController.OnWrongFeed -= WrongFeedGivenTip;
        _playerFeed.OnUnnecessaryFoodUsed -= OnUnnecessaryFoodUsed;
        _playerFeed.OnInsufficientFeed += OnInsufficientFeed;
    }

    private void WrongFeedGivenTip(string collectable, EEnemy enemyType)
    {
        string text = "You should feed " + enemyType.ToString() + " type with " + collectable + ".";

        _tipRoutine = TipRoutine(text);
        StartCoroutine(_tipRoutine);
    }

    private void OnUnnecessaryFoodUsed()
    {
        string text = "This guy already looks friendly, no need to feed him again.";

        _tipRoutine = TipRoutine(text);
        StartCoroutine(_tipRoutine);
    }

    private void OnInsufficientFeed(string collectable)
    {
        string text = "I need more " + collectable + ".";

        _tipRoutine = TipRoutine(text);
        StartCoroutine(_tipRoutine);
    }

    private IEnumerator TipRoutine(string text)
    {
        _tipImageText.text = text;
        _tipImageText.transform.parent.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);

        _tipImageText.transform.parent.gameObject.SetActive(false);
    }
}