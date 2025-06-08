using Assets._Project.Scripts.Gameplay.GameManagment;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.UI
{
    public interface IGameUI //: IService
    {
        UniTask Show();
        UniTask Hide();
    }
    public class GameUIPanel : UIPanel, IGameUI
    {
        [Header("UI References")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _scoreLabel;

        [Header("Animation Settings")]
        [SerializeField] private float _panelFadeDuration = 0.3f;
        [SerializeField] private float _textPunchScaleDuration = 0.1f;
        [SerializeField] private float _textPunchSize = 0.1f;

        private ScoreManager _gameScore;

        private CancellationToken _cancellationToken;

        public void Init()
        {
            _cancellationToken = gameObject.GetCancellationTokenOnDestroy();

            _gameScore = ScoreManager.Instance;
        }

        public override async UniTask Show()
        {
            if (_cancellationToken.IsCancellationRequested)
                return;

            if (gameObject.activeSelf)
                return;

            UpdateScore(_gameScore.Score);

            _gameScore.OnScoreChange += UpdateScore;

            gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;

            await _canvasGroup.DOFade(1f, _panelFadeDuration).SetEase(Ease.OutQuad).WithCancellation(_cancellationToken);
        }

        public override async UniTask Hide()
        {
            if (_cancellationToken.IsCancellationRequested)
                return;

            if (!gameObject.activeSelf)
                return;

            _gameScore.OnScoreChange -= UpdateScore;

            await _canvasGroup.DOFade(0f, _panelFadeDuration).SetEase(Ease.InQuad).WithCancellation(_cancellationToken);

            gameObject.SetActive(false);
        }

        private void UpdateScore(int score)
        {
            _scoreLabel.text = $"Score: {score}";
            AnimatePunch(_scoreLabel.rectTransform);
        }

        private void AnimatePunch(RectTransform target)
        {
            // Kill existing animation if any
            target.DOKill();

            // Reset to normal before animating
            target.localScale = Vector3.one;

            // Apply punch scale
            target.DOPunchScale(Vector3.one * _textPunchSize, _textPunchScaleDuration)
                .SetEase(Ease.OutBack);
        }
    }
}