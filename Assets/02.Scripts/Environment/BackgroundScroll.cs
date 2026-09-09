using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 0.1f;

    private Material _material;
    private float _offsetY = 0f;
    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _offsetY += _scrollSpeed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(0, _offsetY);
    }
}
