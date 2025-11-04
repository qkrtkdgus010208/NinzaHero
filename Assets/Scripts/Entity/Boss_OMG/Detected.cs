using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour
{
    BossController area;
    private bool detect = false;
    [SerializeField] private LayerMask PlayerCollisionLayer;
    Transform target;

    private void Start()
    {
        area = FindAnyObjectByType<BossController>();
    }

    private void Update()
    {
        area.Detected(detect, target);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"¢º Trigger Enter: {collision.name}, Layer: {collision.gameObject.layer}, Tag: {collision.tag}");
        if ((PlayerCollisionLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Debug.Log($"¢º Trigger Enter: {collision.name}, Layer: {collision.gameObject.layer}, Tag: {collision.tag}");
            Debug.Log("Å½ÁöµÇ¾ú´Ù");
            detect = true;
            target = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("¹þ¾î³²");

        if (PlayerCollisionLayer.value == (PlayerCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            Debug.Log("¹þ¾î³²");
            detect = false;
            target = null;
        }
    }
}
