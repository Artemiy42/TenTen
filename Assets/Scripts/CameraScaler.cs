using UnityEngine;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
public class CameraScaler : MonoBehaviour
{
    [SerializeField] private Vector2 _referenceResolution = new Vector2(1080, 1920);
    [SerializeField] private float _defaultOrthograpthicSize = 5;
    [SerializeField] private float _minOrthograpthicSize = 2;
    [SerializeField] private float _defaultYPosition;

    private Camera _camera;
    private float _referenceAspect;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _referenceAspect = _referenceResolution.x / _referenceResolution.y;
    }

    private void LateUpdate()
    {
        ScaleOrthographic();
        CorrectPosition();
    }

    private void ScaleOrthographic()
    {
        float constantWidthSize = _defaultOrthograpthicSize * (_referenceAspect / _camera.aspect);
        _camera.orthographicSize = Mathf.Max(constantWidthSize, _minOrthograpthicSize);
    }

    private void CorrectPosition()
    {
        float constantYPosition = _defaultYPosition * (_referenceAspect / _camera.aspect);
        Vector3 position = transform.position;
        position.y = constantYPosition;
        transform.position = position;
    }
}