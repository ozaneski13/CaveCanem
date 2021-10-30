using UnityEngine;

public class HealthBar_Bilboard : MonoBehaviour
{
    [SerializeField] private Transform _camera = null;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}