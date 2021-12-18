using System.Collections;
using UnityEngine;
using System;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _popUps = null;

    [SerializeField] private Player_Movement _playerMovement = null;
    [SerializeField] private Player_Flashlight _playerFlashlight = null;
    [SerializeField] private Player_Collect _playerCollect = null;

    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private Enemy_Movement _enemyMovement = null;

    [SerializeField] private float _waitTime = 2f;

    private IEnumerator _tutorialRoutine = null;

    private int _popUpIndex = 0;
    private int _dummyIndex = 0;

    private bool _isCollected = false;

    public Action OnTutorialCompleted;

    private void Start()
    {
        _collectableListener.OnCollectableCollected += Collected;

        _tutorialRoutine = TutorialRoutine();
        StartCoroutine(_tutorialRoutine);
    }

    private void OnDestroy()
    {
        if (_collectableListener != null)
            _collectableListener.OnCollectableCollected -= Collected;

        if (_tutorialRoutine != null)
            StopCoroutine(_tutorialRoutine);
    }

    private IEnumerator TutorialRoutine()
    {
        while(true)
        {
            if (_popUpIndex == 0)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _playerMovement.CanDo(_popUpIndex);
                    _popUpIndex++;
                }
            }

            else if (_popUpIndex == 1)
            {
                _popUps[_popUpIndex - 1].SetActive(false);
                _popUps[_popUpIndex].SetActive(true);

                _playerCollect.CanCollect();

                if (_isCollected)
                    _popUpIndex++;
            }

            else if (_popUpIndex == 2 && _isCollected)
            {
                _popUps[_popUpIndex - 1].SetActive(false);
                _popUps[_popUpIndex].SetActive(true);

                _playerMovement.CanDo(_popUpIndex);

                if (Input.GetKeyDown(KeyCode.Space))
                    _popUpIndex++;
            }

            else if (_popUpIndex == 3)
            {
                _popUps[_popUpIndex - 1].SetActive(false);
                _popUps[_popUpIndex].SetActive(true);

                _playerMovement.CanDo(_popUpIndex);

                if (Input.GetKeyDown(KeyCode.LeftShift))
                    _popUpIndex++;
            }

            else if (_popUpIndex == 4)
            {
                _popUps[_popUpIndex - 1].SetActive(false);
                _popUps[_popUpIndex].SetActive(true);

                _playerMovement.CanDo(_popUpIndex);

                if (Input.GetKeyDown(KeyCode.LeftControl))
                    _popUpIndex++;
            }

            else if (_popUpIndex == 5)
            {
                _popUps[_popUpIndex - 1].SetActive(false);
                _popUps[_popUpIndex].SetActive(true);

                _playerFlashlight.CanUse();

                if (Input.GetKeyDown(KeyCode.F))
                    _popUpIndex++;
            }

            else if (_popUpIndex == 6)//Feed
            {
                _popUps[_popUpIndex - 1].SetActive(false);
                _popUps[_popUpIndex].SetActive(true);

                if (Input.GetKeyDown(KeyCode.T))
                    _popUpIndex++;
            }

            else if (_popUpIndex == 7)
            {
                _popUps[_popUpIndex - 1].SetActive(false);
                _popUps[_popUpIndex].SetActive(true);

                _enemyMovement.CanMove();
                _popUpIndex++;
            }

            else
            {
                yield return new WaitForSeconds(_waitTime);
                _popUps[_popUpIndex - 1].SetActive(false);

                break;
            }

            if (_popUpIndex != _dummyIndex)
            {
                OnTutorialCompleted?.Invoke();

                _dummyIndex = _popUpIndex;
                yield return new WaitForSeconds(_waitTime);
            }

            yield return null;
        }
    }

    public void Collected() => _isCollected = true;
}