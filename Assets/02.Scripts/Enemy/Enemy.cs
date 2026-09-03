using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 2f;


    private void Update()
    {
        Vector2 direction = Vector2.down;

        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}