using UnityEngine;

public class PlayerController : MonoBehaviour, ITankController
{
    [SerializeField] private float lerp;
    private PlayerControls _controls;

    private Vector2 direction;
    
    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    void Update()
    {
        direction = Vector2.Lerp(direction, _controls.Player.Move.ReadValue<Vector2>(), lerp * Time.deltaTime);
    }

    public Vector2 GetMoveDirection()
    {
        return direction;
    }
}
