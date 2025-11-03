using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSignalHead : MonoBehaviour
{
    private BossController headHit;
    private static readonly string HIT_WEAPON = "Shuriken";

    // Start is called before the first frame update
    void Start()
    {
        headHit = GetComponentInParent<BossController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == HIT_WEAPON)
        {
            headHit.Damaged(100);
        }
    }
}
