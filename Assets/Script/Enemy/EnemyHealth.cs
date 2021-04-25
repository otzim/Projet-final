using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
  
    void Start()
    {


  
    }
 
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }
    private void DestroyEnemy()
    {
       
        Destroy(gameObject);
    }
}
