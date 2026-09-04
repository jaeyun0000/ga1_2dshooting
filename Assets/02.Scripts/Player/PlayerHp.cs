using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int _hp = 100;

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}