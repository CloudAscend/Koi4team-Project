using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpScale;
    public LayerMask layer;
    public GameObject attackBoundary;
    private float moveX;
    private int animState;
    private bool attack;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private BoxCollider2D box;
    private Animator anime;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        anime = GetComponent<Animator>();

        attackBoundary.SetActive(false);
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        if (moveX != 0)
        {
            sprite.flipX = moveX > 0 == true;
        }

        if (Input.GetButtonDown("Jump") && Jumpable())
        {
            rigid.velocity = new Vector2(0, jumpScale);
        }

        if (Input.GetKeyDown(KeyCode.Z) && !attack)
        {
            StartCoroutine(Attack());
        }

        rigid.velocity = new Vector2(moveX * moveSpeed, rigid.velocity.y);
    }

    private void LateUpdate()
    {
        if (moveX == 0)  animState = 0;
        if (moveX != 0)  animState = 1;
        if (!Jumpable()) animState = 2;
        if (attack)      animState = 3;

        anime.SetInteger("state", animState);
    }

    private IEnumerator Attack()
    {
        attack = true;

        int attackDir = 1;
        if (!sprite.flipX) attackDir = -1;
        attackBoundary.transform.position = new Vector2(transform.position.x + attackDir, transform.position.y);
        attackBoundary.SetActive(true);

        yield return new WaitForSeconds(.5f);

        attackBoundary.SetActive(false);
        attack = false;
    }

    private bool Jumpable()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, layer);
    }
}
