using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected int _damage = 10;
    private bool _isDead = false;

    [Header("아이템 확률")]
    [SerializeField] private int _itemDrop = 30;

    [Header("드랍 아이템")]
    [SerializeField] private Item[] _itemPrefabs;

    // - 죽을 때 생성할 이펙트 프리팹
    [SerializeField] private GameObject _deathEffectPrefab;

    private Animator _animator;
    private AudioSource _damagedAudioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damagedAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();
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

        // Todo: Scrptable Object를 사용해서 리팩토링
        // 이유1: 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알 수가 없음
        // 이유2: 각 Enemy 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵고 가독성 저하
        Vector2 dropPosition = transform.position;

        SpawnDeathEffect();

        Destroy(gameObject);

        if (_itemPrefabs.Length > 0 && _itemDrop > Random.Range(0, 100))
        {
            int dropItem = UnityEngine.Random.Range(0, _itemPrefabs.Length);

            Instantiate(_itemPrefabs[dropItem], dropPosition, Quaternion.identity);
            // Quaternion.identity <- 회전 방지
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}