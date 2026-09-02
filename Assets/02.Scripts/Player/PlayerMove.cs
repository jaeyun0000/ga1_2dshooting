using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.
    
    // 필요 필드:
    public float Speed = 1f;

    private float minX = -3f;
    private float maxX = 3f;
    private float minY = -4.6f;
    private float maxY = -0.4f;
    
    
    
    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는: 별다른 설정이 없을 경우 가능한 많이
    private void Update()
    {
        // GetAxis / GetAxisRaw 차이는 속도가 점차 증가/감소하냐, 바로 증가/감소 하냐의 차이
        float h = Input.GetAxisRaw("Horizontal");  // 키보드 좌/우 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxisRaw("Vertical");    // 키보드 상/하 입력 상태에 따라 -1f ~ 0 ~ 1f
        
        Debug.Log($"h:{h}, v:{v}");
        
        Vector2 direction = new Vector2(h, v); // 방향

        // 1. 이미지와 같이 빨간색 영역 안에서만 캐릭터가 이동할 수 있게...
        // if (transform.position.x >= 1.86 && direction.x > 0 || transform.position.x <= -1.86 && direction.x < 0)
        // {
        //     direction.x = 0;
        // }
        
        if (transform.position.y >= maxY && direction.y > 0 || transform.position.y <= minY && direction.y < 0)
        {
            direction.y = 0;
        }
        
        
        Vector2 nomalizedSpeed = (direction.normalized * Speed); // 벡터의 길이를 1로 만들어주는 것 (즉, 방향만 유지한다.)
        transform.Translate(nomalizedSpeed * Time.deltaTime);

        
        // 2. 좌우 이동에 있어 한쪽으로 쭈욱 이동하면 반대쪽에서 나오게..
        if (transform.position.x <= minX)
        {
            transform.position = new Vector2 (maxX - 0.1f, transform.position.y);
        }
        if (transform.position.x >= maxX)
        {
            transform.position = new Vector2 (minX + 0.1f, transform.position.y);
        }
        
        
        // 3. 키보드 E키를 누르면 스피드 Up! Q키를 누르면 스피드 Down!
        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed += 0.1f;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed -= 0.1f;
        }
        
        
        // // 1. 키보드 입력을 받는다.
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     Debug.Log("왼쪽 방향키를 누르는 중");
        //     
        //     // 2. 키보드 입력에 따라 방향을 구한다.
        //     // 게임에는 벡터라는 타입이 있다. 벡터는 (크기와 방향을 의미한다)
        //     Vector2 direction = new Vector2(-1, 0); // 왼쪽 방향
        //     // Vector2 direction = Vector2.left;
        //
        //     // 3. 방향과 속력에 따라 이동한다.
        //     // 속도 = 방향 * 속력                         // 매직 넘버란: 보는 사람에 따라 의미가 달라질 수 있는
        //     // 헷갈리는 숫자
        //     transform.Translate(direction * Speed * Time.deltaTime);
        //     // deltaTime: 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 MS로 반환
        // }
        
        // 새로운 위치 = 현재 위치 + (방향 * 속력 * 시간)
        // transform.position = transform.position + (Vector3)direction * Speed * Time.deltaTime;
    }
}
