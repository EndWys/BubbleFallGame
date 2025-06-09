using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.UI
{
    public interface IGameOverdUI : IService
    {
        UniTask Show();
        UniTask Hide();

        event Action OnTouch;
    }
    public class GameOverUIPanel : UIPanel, IGameOverdUI
    {
        [Header("References")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _yourScoreLable;
        [SerializeField] private TextMeshProUGUI _scoreNumber;
        [SerializeField] private TextMeshProUGUI _tapLable;
        [SerializeField] private Button _panelButton;

        [Header("Animation Settings")]
        [SerializeField] private float _pannelFadeDuration = 0.5f;
        [SerializeField] private float _delayBetween = 0.1f;
        [SerializeField] private float _punchScale = 1.15f;
        [SerializeField] private float _punchDuration = 0.25f;

        private CancellationTokenSource _tokenSource;

        private ScoreManager _gameScore;

        public event Action OnTouch;

        public void Init()
        {
            _gameScore = ServiceLocator.Local.Get<ScoreManager>();

            _panelButton.onClick.AddListener(OnPanelClick);

            _tokenSource = new CancellationTokenSource();
        }

        public override async UniTask Show()
        {
            if (_tokenSource.IsCancellationRequested)
                return;

            if (gameObject.activeInHierarchy)
                return;

            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;

            _yourScoreLable.rectTransform.localScale = Vector3.zero;
            _scoreNumber.rectTransform.localScale = Vector3.zero;
            _tapLable.rectTransform.localScale = Vector3.zero;

            _scoreNumber.text = _gameScore.Score.ToString();

            // Fade in the whole panel
            await _canvasGroup.DOFade(1, _pannelFadeDuration)
                .SetEase(Ease.OutQuad)
                .WithCancellation(_tokenSource.Token);

            // Animate each text one after another
            await AnimateBounce(_yourScoreLable.rectTransform, _punchScale, _punchDuration);

            await UniTask.Delay(TimeSpan.FromSeconds(_delayBetween), cancellationToken: _tokenSource.Token);

            await AnimateBounce(_scoreNumber.rectTransform, _punchScale, _punchDuration);

            await UniTask.Delay(TimeSpan.FromSeconds(_delayBetween), cancellationToken: _tokenSource.Token);

            await AnimateBounce(_tapLable.rectTransform, _punchScale, _punchDuration);
        }

        public override async UniTask Hide()
        {
            if (_tokenSource.IsCancellationRequested)
                return;

            if (!gameObject.activeInHierarchy)
                return;

            await _canvasGroup.DOFade(0, _pannelFadeDuration).SetEase(Ease.InQuad).WithCancellation(_tokenSource.Token);

            if (_tokenSource.IsCancellationRequested)
                return;

            gameObject.SetActive(false);
        }

        private async UniTask AnimateBounce(RectTransform target, float targetScale, float duration)
        {
            await target.DOScale(Vector3.one * targetScale, duration)
                .SetEase(Ease.OutBack)
                .WithCancellation(_tokenSource.Token);

            // Return to original scale
            await target.DOScale(Vector3.one, 0.15f)
                .SetEase(Ease.InQuad)
                .WithCancellation(_tokenSource.Token);
        }

        private void OnPanelClick()
        {
            OnTouch?.Invoke();
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}