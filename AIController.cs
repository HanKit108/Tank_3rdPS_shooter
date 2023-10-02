using UnityEngine;

public class AIController : MonoBehaviour, ITankController
{
    [SerializeField] private Transform _targetPos;

    [SerializeField] private float sensorLength = 2;
    [SerializeField] private float _forwardRayMultiplier = 1;
    [SerializeField] private float _basicRaycastWeight = 1f; 
    [SerializeField] private float lerp = 10, closestDistance = 3;

    [SerializeField] private bool isActive = true;

    private Vector3 _vectorToTarget, _target, _start;
    private float _width, _length, _forward, _steering;
    private RaycastHit rightHit, rightHit2, rightHit3, leftHit, leftHit2, leftHit3;
    private TankBody _bodyScript;
    
    private void Awake()
    {
        _bodyScript = GetComponentInParent<TankBody>();
    }

    void Start()
    {
        _width = _bodyScript.CollisionWidth;
        _length = _bodyScript.CollisionLength;
    }

    void Update()
    {
        DrawGismo();
    }

    public Vector2 GetMoveDirection()
    {
        if(isActive)
        {
            RaycastHit frontHit;
            _target = new Vector3(_targetPos.position.x, _targetPos.position.y, _targetPos.position.z);
            _vectorToTarget = transform.InverseTransformPoint(_target);
            _start = transform.position + transform.forward * _length / 2;

            Vector3 distance = _targetPos.position - transform.position;
            if(distance.magnitude > closestDistance)
                _forward = Mathf.Lerp(_forward, 1, lerp * Time.deltaTime);
            else
                _forward = Mathf.Lerp(_forward, -1, lerp * Time.deltaTime);

            if(Physics.Raycast(_start, transform.forward, out frontHit, sensorLength * _forwardRayMultiplier)
                || Physics.Raycast(_start + transform.right * _width / 2, transform.forward, out rightHit, sensorLength * _forwardRayMultiplier)
                        || Physics.Raycast(_start - transform.right * _width / 2, transform.forward, out leftHit, sensorLength * _forwardRayMultiplier) )
            {
                float weight = MinWeight(frontHit.distance, rightHit.distance, leftHit.distance);
                _forward -= _basicRaycastWeight * NormalizeHitDistance(weight, sensorLength, 0.5f);
            }
            
            _forward = Mathf.Clamp(_forward, -1f, 1f);
            _steering = Mathf.Lerp(_steering, Mathf.Clamp(_vectorToTarget.x + AvoidAnObstacle(), -1f, 1f), lerp * Time.deltaTime);
            return new Vector2(_steering, _forward);
        }
        return new Vector2(0, 0);
    }

    private void DrawGismo()
    {
        Debug.DrawRay(_start + transform.right * _width / 2, transform.forward * sensorLength, Color.yellow);
        Debug.DrawRay(_start - transform.right * _width / 2, transform.forward * sensorLength, Color.yellow);

        Debug.DrawRay(_start + transform.right * _width / 2, (transform.forward + transform.right) * sensorLength, Color.yellow);
        Debug.DrawRay(_start - transform.right * _width / 2, (transform.forward - transform.right) * sensorLength, Color.yellow);
        Debug.DrawRay(transform.position + transform.right * _width / 2, transform.forward + transform.right * sensorLength / 2, Color.yellow);
        Debug.DrawRay(transform.position - transform.right * _width / 2, transform.forward - transform.right * sensorLength / 2, Color.yellow);
    }

    private float AvoidAnObstacle()
    {
        float rightW, rightW2, rightW3, leftW, leftW2, leftW3;

        rightW = GetRaycastWeight(_start - transform.right * _width / 2, transform.forward, rightHit, sensorLength, 0.5f);
        rightW2 = GetRaycastWeight(_start - transform.right * _width / 2, transform.forward - transform.right, rightHit2, sensorLength, 0.5f);
        rightW3 = GetRaycastWeight(transform.position - transform.right * _width / 2, transform.forward - transform.right, rightHit3, sensorLength / 2, 1);

        leftW = GetRaycastWeight(_start + transform.right * _width / 2, transform.forward, leftHit, sensorLength, 0.5f);
        leftW2 = GetRaycastWeight(_start + transform.right * _width / 2, transform.forward + transform.right, leftHit2, sensorLength, 0.5f);
        leftW3 = GetRaycastWeight(transform.position + transform.right * _width / 2, transform.forward + transform.right, leftHit3, sensorLength / 2, 1);

        return (_basicRaycastWeight) * (rightW + rightW2 + rightW3) - (_basicRaycastWeight) * (leftW + leftW2 + leftW3);
    }

    private float GetRaycastWeight(Vector3 origin, Vector3 direction, RaycastHit hitinfo, float maxDistance, float offset)
    {
        if(Physics.Raycast(origin, direction, out hitinfo, maxDistance))
            return NormalizeHitDistance(hitinfo.distance, maxDistance, offset);
        else 
            return 0;
    }

    private float NormalizeHitDistance(float distance, float maxDistance, float offset)
    {
        if(distance > maxDistance * offset)
            return (maxDistance - distance) / (maxDistance * offset);
        else
            return 1;
    }

    private float MinWeight(float w1, float w2, float w3)
    {
        float min = w1;
        if(w2 < w1) min = w2;
        if(w3 < min) min = w3;

        return min;
    }
}
