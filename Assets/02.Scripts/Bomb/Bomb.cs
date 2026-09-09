using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(int.MaxValue);
            }
        }
    }
}
