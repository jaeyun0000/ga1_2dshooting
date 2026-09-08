using System;
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Animator _animator;

    [SerializeField] private float _speed = 1f;
    public float Speed => _speed;

    [Header("Trail")]
    [SerializeField] private TrailRenderer _playerTrail;
    [SerializeField] private float _trailLength = 1f;
    private Coroutine _trailCoroutine;

    private float _minX = -3f;
    private float _maxX = 3f;
    private float _minY = -4.6f;
    private float _maxY = -0.4f;


    // 객체가 생성될 때 한 번 실행된다.
    private void Awake()
    {
        // 애니메이터 컴포넌트에 대한 참조를 가져와서 할당한다.
        _animator = GetComponent<Animator>();
    }

    // 초당 프레임 실행 횟수는: 별다른 설정이 없을 경우 가능한 많이
    private void Update()
    {
        Move();

        UpdateTrailTime();
        // 게임에는 벡터라는 타입이 있다. 벡터는 (크기와 방향을 의미한다)
        // // 속도 = 방향 * 속력      // 매직 넘버란: 보는 사람에 따라 의미가 달라질 수 있는
        // // deltaTime: 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 MS로 반환

        // 새로운 위치 = 현재 위치 + (방향 * 속력 * 시간)
        // transform.position = transform.position + (Vector3)direction * Speed * Time.deltaTime;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    private void Move()
    {
        // GetAxis / GetAxisRaw 차이는 속도가 점차 증가/감소하냐, 바로 증가/감소 하냐의 차이
        float h = Input.GetAxisRaw("Horizontal"); // 키보드 좌/우 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxisRaw("Vertical"); // 키보드 상/하 입력 상태에 따라 -1f ~ 0 ~ 1f

        Vector2 direction = new Vector2(h, v);

        _animator.SetInteger("x", (int)direction.x);


        if (transform.position.y >= _maxY && direction.y > 0 ||
            transform.position.y <= _minY && direction.y < 0)
        {
            direction.y = 0;
        }


        Vector2 nomalizedSpeed = (direction.normalized * _speed); // 벡터의 길이를 1로 만들어주는 것 (즉, 방향만 유지한다.)
        transform.Translate(nomalizedSpeed * Time.deltaTime);


        if (transform.position.x <= _minX)
        {
            WarpX(_maxX - 0.1f);
        }
        else if (transform.position.x >= _maxX)
        {
            WarpX(_minX + 0.1f);
        }
    }

    private void WarpX(float targetX)
    {
        if (_playerTrail != null)
        {
            // Trail 기록 끄기
            _playerTrail.emitting = false;

            // 현재까지 생성된 Trail 제거
            _playerTrail.Clear();
        }

        Vector2 newPosition = transform.position;
        newPosition.x = targetX;
        transform.position = newPosition;

        if (_playerTrail != null)
        {
            if (_trailCoroutine != null)
            {
                StopCoroutine(_trailCoroutine);
            }

            _trailCoroutine = StartCoroutine(RestartTrail());
        }
    }

    private IEnumerator RestartTrail()
    {
        // 순간이동한 프레임은 Trail을 생성하지 않는다.
        yield return null;  // <- 대표적으로 다음 프레임까지 기다려라

        // 혹시 남아있는 Trail 정보를 한 번 더 제거
        _playerTrail.Clear();

        // 새로운 위치에서 Trail 생성 시작
        _playerTrail.emitting = true;

        _trailCoroutine = null;
    }

    private void UpdateTrailTime()
    {
        if (_playerTrail == null)
        {
            return;
        }

        if (_speed <= 0f)
        {
            return;
        }

        // 거리 = 속도 x 시간
        // 시간 = 원하는 거리 / 속도
        _playerTrail.time = _trailLength / _speed;
    }

    public void AddMoveSpeed(float speed)
    {
        if (_speed < 20f)
        {
            _speed += speed;
            Debug.Log($"현재 이동 속도: {_speed}");
        }
        else
        {
            Debug.Log($"최대 이동 속도: {_speed}");
        }
    }
}