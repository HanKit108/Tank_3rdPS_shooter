using UnityEngine;

public class PlayerShoot : MonoBehaviour, ITurretController
{
    [SerializeField] private Transform camera;
    
    private PlayerControls _controls;

    private bool isShot = false;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Player.Shoot.performed += context => Fire();
        _controls.Player.Shoot.canceled += context => Cool();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Fire()
    {
        isShot = true;
    }

    private void Cool()
    {
        isShot = false;
    }

    public Quaternion GetTurretRotation()
    {
        return camera.rotation;
    }

    public bool IsShoot()
    {
        return isShot;
    }
}
