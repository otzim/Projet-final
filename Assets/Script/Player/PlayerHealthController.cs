using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int healthInitial = 3;

    public Animator animations;

    private int healthCurrent;

    public HealthBar healthBar;

    private void Start()
    {
        ResetHealth();
        healthBar.SetMaxHealth(healthInitial);
        animations = gameObject.GetComponentInChildren<Animator>();
    }

    private void ResetHealth()
    {
        healthCurrent = healthInitial;
    }

    public void TakeDamage(int damageAmout)
    {
        healthCurrent -= damageAmout;

        if (healthCurrent <= 0)
        {
            animations.SetTrigger("Dead");
            Destroy(gameObject, 3);
            Debug.Log("Dead");
        }

        healthBar.SetHealth(healthCurrent);
    }

    //public void Heal(int healAmout)
    //{
    //    healthCurrent += healAmout;

    //    if (healthCurrent > healthInitial)
    //    {
    //        ResetHealth();
    //    }
    //}


}
