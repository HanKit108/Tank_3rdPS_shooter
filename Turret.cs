using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform barel;
    [SerializeField] private GameObject projectile;
    [SerializeField] private TankStats tankStats;

    private float _cooldown = 1;
    private float _moveSpeed;
    private ITurretController _turretController;

    private bool _isShoot = false;

    private Quaternion _target;


    private void Awake()
    {
        _turretController = GetComponentInParent<ITurretController>();

        _moveSpeed = tankStats.MoveSpeed;
        _cooldown = tankStats.Cooldown;
    }

    private void OnEnable()
    {
        _isShoot = false;
    }

    void Shoot()
    {
        if(!_isShoot)
        {
            _isShoot = true;
            Instantiate(projectile, attackPoint.position, attackPoint.rotation);
            StartCoroutine(Cooldown(_cooldown));
        }
    }

    IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        _isShoot = false;
    }

    void Update()
    {
        _target = _turretController.GetTurretRotation();
        if(_turretController.IsShoot())
        {
            Shoot();
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
