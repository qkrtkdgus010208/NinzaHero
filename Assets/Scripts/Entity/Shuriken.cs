using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public Vector3 targetPos;

    private float offset = 0.5f;
    float rotateSpeed = 2f;
    float speed = 10f;
    Quaternion targetRot;
    Rigidbody2D rigid;
    BoxCollider2D area;
    Vector3 direction;
    bool isThrow = false;

    private void Start()
    {
        targetRot = Quaternion.Euler(0, 0, 90);
        rigid = GetComponent<Rigidbody2D>();
        area = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        Rotation();
        Hanging();
    }
    private void Hanging()
    {
        if (targetPos != Vector3.zero)
        {
            direction = (targetPos - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void Rotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            targetPos = collision.gameObject.transform.position;
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isThrow = false;
    }
}
