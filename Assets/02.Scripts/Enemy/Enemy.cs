using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] private int _damage = 20;

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
        if (other.gameObject.CompareTag("Player"))
        {
            // 나 죽고
            Destroy(gameObject);

            // GetComponent<타입> -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            PlayerHp playerHp = other.gameObject.GetComponent<PlayerHp>();

            // 응집도는 높히고, 결합도는 낮춰라
            // 결합도란 묻는 거.. 매번 묻는 거..
            // 무적모드 검사하고
            // 방어력 검사..
            playerHp.TakeDamage(_damage);
        }
    }
}