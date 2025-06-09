using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Assets._Project.Scripts.UI
{
    public interface IReloadUI //: IService
    {
        UniTask Show();
        UniTask Hide();
    }
    public class ReloadUIPanel : UIPanel, IReloadUI
    {
        [Header("References")]
        [SerializeField] private RectTransform _parentCanvas;
        [SerializeField] private RectTransform _circleRect;

        [Header("Animation Settings")]
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private double panelHideDelay = 0.1f;

        private CancellationTokenSource _tokenSource;

        public void Init()
        {
            _tokenSource = new CancellationTokenSource();
        }

        public override async UniTask Show()
        {
            if (_tokenSource.IsCancellationRequested)
                return;

            if (gameObject.activeInHierarchy)
                return;

            _circleRect.sizeDelta = Vector2.zero;
            gameObject.SetActive(true);

            float maxRadius = GetMaxScreenRadius();

            await _circleRect
                .DOSizeDelta(Vector2.one * maxRadius * 2f, animationDuration)
                .SetEase(Ease.OutCubic)
                .WithCancellation(_tokenSource.Token);
        }

        public override async UniTask Hide()
        {
            if (_tokenSource.IsCancellationRequested)
                return;

            if (!gameObject.activeInHierarchy)
                return;

            await UniTask.Delay(TimeSpan.FromSeconds(panelHideDelay), cancellationToken: _tokenSource.Token);

            await _circleRect
                .DOSizeDelta(Vector2.zero, animationDuration)
                .SetEase(Ease.InCubic)
                .WithCancellation(_tokenSource.Token);

            if (_tokenSource.IsCancellationRequested)
                return;

            gameObject.SetActive(false);
        }

        private float GetMaxScreenRadius()
        {
            float width = _parentCanvas.rect.width;
            float height = _parentCanvas.rect.height;
            return Mathf.Sqrt(width * width + height * height) * 0.5f;
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}