using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attack = 1;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // 5초 뒤에 총알 삭제
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        

        // 1.임의의 벽을 만들어 충돌할시 삭제
        // 2.이미 만들어진 유니티 API를 이용해 삭제
        // 3.계산해서 삭제
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // 적 체력에 데미지를 준다
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.hp -= attack;

            // 총알 삭제
            Destroy(gameObject);
        }
    }
}
