using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 관리: 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 로직
    private int _bestScore;
    private int _currentScore;

    // UI 책임 추가 (텍스트메시 프로 참조)
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;


    private void Update()
    {
        _bestScoreText.text = $"BestScore: {_bestScore}";
        _currentScoreText.text = $"Score: {_currentScore}";
    }
}