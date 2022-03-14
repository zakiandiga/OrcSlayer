using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    //private HealthPoint healthPoint;

    [SerializeField] private Slider healthBar;


    private Player bindedPlayer;

    #region Calculation Variables
    private int _playerMaxHP;
    private int _playerCurrentHP;


    #endregion

    public static event Action<PlayerHUD> OnPlayerUIActive;

    private void Start()
    {
        OnPlayerUIActive?.Invoke(this);
    }

    private void OnEnable()
    {        
        Player.OnInitializePlayerUI += AssignPlayer;
        
    }

    private void OnDisable()
    {
        Player.OnInitializePlayerUI -= AssignPlayer;
        bindedPlayer.OnTakeDamage -= ReduceHealth;
    }

    private void AssignPlayer(Player player)
    {
        bindedPlayer = player;
        bindedPlayer.OnTakeDamage += ReduceHealth; //Subs to player take damage
        InitializeHealth(bindedPlayer.PlayerMaxHP, bindedPlayer.PlayerCurrentHP);
    }

    private void InitializeHealth(int maxHealth, int currentHealth)
    {
        _playerMaxHP = maxHealth;
        _playerCurrentHP = currentHealth;
        healthBar.value = (float) _playerCurrentHP / _playerMaxHP;
    }

    public void ReduceHealth(int damage, Vector3 contactPoint, WeaponType weaponType)
    {

        _playerCurrentHP -= damage;
        healthBar.value = (float) _playerCurrentHP / _playerMaxHP;
    }
}
