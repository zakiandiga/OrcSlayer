using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    //private HealthPoint healthPoint;

    [SerializeField] private Image healthBar;

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
        Player.OnPlayerTakesDamage += ReduceHealth;
    }

    private void OnDisable()
    {
        Player.OnInitializePlayerUI -= AssignPlayer;
        Player.OnPlayerTakesDamage -= ReduceHealth;
    }

    private void AssignPlayer(Player player)
    {
        bindedPlayer = player;
        InitializeHealth(bindedPlayer.PlayerMaxHP, bindedPlayer.PlayerCurrentHP);
    }

    private void InitializeHealth(int maxHealth, int currentHealth)
    {
        _playerMaxHP = maxHealth;
        _playerCurrentHP = currentHealth;
        Debug.Log(_playerCurrentHP + "/" + _playerMaxHP);
        healthBar.fillAmount = (float) _playerCurrentHP / _playerMaxHP;
    }

    private void ReduceHealth(int damage)
    {

        _playerCurrentHP -= damage;
        healthBar.fillAmount = (float) _playerCurrentHP / _playerMaxHP;
    }
}

//Currently unused
public class HealthPoint
{
    public const int MaxHealthDisplay = 100;

    private float changeAmount;
    private float currentHealth;
    private float regenAmount;

    public HealthPoint()
    {
        currentHealth = 0;
    }

    public void InitializeHealth(int startingHealth)
    {
        currentHealth = startingHealth;
    }

    public void RecoverHealth (int healthChange)
    {
        if(currentHealth <= 0)
        {
            currentHealth += healthChange;
        }
    }

    public void ReduceHealth (int damage)
    {
        if (currentHealth >= 0)
            currentHealth -= damage;
    }

    public float NormalizedHealthValue()
    {
        return currentHealth / MaxHealthDisplay;
    }
}
