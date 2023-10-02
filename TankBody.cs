using UnityEngine;

public class TankBody : MonoBehaviour
{
    private float _moveSpeed;
    [SerializeField] private TankStatsSO tankStats;
    
    [SerializeField] private float collisionWidth;
    public float CollisionWidth
    {
        get {return collisionWidth;}
    }

    [SerializeField] private float collisionLength;
    public float CollisionLength
    {
        get {return collisionLength;}
    }

    private Vector2 _direction;

    private ITankController _tankController;


    private void Awake()
    {
        _tankController = GetComponentInParent<ITankController>();
        _moveSpeed = tankStats.MoveSpeed;
    }

    void Update()
    {
        _direction = _tankController.GetMoveDirection();

        if(!CheckCollisionFront() && _direction.y > 0 
            || !CheckCollisionBack() && _direction.y < 0
                ||  !CheckCollisionRight() && _direction.x > 0
                    || !CheckCollisionLeft() && _direction.x < 0)
        {
            _direction *= Vector2.zero; 
        }

        DrawGizmoCollision();
        Move(_direction.y);
        BodyRotate(_direction.x);

    }

    private void Move(float direction)
    {
        float currentSpeed = _moveSpeed * direction;
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    private void BodyRotate(float direction)
    {
        float currentRotateSpeed = _moveSpeed * direction * 10;
        transform.Rotate(new Vector3(0, currentRotateSpeed * Time.deltaTime, 0));
    }

    private void DrawGizmoCollision()
    {
        Vector3 start = transform.position + transform.forward * collisionLength / 2 - transform.right * collisionWidth / 2;
        Vector3 end = transform.position - transform.forward * collisionLength / 2 - transform.right * collisionWidth / 2;
        Vector3 right = transform.position - transform.forward * collisionLength / 2 + transform.right * collisionWidth / 2;
        Vector3 left = transform.position - transform.forward * collisionLength / 2 - transform.right * collisionWidth / 2;
        
        Debug.DrawRay(start, transform.right * collisionWidth, Color.yellow);
        Debug.DrawRay(end, transform.right * collisionWidth, Color.yellow);
        Debug.DrawRay(right, transform.forward * collisionLength, Color.yellow);
        Debug.DrawRay(left, transform.forward * collisionLength, Color.yellow);
    }

    private bool CheckCollisionFront()
    {
        Vector3 start = transform.position + transform.forward * collisionLength / 2;
        
        if (Physics.Raycast(start, transform.right, collisionWidth / 2) 
            || Physics.Raycast(start, -transform.right, collisionWidth / 2) 
                || Physics.Raycast(transform.position, transform.forward, collisionLength / 2))
        {
            return false;
        }
        return true;
    }

    private bool CheckCollisionBack()
    {
        Vector3 start = transform.position - transform.forward * collisionLength / 2;
        
        if (Physics.Raycast(start, transform.right, collisionWidth / 2) 
            || Physics.Raycast(start, -transform.right, collisionWidth / 2) 
                || Physics.Raycast(transform.position, -transform.forward, collisionLength / 2))
        {
            return false;
        }
        return true;
    }

    private bool CheckCollisionRight()
    {
        Vector3 startLeft = transform.position - transform.right * collisionWidth / 2;
        Vector3 startRight = transform.position + transform.right * collisionWidth / 2;
        
        if (Physics.Raycast(startLeft, -transform.forward, collisionLength / 2) 
            || Physics.Raycast(startRight, transform.forward, collisionLength / 2))
        {
            return false;
        }
        return true;
    }

    private bool CheckCollisionLeft()
    {
        Vector3 startLeft = transform.position - transform.right * collisionWidth / 2;
        Vector3 startRight = transform.position + transform.right * collisionWidth / 2;
        
        if (Physics.Raycast(startLeft, transform.forward, collisionLength / 2) 
            || Physics.Raycast(startRight, -transform.forward, collisionLength / 2))
        {
            return false;
        }
        return true;
    }
}
