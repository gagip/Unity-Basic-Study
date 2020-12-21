using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;
    public float speed = 1f;
    public int nextMove;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 시작하자마자 경로 찾기
        FindPath();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Die();
    }

    /// <summary>
    /// 자동으로 순찰
    /// </summary>
    void Move()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 내 앞에 블록이 있나?
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * speed, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // rayHit은 frontVec 쪽에 Ground 레이어마스크인 객체만 반환
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        // 내 앞에 블록이 없다면 되돌아가기
        if (rayHit.collider == null)
            Turn();
    }

    /// <summary>
    /// 랜덤으로 경로 찾기
    /// </summary>
    void FindPath()
    {
        Debug.Log("FindPath");
        /*
         * nextMove == -1: 왼쪽이동
         * nextMove == 0: 제자리이동
         * nextMove == 1: 오른쪽이동
         */
        nextMove = Random.Range(-1,2); // [-1, 2)

        float nextIdleTime = Random.Range(1f, 3f);
        Invoke("FindPath", nextIdleTime);

        // 좌우반전
        if (nextMove != 0)
            spriteRenderer.flipX = (nextMove == 1);
    }


    /// <summary>
    /// 뒤돌기
    /// </summary>
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = (nextMove == 1);
        CancelInvoke();
        Invoke("FindPath", 2f);
    }



    /// <summary>
    /// 체력이 0 이하가 되면 제거
    /// </summary>
    void Die()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
