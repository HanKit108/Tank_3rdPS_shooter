using System;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public event EventHandler OnPlayerDied;
    
    [SerializeField] private Slider HpBar;
    [SerializeField] private TankStatsSO tankStats;

    private float _maxHP = 500f;
    private float _hp;
    private bool _isPlayer = false;

    [SerializeField] private GameManager gameManager;

    void Start()
    {
        _maxHP = tankStats.Hp;
        _hp = _maxHP;
        HpBar.maxValue = _maxHP;
        if(TryGetComponent(out PlayerController Ai)) _isPlayer = true;
    }

    void Update()
    {
        HpBar.value = _hp;
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if(_hp <= 0) Death();
    }

    private void Death()
    {
        gameObject.SetActive(false);
        _hp = _maxHP;
        if(OnPlayerDied != null && _isPlayer)
        {
            OnPlayerDied(this, EventArgs.Empty);
        }
    }
}
