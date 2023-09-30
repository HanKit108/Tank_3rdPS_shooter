using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider HpBar;
    [SerializeField] private TankStats tankStats;

    private Turret _turret;

    private float _maxHP = 500f;
    private float _hp;

    void Start()
    {
        _maxHP = tankStats.Hp;
        _turret = GetComponentInParent<Turret>();
        
        _hp = _maxHP;
        HpBar.maxValue = _maxHP;
    }

    void Update()
    {
        HpBar.value = _hp;
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if(_hp <= 0)
           Death();
        //Debug.Log("получен урон, текущее ХП " + _hp);
    }

    private void Death()
    {
        gameObject.SetActive(false);
        _hp = _maxHP;

        if(_turret != null)
        {
            _turret.IsShoot = false;
        }
    }
}
