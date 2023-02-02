using UnityEngine;
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
    
        [Header("Buttons")]
        [SerializeField] Button nextLevelButton;
        [SerializeField] Button tryAgainButton;

        private void Awake()
        {
            Singleton();
            _eventData = Resources.Load("EventData") as EventData;
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
