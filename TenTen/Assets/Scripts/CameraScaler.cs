using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] private int _paddingHeight;
    [SerializeField] private int _paddingWidth;
    private int _boardHeight = 10;
    private int _boardWidth = 10;
    private int _sceneHeight;
    private int _sceneWidth;

    private void Update()
    {   
        _sceneHeight = _paddingHeight + _boardHeight;
        _sceneWidth = _paddingWidth + _boardWidth;

        float screenRation = (float)Screen.width / (float)Screen.height;
        float targetRation = _sceneWidth / _sceneHeight;

        if (screenRation >= targetRation)
        {
            Camera.main.orthographicSize = _sceneHeight / 2;
        }
        else
        {
            float differenceInSize = targetRation / screenRation;
            Camera.main.orthographicSize = _sceneHeight / 2 * differenceInSize;
        }
    }
}
