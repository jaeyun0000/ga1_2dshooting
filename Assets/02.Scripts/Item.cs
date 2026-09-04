using UnityEngine;

enum ItemType
{
    PlayerAttackSpeed,
    PlayerHealth,
    PlayerMoveSpeed
}

public class Item : MonoBehaviour
{
    private Player _player;
    private PlayerMove _playerMove;
    private PlayerFire _playerFire;
    private float _moveSpeed = 4f;
    [SerializeField] private ItemType _itemType;
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
                    _playerFire.AddAttackSpeed(0.1f);
                    break;
                case ItemType.PlayerHealth:
                    _player.AddHealth(10);
                    break;
                case ItemType.PlayerMoveSpeed:
                    _playerMove.AddMoveSpeed(1f);
                    break;
            }

            Destroy(gameObject);
        }
    }
}