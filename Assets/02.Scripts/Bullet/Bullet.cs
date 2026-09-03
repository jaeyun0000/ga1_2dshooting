using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 40f;
    public float moveSpeed = 8f;

    private void Update()
    {
        Vector2 direction = Vector2.up; // new Vector2(1, 0);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }


    // 충돌 관련 이벤트 (Enter -> Stay -> Exit)
    // 충돌이 시작되면 호출되는 이벤트
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 했다");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 나 죽고
            Destroy(this.gameObject);

            // GetComponent<타입> -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.health -= damage;
            if (enemy.health <= 0)
            {
                // 너 죽자
                Destroy(collision.gameObject);
            }
        }
    }
}