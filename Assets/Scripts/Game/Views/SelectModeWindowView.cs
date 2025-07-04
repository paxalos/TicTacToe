using System;
using Game.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class SelectModeWindowView : MonoBehaviour
    {
        public event Action PlayButtonClicked;

        [SerializeField] private TMP_Dropdown enemyDropdown;
        [SerializeField] private TMP_Text selectPlaySymbolLabel;
        [SerializeField] private TMP_Dropdown playSymbolDropdown;
        [SerializeField] private Button playButton;


        public PlayerType Enemy { get; private set; } = PlayerType.EasyBot;
        public PlaySymbolType PlaySymbol { get; private set; } = PlaySymbolType.Cross;

        private void Awake()
        {
            enemyDropdown.onValueChanged.AddListener(EnemyDropdown_ValueChanged);
            playSymbolDropdown.onValueChanged.AddListener(PlaySymbolDropdown_ValueChanged);
            playButton.onClick.AddListener(PlayButton_Clicked);
        }

        private void EnemyDropdown_ValueChanged(int index)
        {
            switch (index)
            {
                case 0:
                    Enemy = PlayerType.EasyBot;
                    break;
                case 1:
                    Enemy = PlayerType.NormalBot;
                    break;
                case 2:
                    Enemy = PlayerType.HardBot;
                    break;
                case 3:
                    Enemy = PlayerType.RealPlayer;
                    break;
            }
            switch (Enemy)
            {
                case PlayerType.RealPlayer:
                    selectPlaySymbolLabel.gameObject.SetActive(false);
                    playSymbolDropdown.gameObject.SetActive(false);
                    break;
                default:
                    selectPlaySymbolLabel.gameObject.SetActive(true);
                    playSymbolDropdown.gameObject.SetActive(true);
                    break;
            }
        }

        private void PlaySymbolDropdown_ValueChanged(int index)
        {
            switch (index)
            {
                case 0:
                    PlaySymbol = PlaySymbolType.Cross;
                    break;
                case 1:
                    PlaySymbol = PlaySymbolType.Circle;
                    break;
            }
        }

        private void PlayButton_Clicked()
        {
            var handler = PlayButtonClicked;
            handler?.Invoke();
        }
    }
}