using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private TankStats tankStats;

    private float _damage = 100f;
    public float Damage 
    {
        get {return _damage;}
    }

    private float _projectileSpeed = 20f;

    void Start()
    {
        _damage = tankStats.Damage;
        _projectileSpeed = tankStats.ProjectileSpeed;
        Destroy(gameObject, 2);
    }

    void Update()
    {
        transform.position += transform.forward * _projectileSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(5 * Time.deltaTime, 0, 0));
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 1))
        {
            if(hit.collider.gameObject.GetComponentInParent<PlayerHealth>() != null)
            {
                hit.collider.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(_damage);
            }
        Destroy(gameObject);
        }
    }
}
