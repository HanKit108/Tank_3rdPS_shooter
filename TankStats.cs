// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TankStats", menuName = "Tank Stats", order = 51)]
public class TankStats : ScriptableObject
{
    [SerializeField] private float hp;
    public float Hp
    {
        get{return hp;}
    }

    [SerializeField] private float cooldown;
    public float Cooldown
    {
        get{return cooldown;}
    }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed
    {
        get{return moveSpeed;}
    }

    [SerializeField] private float damage;
    public float Damage
    {
        get{return damage;}
    }

    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed
    {
        get{return projectileSpeed;}
    }
}
