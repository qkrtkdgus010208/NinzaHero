using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    TailAttack hit;

    private void Awake()
    {
        hit = GetComponentInParent<TailAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit.SetDamage(collision);
    }
}
