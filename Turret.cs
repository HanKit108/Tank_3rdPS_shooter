using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform barel;
    [SerializeField] private GameObject projectile;
    [SerializeField] private TankStats tankStats;

    private float _cooldown = 1;
    private float _moveSpeed;

    private AITurret _aiScript;
    private PlayerShoot _playerScript;

    public bool IsShoot = false;

    private bool _isPlayer = false;
    private Quaternion _target;


    private void Awake()
    {
        _aiScript = GetComponentInParent<AITurret>();
        _playerScript = GetComponentInParent<PlayerShoot>();

        if(_aiScript != null)
        {
            _isPlayer = false;
        }
        else
        {
            _isPlayer = true;
        }

        _moveSpeed = tankStats.MoveSpeed;
        _cooldown = tankStats.Cooldown;
    }

    void Shoot()
    {
        if(!IsShoot)
        {
            IsShoot = true;
            Instantiate(projectile, attackPoint.position, attackPoint.rotation);
            StartCoroutine(Cooldown(_cooldown));
        }
    }

    IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        IsShoot = false;
    }

    void Update()
    {
        if(_isPlayer)
        {
            _target = camera.rotation;
            if(_playerScript.IsShot)
            {
                Shoot();
            }
        }
        else
        {
            _target = _aiScript.Rotate();
            if(_aiScript.CanAttack())
            {
                Shoot();
            }
        }

        Rotate(_target);
    }


    private void Rotate(Quaternion target)
    {
        var step = _moveSpeed  * Time.deltaTime * 10;

        Quaternion barelIncline = target;

        target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

        turret.rotation = Quaternion.RotateTowards(turret.rotation, target, step);

        if(barelIncline.eulerAngles.x < 30)
        {
            barelIncline.eulerAngles = new Vector3(barelIncline.eulerAngles.x, barel.eulerAngles.y, 0);
            barel.rotation = Quaternion.RotateTowards(barel.rotation, barelIncline, step);
        }
    }
}
