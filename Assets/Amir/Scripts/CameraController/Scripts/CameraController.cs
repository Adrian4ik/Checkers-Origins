using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
        [SerializeField]
    private List<Transform> _targetTransforms = new ();
        [SerializeField]
    private float _desiredDistance = 10;

        [Space]
        [SerializeField]
    private bool _smoothFollow = true;
        [SerializeField,
        Range(0, 50)]
    private int _smoothSpeed = 4;

        [Space]
        [SerializeField]
    private bool _showMesh = true;
        [SerializeField]
    private Color _meshColor = new Color(1, 1, 1, 0.25f);
        [SerializeField,
        Min(0)]
    private float _meshSize = 0.2f;
        [SerializeField]
    private bool _showRealFOV = false;

    private Transform _childTransform; // временно заменено с _cameraTransform для использования не только с камерой
    private Mesh _cameraMesh;
    private Camera _cameraComponent;
    private Vector3 _startPosition,
                    _startChildPosition;
    private float _aspectRatioOffset = 2;

    public List<Transform> TargetTransforms =>  _targetTransforms;



    //private void OnEnable() { GetStartPositions(); }

    private void Awake() { GetStartPositions(); }

    private void OnDrawGizmos()
    {
        if (_childTransform == null)
            _childTransform = transform.GetChild(0).transform;

        if (!_showMesh)
            return;

        Gizmos.color = _meshColor;

        if (_cameraComponent == null)
            if (!_childTransform.TryGetComponent(out _cameraComponent))
                return;

        DrawCameraMesh();
        DrawFOVMesh();
        DrawConnectingLines();

        if (_childTransform.localPosition.z >= -0.01f && _targetTransforms.Count <= 1)
            return;

        DrawLookingPoint();
    }

    private void LateUpdate()
    {
        float speed = _smoothSpeed * Time.deltaTime;

        if (_childTransform == null)
            _childTransform = transform.GetChild(0).transform;

        _childTransform.localRotation = Quaternion.Slerp(_childTransform.localRotation,
                                                         new Quaternion(0, 0, 0, 1),
                                                         speed);

        if (_targetTransforms.Count < 1)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startPosition, speed);
            _childTransform.localPosition = Vector3.Lerp(_childTransform.localPosition, _startChildPosition, speed);

            return;
        }

        if (_targetTransforms.Count > 2) // зарезервировано на будущее
            return;

        Vector3 target = _targetTransforms[0].position,
                offset = new Vector3(0, 0, -_desiredDistance);

        if (_targetTransforms.Count > 1)
        {
            float distance = Vector3.Distance(_targetTransforms[0].position,
                                              _targetTransforms[1].position);

            target = GetCentralPosition(distance);
            offset = GetCameraDistance(distance);
        }

        transform.position = Vector3.Lerp(transform.position, target, speed);
        _childTransform.localPosition = Vector3.Lerp(_childTransform.localPosition, offset, speed);
    }



    private void GetStartPositions()
    {
        _startPosition = transform.localPosition;

        if (_childTransform == null)
            return;

        _childTransform = transform.GetChild(0).transform;
        _startChildPosition = _childTransform.localPosition;
    }

    private void DrawCameraMesh()
    {
        if (_cameraMesh == null)
            _cameraMesh = Resources.Load<Mesh>("Meshes/camera");

        Gizmos.DrawMesh(_cameraMesh,
                        _childTransform.position,
                        _childTransform.rotation,
                        Vector3.one * _meshSize);
    }

    private void DrawFOVMesh()
    {
        _aspectRatioOffset = _cameraComponent.aspect;

        Gizmos.DrawMesh(GetFOVMesh(), _childTransform.position, _childTransform.rotation); // transform.position, transform.forward);
    }

    private void DrawConnectingLines()
    {
        if (_targetTransforms.Count > 2) // зарезервировано на будущее
            return;

        Gizmos.color = Color.yellow;

        if (_targetTransforms.Count > 0)
            Gizmos.DrawLine(transform.position, _targetTransforms[0].position);
        //else
            //Gizmos.DrawLine(transform.position, transform.parent.position + _startPosition);

        //Debug.Log(_startPosition);

        if (_targetTransforms.Count > 1)
        {
            Gizmos.DrawLine(transform.position, _targetTransforms[1].position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(_targetTransforms[0].position, _targetTransforms[1].position);
        }
    }

    private void DrawLookingPoint()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawSphere(transform.position, _meshSize / 4);
        Gizmos.DrawLine(transform.position, _childTransform.position);
    }

    private Mesh GetFOVMesh()
    {
        Mesh fov_mesh = new Mesh();

        float ncp = _cameraComponent.nearClipPlane - 0.01f,
              fcp = Mathf.Clamp(_meshSize * 20, 2, _cameraComponent.farClipPlane),
              tan = Mathf.Tan(_cameraComponent.fieldOfView / 2 * Mathf.Deg2Rad);

        float near_h = ncp * tan,
              near_w = near_h * _aspectRatioOffset;

        float far_h = _showRealFOV? fcp * tan : _meshSize * 4,
              far_w = far_h * _aspectRatioOffset;

        fov_mesh.vertices = new Vector3[]
        {
            // face
            new Vector3(-near_w, -near_h, ncp),
            new Vector3(-near_w,  near_h, ncp),
            new Vector3( near_w,  near_h, ncp),
            new Vector3( near_w, -near_h, ncp),

            // left
            new Vector3(-near_w, -near_h, ncp),
            new Vector3(-far_w, -far_h, fcp),
            new Vector3(-far_w,  far_h, fcp),
            new Vector3(-near_w,  near_h, ncp),

            // top
            new Vector3(-near_w, near_h, ncp),
            new Vector3(-far_w, far_h, fcp),
            new Vector3( far_w, far_h, fcp),
            new Vector3( near_w, near_h, ncp),

            // right
            new Vector3(near_w,  near_h, ncp),
            new Vector3(far_w,  far_h, fcp),
            new Vector3(far_w, -far_h, fcp),
            new Vector3(near_w, -near_h, ncp),

            // bottom
            new Vector3( near_w, -near_h, ncp),
            new Vector3( far_w, -far_h, fcp),
            new Vector3(-far_w, -far_h, fcp),
            new Vector3(-near_w, -near_h, ncp),

            // infinity
            new Vector3( far_w, -far_h, fcp),
            new Vector3( far_w,  far_h, fcp),
            new Vector3(-far_w,  far_h, fcp),
            new Vector3(-far_w, -far_h, fcp),
        };

        fov_mesh.triangles = new int[]
        {
            // перед
            0, 1, 3,
            3, 1, 2,

            // л бок
            4, 5, 7,
            7, 5, 6,

            // верх
            8, 9, 11,
            11, 9, 10,

            // п бок
            12, 13, 15,
            15, 13, 14,

            // низ
            16, 17, 19,
            19, 17, 18,

            // зад
            20, 21, 23,
            23, 21, 22,
        };

        fov_mesh.RecalculateNormals();
        fov_mesh.RecalculateBounds();

        return fov_mesh;
    }

    private Vector3 GetCameraDistance(float _distance)
    {
        if (_distance >= _desiredDistance - _aspectRatioOffset)
            return new Vector3(0, 0, -_distance - _aspectRatioOffset);
        else
            return new Vector3(0, 0, -_desiredDistance);
    }

    private Vector3 GetCentralPosition(float _distance)
    {
        Vector3 direction_move = _targetTransforms[0].position - _targetTransforms[1].position;

        return _targetTransforms[0].position - direction_move.normalized * _distance / 2;
    }



    public void AddTargetTransform(Transform new_transform)
    {
        _targetTransforms.Add(new_transform);
    }

    public void RemoveLastTransform()
    {
        if (_targetTransforms.Count > 0)
            _targetTransforms.RemoveAt(_targetTransforms.Count - 1);
    }

    public void RemoveTransformByIndex(int index)
    {
        if (index > 0 && index < _targetTransforms.Count)
            _targetTransforms.RemoveAt(index);
    }

    public void RemoveTargetTransform(Transform target_transform)
    {
        if (_targetTransforms.Contains(target_transform))
            _targetTransforms.Remove(target_transform);
    }

    public void ShowMesh(bool state)
    {
        _showMesh = state;
    }

    /*public void SetPlayerTransform(Transform player_transform)
    {
        _playerTransform = player_transform;
    }

    public void SetTargetTransform(Transform target_transform)
    {
        _targetTransform = target_transform;
    }*/
}
