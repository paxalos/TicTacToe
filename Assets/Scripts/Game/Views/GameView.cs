using Game.Enums;
using Game.Models;
using Game.Presenters;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Game.Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private SelectModeWindowView selectModeWindow;
        [SerializeField] private GameWindowView gameWindow;

        [Inject] private GamePresenter presenter;

        private void Awake()
        {
            mainMenuButton.onClick.AddListener(MainMenuButton_Clicked);

            selectModeWindow.gameObject.SetActive(true);
            gameWindow.gameObject.SetActive(false);

            selectModeWindow.PlayButtonClicked += SelectModeWindow_PlayButtonClicked;
        }

        private void MainMenuButton_Clicked()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void SelectModeWindow_PlayButtonClicked()
        {
            selectModeWindow.gameObject.SetActive(false);
            gameWindow.gameObject.SetActive(true);

            PlayerType[] players = new PlayerType[GameModel.PLAYERS_COUNT];
            switch (selectModeWindow.PlaySymbol)
            {
                case PlaySymbolType.Cross:
                    players[0] = PlayerType.RealPlayer;
                    players[1] = selectModeWindow.Enemy;
                    break;
                case PlaySymbolType.Circle:
                    players[0] = selectModeWindow.Enemy;
                    players[1] = PlayerType.RealPlayer;
                    break;
            }
            presenter.SetPlayers(players);

            gameWindow.StartGame();
        }
    }
}