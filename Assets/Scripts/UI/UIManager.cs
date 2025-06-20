namespace UI
{
    using UnityEngine;

    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject defeatPanel;
        [SerializeField] private GameObject pauseMenu;

        public void ShowVictoryScreen()
        {
            victoryPanel.SetActive(true);
        }

        public void ShowGameOverScreen()
        {
            defeatPanel.SetActive(true);
        }

        public void ShowPauseMenu(bool show)
        {
            pauseMenu.SetActive(show);
        }
    }

}