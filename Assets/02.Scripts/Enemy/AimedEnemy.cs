using UnityEngine;

public class AimedEnemy : Enemy
{
    private GameObject _player;
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.Log("No player found");
            return;
        }

        _direction = _player.transform.position - transform.position;

        float dx = _direction.x; // 플레이어와 에너미 사이의 밑변 길이
        float dy = _direction.y; // 플레이어와 에너미 사이의 높이 길이
        // tan0세타 = dy / dx
        // ran^ * tan0세타 = tan^ dy / dx
        // 0세타 = tant^ * dy / dx
        // 각도 = 세타 * Rad2Deg
        float radian = Mathf.Atan2(dy, dx);
        float angle = radian * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        _direction.Normalize();
    }

    protected override void Move()
    {
        if (_player == null) return;

        // transform.Translate(_direction * _moveSpeed * Time.deltaTime);
        transform.position += (Vector3)(_direction * _moveSpeed) * Time.deltaTime;
    }
}