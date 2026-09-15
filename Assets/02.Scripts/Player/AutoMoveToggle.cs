using UnityEngine;

public class AutoMoveToggle : MonoBehaviour
{
    [SerializeField] private PlayerAutoMove _playerAutoMove;

    public void ToggleAutoMove()
    {
        _playerAutoMove.enabled = !_playerAutoMove.enabled;
    }
}