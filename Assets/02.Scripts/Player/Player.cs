using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(int heal)
    {
        _health += heal;
        Debug.Log($"현재 체력: {_health}");
    }

    // GetComponent<PlayerMove>().변수 = 수정;
}