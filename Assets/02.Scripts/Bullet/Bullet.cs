using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 8f;

    private void Update()
    {
        Vector2 direction = Vector2.up; // new Vector2(1, 0);
        transform.Translate(direction * Speed * Time.deltaTime);
    }
}