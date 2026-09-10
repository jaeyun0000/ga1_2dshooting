using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // static (정적)
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    // 관리: 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 로직
    private int _bestScore;
    private int _currentScore;

    // 저장키
    private const string SaveKey = "BestScore";

    // UI 책임 추가 (텍스트메시 프로 참조)
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;


    private void Awake()
    {
        // 늦게 태어난 매니저는 나는 늦었네~ 하면서 자살
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        // 입력: Input
        // 저장/불러오기: PlayerPrefs

        if (PlayerPrefs.HasKey(SaveKey))
        {
            _bestScore = PlayerPrefs.GetInt(SaveKey);
        }

        Refresh();
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;

            // 저장: PlayerPrefs.Set~ 시리즈를 이용해서 int/float/string을 저장 가능하다.
            // 내 컴퓨터 어딘가에 저장이 된다..
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save();
        }

        Refresh();
    }

    private void Refresh()
    {
        _bestScoreText.text = $"BestScore: {_bestScore}";
        _currentScoreText.text = $"Score: {_currentScore}";
    }
}