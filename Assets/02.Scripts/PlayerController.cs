using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;                // 이동속도

    [Header("점프")]
    public float jumpPower = 3f;            // 점프력
    public int maxJumpCount = 2;            // 최대 점프 횟수
    int curJumpCount;                       // 현재 점프 횟수
    public float jumpVelocity = 0.8f;       // 점프 속도

    
    float jumpTime;                         // 점프 시간
    float jumpCoolTime = 1f;                // 점프 쿨타임
    float h;                                // X축 이동값 (좌, 우)
    float v;                                // Y축 이동값 (상, 하)
    
    Rigidbody2D rigid;                      // 리지드바디 2D
    SpriteRenderer spriteRender;            // 이미지

    // Start is called before the first frame update
    void Start()
    {
        curJumpCount = maxJumpCount;

        // 타 컴포넌트(Rigidbody2D 컴포넌트)를 가지고 온다
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    /// <summary>
    /// 플레이어 이동
    /// </summary>
    private void Move()
    {
        // 방향키 -> 이동값
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // 방향벡터
        Vector2 dir = new Vector2(h, v);

        // 방향에 따라 스프라이트 방향도 바꾸기
        if (Input.GetButton("Horizontal"))
            spriteRender.flipX = (Input.GetAxisRaw("Horizontal") == -1);

        // 오브젝트 이동
        transform.Translate(dir * speed * Time.deltaTime);
    }

    /// <summary>
    /// 플레이어 점프
    /// </summary>
    private void Jump()
    {
        // if (점프횟수가 존재 && 점프키를 눌르면)       =>  점프
        if (curJumpCount > 0 && Input.GetButtonDown("Jump"))
        {
            curJumpCount -= 1;              // 점프 횟수 -1
            jumpTime = jumpCoolTime;        // 점프시간 초기화

            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);            // 오브젝트를 위로 힘을 가한다
        }

        // 점프 강약 조절
        if (Input.GetButton("Jump"))
        {
            jumpTime -= Time.deltaTime;                             // 누른 시간만큼 점프시간 감소
            if (jumpTime > 0)
                rigid.velocity += new Vector2(0, jumpVelocity);     // 체공시간만큼 위로 힘을 가한다
        }
    }

    /// <summary>
    /// 충돌하기 직전 이벤트 함수
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅과 충돌할시
        if (collision.gameObject.tag == "Ground")
        {
            curJumpCount = maxJumpCount;
        }
    }
}
