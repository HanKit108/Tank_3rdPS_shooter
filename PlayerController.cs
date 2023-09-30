using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float lerp;
    private PlayerControls _controls;

    private Vector2 direction;
 
    public Vector2 Direction
    {
        get {return direction;}
    }
    
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
        //direction = _controls.Player.Move.ReadValue<Vector2>();
        direction = Vector2.Lerp(direction, _controls.Player.Move.ReadValue<Vector2>(), lerp * Time.deltaTime);
    }
}
