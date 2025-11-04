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
        if (collision.gameObject.CompareTag("Player"))
        {
            detect = true;
            target = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            detect = false;
            target = null;
        }
    }
}
