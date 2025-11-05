using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceArea : MonoBehaviour
{
    EnergyBallController ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌!");

        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("충돌!");
            ball = collision.gameObject.GetComponent<EnergyBallController>();

            ball.Bounce();
        }
    }
}
