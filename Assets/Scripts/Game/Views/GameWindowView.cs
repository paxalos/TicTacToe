using Cysharp.Threading.Tasks;
using Game.Enums;
using Game.Presenters;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace Game.Views
{
    public class GameWindowView : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageLabel;
        [SerializeField] private TMP_Text drawCountLabel;
        [SerializeField] private TMP_Text[] playersWinCountLabel;
        [SerializeField] private FieldView fieldView;
        [SerializeField] private Button[] cells;
        [SerializeField] private ElementView[] elementsPrefabs;
        [SerializeField] private Button replayButton;

        [Inject] private GamePresenter presenter;

        public void ActivateCells(int[] cellIndexes)
        {
            for (int i = 0; i < cellIndexes.Length; i++)
            {
                int cellIndex = cellIndexes[i];
                cells[cellIndex].interactable = true;
            }
        }

        public UniTask GetCellClickWait(int[] cellIndexes)
        {
            UniTask[] cellsWaiting = new UniTask[cellIndexes.Length];
            for (int i = 0; i < cellIndexes.Length; i++)
            {
                int cellIndex = cellIndexes[i];
                var cell = cells[cellIndex];
                cellsWaiting[i] = cell.OnClickAsync();
            }
            return UniTask.WhenAny(cellsWaiting);
        }

        public void SelectCell(int cellIndex)
        {
            DeactivateAllCells();
            presenter.SelectCell(cellIndex);
        }

        public void SetElementInCell(int cellIndex, PlaySymbolType elementType)
        {
            var elementPrefab = elementsPrefabs.First(elementPrefab => elementPrefab.PlaySymbol == elementType);
            var element = Instantiate(elementPrefab, cells[cellIndex].transform);
            element.StartDrawAnimation();
        }

        public void SetGameResult(GameResultType gameResult, int winningLineIndex)
        {
            switch (gameResult)
            {
                case GameResultType.CrossWin:
                    messageLabel.text = "First player win!";
                    fieldView.DrawWinLine(winningLineIndex);
                    break;
                case GameResultType.CircleWin:
                    messageLabel.text = "Second player win!";
                    fieldView.DrawWinLine(winningLineIndex);
                    break;
                case GameResultType.Draw:
                    messageLabel.text = "Draw";
                    break;
            }

            replayButton.gameObject.SetActive(true);
        }

        public void SetPlayerTurnMessage(int playerIndex)
        {
            string message = $"{(playerIndex == 0 ? "First" : "Second")} player turn";
            messageLabel.text = message;
        }

        public void SetDrawCount(int drawCount)
        {
            drawCountLabel.text = drawCount.ToString();
        }

        public void SetPlayerWinCount(int playerIndex, int winCount)
        {
            var playerWinCountLabel = playersWinCountLabel[playerIndex];
            playerWinCountLabel.text = winCount.ToString();
        }
        
        public async void StartGame()
        {
            fieldView.DrawFieldLines();
            await UniTask.WaitForSeconds(fieldView.DrawFieldAnimationDuration);
            presenter.StartGame();
        }


        private void Awake()
        {
            replayButton.onClick.AddListener(ReplayButton_Click);
        }

        private void ReplayButton_Click()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                var cellTransform = cells[i].transform;
                if (cellTransform.childCount != 0)
                {
                    var elementTransform = cellTransform.GetChild(0);
                    Destroy(elementTransform.gameObject);
                }
            }

            replayButton.gameObject.SetActive(false);

            StartGame();
        }

        private void DeactivateAllCells()
        {
            for (int i = 0; i < cells.Length; i++)
                cells[i].interactable = false;
        }
    }
}