using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private int totalDamage = 0;

    public void Damage(int damage)
    {
        totalDamage += damage;
        Debug.Log("合計 : " + totalDamage);
    }
}
