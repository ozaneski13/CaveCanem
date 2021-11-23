using UnityEngine;

public class Enemy_StopAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;

    public void ResetAnim()
    {
        _animator.Rebind();
        _animator.Update(1f);
    }
}