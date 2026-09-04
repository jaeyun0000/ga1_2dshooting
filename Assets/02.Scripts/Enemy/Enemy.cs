using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected int _damage = 10;

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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 나 죽고
            Destroy(gameObject);

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
}