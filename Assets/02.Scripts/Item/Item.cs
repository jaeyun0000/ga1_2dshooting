using UnityEngine;

public class Item : MonoBehaviour
{
    private Player _player;
    private PlayerMove _playerMove;
    private PlayerFire _playerFire;
    private float _moveSpeed = 4f;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private float _itemValue = 1;
    [SerializeField] private float _timer = 2f;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            _player = playerObject.GetComponent<Player>();
            _playerMove = playerObject.GetComponent<PlayerMove>();
            _playerFire = playerObject.GetComponent<PlayerFire>();
        }
    }

    private void Update()
    {
        if (_player == null) return;

        if (_timer >= 0)
        {
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0)
        {
            Vector2 direction = _player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * _moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player == null)
                return;

            switch (_itemType)
            {
                case ItemType.PlayerAttackSpeed:
                    _playerFire.AddAttackSpeed(_itemValue);
                    Debug.Log($"플레이어 공격속도: {_playerFire.AttackCooldown}");
                    break;
                case ItemType.PlayerHealth:
                    Debug.Log($"플레이어 체력: {player.Health}");
                    _player.AddHealth((int)_itemValue);
                    break;
                case ItemType.PlayerMoveSpeed:
                    // 캡슐화 :
                    // + 데이터 은닉 (Speed 속성 private 처리)
                    // + 행위를 통한 상태 변경 (SpeedUp 호출)
                    _playerMove.AddMoveSpeed(_itemValue);
                    Debug.Log($"플레이어 이동속도: {_playerMove.Speed}");
                    break;
            }

            Destroy(gameObject);
        }
    }
}