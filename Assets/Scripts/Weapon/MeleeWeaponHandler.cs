using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    public override void Attack()
    {
        base.Attack();
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, AttackRange, Vector2.zero, 0, target);

        if (hit.collider == null)
        {
            return;
        }

        ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
        if (resourceController != null)
        {
            resourceController.ChangeHealth(-Power);
            if (IsOnKnockback)
            {
                BaseController controller = hit.collider.GetComponent<BaseController>();
                if (controller != null)
                {
                    controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                }
            }
        }
    }
}
