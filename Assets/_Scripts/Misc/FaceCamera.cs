using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationCorrection = new Vector3(0f, 180f, 0f);
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_mainCamera == null)
        {
            Debug.LogError("Main camera reference is missing!");
            return;
        }
        Vector3 lookAtDirection = _mainCamera.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookAtDirection, Vector3.up);
        transform.rotation = Quaternion.Euler(rotation.eulerAngles + _rotationCorrection);
    }
}