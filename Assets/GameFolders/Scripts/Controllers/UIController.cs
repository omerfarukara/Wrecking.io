using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameFolders.Scripts.Controllers
{
    public class UIController : MonoSingleton<UIController>
    {
        private EventData _eventData;

        [SerializeField] private Joystick joystick;

        [Header("Panels")]
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject chooseGameType;

        [Header("Buttons")]
        [SerializeField] Button playerAiLevel;
        [SerializeField] Button holeLevel;

        [Header("Win Count Texts")]
        [SerializeField] TextMeshProUGUI playerWinText;
        [SerializeField] TextMeshProUGUI aiWinText;

        private void Awake()
        {
            Singleton();
            _eventData = Resources.Load("EventData") as EventData;

            playerAiLevel.onClick.AddListener(delegate { LoadScene(1); });
            holeLevel.onClick.AddListener(delegate { LoadScene(2); });
        }

        private void OnEnable()
        {
            _eventData.OnFinish += Finish;
        }

        private void Start()
        {
            if (playerWinText == null && aiWinText == null) return;
            SetScores(playerWinText, GameManager.Instance.PlayerWinCount);
            SetScores(aiWinText, GameManager.Instance.AiWinCount);
        }

        private void OnDisable()
        {
            _eventData.OnFinish -= Finish;
        }

        private void LoadScene(int levelCount)
        {
            GameManager.Instance.GameState = GameState.Play;
            SceneManager.LoadScene(levelCount);
        }

        private void Finish(bool statu)
        {
            if (statu)
            {
                Win();
            }
            else
            {
                Lose();
            }
            chooseGameType.SetActive(true);
        }

        private void Win()
        {
            victoryPanel.SetActive(true);
            SetScores(playerWinText, GameManager.Instance.PlayerWinCount);
        }

        private void Lose()
        {
            losePanel.SetActive(true);
            SetScores(aiWinText, GameManager.Instance.AiWinCount);
        }

        private void SetScores(TextMeshProUGUI setText, int value)
        {
            setText.text = value.ToString();
        }

        public float GetHorizontal()
        {
            return joystick.Horizontal;
        }

        public float GetVertical()
        {
            return joystick.Vertical;
        }
    }
}
