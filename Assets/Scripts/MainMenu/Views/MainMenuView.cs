using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuWindow;
        [SerializeField] private GameObject logoWindow;
        [SerializeField] private Button playButton;
        [SerializeField] private Button logoButton;
        [SerializeField] private Button backToMenuButton;

        private void Awake()
        {
            mainMenuWindow.SetActive(true);
            logoWindow.SetActive(false);

            playButton.onClick.AddListener(PlayButton_Clicked);
            logoButton.onClick.AddListener(LogoButton_Clicked);
            backToMenuButton.onClick.AddListener(BackToMenuButton_Clicked);
        }

        private void PlayButton_Clicked()
        {
            SceneManager.LoadScene("Game");
        }

        private void LogoButton_Clicked()
        {
            mainMenuWindow.SetActive(false);
            logoWindow.SetActive(true);
        }

        private void BackToMenuButton_Clicked()
        {
            mainMenuWindow.SetActive(true);
            logoWindow.SetActive(false);
        }
    }
}