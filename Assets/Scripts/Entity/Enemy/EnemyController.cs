using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector2 randomOffset;
    private Vector2 originPos;
    private Vector2 targetPos;

    void Start()
    {
        originPos = transform.position;
        randomOffset = Random.insideUnitCircle * 5f;
    }

    private void Update()
    {
        targetPos = originPos + randomOffset;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * 2f);
    }
}
