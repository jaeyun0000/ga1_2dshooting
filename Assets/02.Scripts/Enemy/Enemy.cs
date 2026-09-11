using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    private int _maxHP;
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected int _damage = 10;
    private bool _isDead = false;

    [Header("Enemy Type")]
    [SerializeField] private EnemyType _enemyType;
    public EnemyType EnemyType => _enemyType;

    [Header("아이템 드랍 확률")]
    [SerializeField] private int _itemDrop = 30;

    [Header("드랍 아이템")]
    [SerializeField] private ItemSpawnDataTableSO _itemDataTable;

    // - 죽을 때 생성할 이펙트 프리팹
    [SerializeField] private GameObject _deathEffectPrefab;

    private Animator _animator;
    private AudioSource _damagedAudioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damagedAudioSource = GetComponent<AudioSource>();
        _maxHP = _health;
    }

    private void Update()
    {
        Move();
    }

    protected virtual void OnEnable()
    {
        _health = _maxHP;

        _isDead = false;
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        if (_animator != null)
        {
            _animator.SetTrigger("Hit");
        }

        _damagedAudioSource.Play();

        _health -= damage;
        if (_health <= 0)
        {
            // 싱글톤 패턴
            // 1. 전역적으로 접근 가능하다.
            // 2. 인스턴스(생성된 객체)가 하나임을 보장한다.

            ScoreManager.Instance.AddScore(100);

            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Die();

            // GetComponent<타입> -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogWarning("Player null");
                return;
            }

            player.TakeDamage(_damage);
        }
    }

    private void Die()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;

        Vector2 dropPosition = transform.position;

        SpawnDeathEffect();

        gameObject.SetActive(false);
        // Destroy(gameObject);

        if (_itemDataTable != null && _itemDrop > Random.Range(0, 100))
        {
            int totalWeight = 0;
            foreach (ItemSpawnData data in _itemDataTable.Datas)
            {
                totalWeight += data.Weight;
            }

            int randomWeight = Random.Range(0, totalWeight);

            int cumulativeWeight = 0;
            foreach (ItemSpawnData data in _itemDataTable.Datas)
            {
                cumulativeWeight += data.Weight; // 누적
                if (randomWeight < cumulativeWeight) // 구간
                {
                    Item item = ItemPool.Instance.GetItem(data.ItemType);
                    item.transform.position = dropPosition;
                    break;
                }
            }
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}