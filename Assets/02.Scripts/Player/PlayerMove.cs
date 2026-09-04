using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _speed = 1f;

    private float _minX = -3f;
    private float _maxX = 3f;
    private float _minY = -4.6f;
    private float _maxY = -0.4f;


    // 초당 프레임 실행 횟수는: 별다른 설정이 없을 경우 가능한 많이
    private void Update()
    {
        Move();

        SpeedUpDown();

        // 게임에는 벡터라는 타입이 있다. 벡터는 (크기와 방향을 의미한다)
        // // 속도 = 방향 * 속력      // 매직 넘버란: 보는 사람에 따라 의미가 달라질 수 있는
        // // deltaTime: 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 MS로 반환

        // 새로운 위치 = 현재 위치 + (방향 * 속력 * 시간)
        // transform.position = transform.position + (Vector3)direction * Speed * Time.deltaTime;
    }

    private void Move()
    {
        // GetAxis / GetAxisRaw 차이는 속도가 점차 증가/감소하냐, 바로 증가/감소 하냐의 차이
        float h = Input.GetAxisRaw("Horizontal"); // 키보드 좌/우 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxisRaw("Vertical"); // 키보드 상/하 입력 상태에 따라 -1f ~ 0 ~ 1f

        Vector2 direction = new Vector2(h, v);


        if (transform.position.y >= _maxY && direction.y > 0 || transform.position.y <= _minY && direction.y < 0)
        {
            direction.y = 0;
        }


        Vector2 nomalizedSpeed = (direction.normalized * _speed); // 벡터의 길이를 1로 만들어주는 것 (즉, 방향만 유지한다.)
        transform.Translate(nomalizedSpeed * Time.deltaTime);


        if (transform.position.x <= _minX)
        {
            transform.position = new Vector2(_maxX - 0.1f, transform.position.y);
        }

        if (transform.position.x >= _maxX)
        {
            transform.position = new Vector2(_minX + 0.1f, transform.position.y);
        }
    }

    private void SpeedUpDown()
    {
        // 3. 키보드 E키를 누르면 스피드 Up! Q키를 누르면 스피드 Down!
        if (Input.GetKeyDown(KeyCode.E))
        {
            _speed += 1f;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _speed -= 1f;
        }
    }

    public void AddMoveSpeed(float speed)
    {
        _speed += speed;
    }
}