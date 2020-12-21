using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("캐릭터 속성")]
    public int maxHp = 3;                   // 최대 체력
    public int hp = 3;                      // 현재 체력
    public float speed = 3f;                // 이동속도

    [Header("점프")]
    public float jumpPower = 3f;            // 점프력
    public float jumpVelocity = 8f;         // 점프 속도
    public int maxJumpCount = 2;            // 최대 점프 횟수
    int jumpCount;                          // 현재 점프 횟수
    float jumpTime;                         // 점프 시간
    float jumpCoolTime = 1f;                // 점프 쿨타임

    [Header("총알")]
    public GameObject bullet;               // 총알
    [Tooltip("총알이 발사되는 위치")] public Transform muzzle;                // 총구

    Rigidbody2D rigid;                      // 리지드바디 2D
    SpriteRenderer spriteRender;            // 이미지

    // Start is called before the first frame update
    void Start()
    {
        // 변수 초기화
        hp = maxHp;
        jumpCount = maxJumpCount;
        jumpTime = jumpCoolTime;

        // 타 컴포넌트(Rigidbody2D 컴포넌트)를 가지고 온다
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        Jump();
    }

    /// <summary>
    /// 플레이어 이동
    /// </summary>
    private void Move()
    {
        // 방향키 -> 이동값
        if (Input.GetButton("Horizontal"))
        {
            // x축 이동값
            float h = Input.GetAxis("Horizontal");

            // 캐릭터 좌우 반전
            if (h > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else
                transform.eulerAngles = new Vector3(0, 180, 0);

            // 캐릭터 이동
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 플레이어 점프
    /// </summary>
    private void Jump()
    {
        // if (점프횟수가 존재 && 점프키를 눌르면)       =>  점프
        if (jumpCount > 0 && Input.GetButtonDown("Jump"))
        {
            jumpCount -= 1;                 // 점프 횟수 -1
            jumpTime = jumpCoolTime;        // 점프시간 초기화

            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);            // 오브젝트를 위로 힘을 가한다
        }

        // 점프 강약 조절
        if (Input.GetButton("Jump"))
        {
            
            jumpTime -= Time.deltaTime;                                              // 누른 시간만큼 점프시간 감소
            if (jumpTime > 0)
            {
                rigid.velocity += new Vector2(0, jumpVelocity * Time.deltaTime);     // 체공시간만큼 위로 힘을 가한다
            }
                
        }
    }


    /// <summary>
    /// 총알 발사한다
    /// </summary>
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, muzzle.transform.position, transform.rotation);
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
            jumpCount = maxJumpCount;
        }
    }
}
