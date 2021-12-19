using UnityEngine;

public abstract class Bilboard : MonoBehaviour
{
    [SerializeField] private Transform _camera = null;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}