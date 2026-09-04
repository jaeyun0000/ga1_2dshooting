using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected int _damage = 10;
    [Header("아이템 확률")]
    [SerializeField] private int _itemDrop = 30;

    [Header("드랍 아이템")]
    [SerializeField] private Item[] _itemPrefabs;

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
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
        int dropItem = UnityEngine.Random.Range(0, _itemPrefabs.Length);
        Vector2 dropPosition = transform.position;

        Destroy(gameObject);

        if (_itemPrefabs.Length > 0 && _itemDrop > Random.Range(0, 100))
        {
            Instantiate(_itemPrefabs[dropItem], dropPosition, Quaternion.identity);
            // Quaternion.identity <- 회전 방지
        }
    }
}