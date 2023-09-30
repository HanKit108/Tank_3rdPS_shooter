using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerControls _controls;

    private bool isShot = false;
 
    public bool IsShot
    {
        get {return isShot;}
    }


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

}
