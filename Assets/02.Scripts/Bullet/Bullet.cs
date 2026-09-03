using UnityEngine;

public class Bullet : MonoBehaviour
{
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

        // 나 죽고
        Destroy(this.gameObject);

        // 너 죽자
        Destroy(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("충돌 중이다");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("충돌이 끝났다");
    }
}