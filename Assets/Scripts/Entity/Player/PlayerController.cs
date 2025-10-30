using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Action()
    {
        float horizon = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizon, vertical);

        if(moveDirection.magnitude != 0f)
        {
            moveDirection = moveDirection.normalized;
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        lookDirection = moveDirection;
    }
}
