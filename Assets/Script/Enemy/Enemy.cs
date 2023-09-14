using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public Transform[] waypoints;
    public Transform targetTrans;
    public GameObject targetBoundary;
    private Rigidbody2D rigid;
    private int turnpoint = 0;
    private bool interTarget;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (interTarget) DetectMove();
        else LoopMove();
    }

    private void LoopMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[turnpoint].position, moveSpeed * Time.deltaTime);
        if (Mathf.Round(transform.position.x) == waypoints[turnpoint].position.x)
            turnpoint = ++turnpoint % 2;
    }

    private void DetectMove()
    {
        if (targetTrans.position.x != Mathf.Round(transform.position.x))
            transform.position = Vector2.MoveTowards(transform.position, targetTrans.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            targetBoundary.SetActive(false);
            interTarget = true;
        }
    }
}
