using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;

    public void Damage(float damage)
    {
        
        this.health -= damage;
        print("Enemy health: " + health);
    }
}
