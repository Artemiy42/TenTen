using UnityEngine;

namespace CodeBase.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    [DisallowMultipleComponent]
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private Vector2 _referenceResolution = new Vector2(1080, 1920);
        [SerializeField] private float _defaultOrthographicSize = 5;
        [SerializeField] private float _minOrthographicSize = 2;
        [SerializeField] private float _defaultYPosition;

        private UnityEngine.Camera _camera;
        private float _referenceAspect;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _referenceAspect = _referenceResolution.x / _referenceResolution.y;
        }

        private void LateUpdate()
        {
            ScaleOrthographic();
            CorrectPosition();
        }

        private void ScaleOrthographic()
        {
            var constantWidthSize = _defaultOrthographicSize * (_referenceAspect / _camera.aspect);
            _camera.orthographicSize = Mathf.Max(constantWidthSize, _minOrthographicSize);
        }

        private void CorrectPosition()
        {
            var constantYPosition = _defaultYPosition * (_referenceAspect / _camera.aspect);
            var position = transform.position;
            position.y = constantYPosition;
            transform.position = position;
        }
    }
}