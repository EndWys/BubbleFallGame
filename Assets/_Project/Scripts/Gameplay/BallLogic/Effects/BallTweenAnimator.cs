using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

public class BallTweenAnimator : MonoBehaviour
{
    private Vector3 _initialScale;
    private Quaternion _initialRotation;
    private Vector3 _initialPosition;

    private Tween _currentTween;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _initialRotation = transform.localRotation;
        _initialPosition = transform.localPosition;
    }

    public void ResetState()
    {
        _currentTween?.Kill();
        transform.localScale = _initialScale;
        transform.localRotation = _initialRotation;
        transform.localPosition = _initialPosition;
    }

    public void PlaySpawnAnimation(float duration = 0.4f)
    {
        ResetState();

        transform.localScale = Vector3.zero;

        _currentTween = transform.DOScale(_initialScale, duration)
            .SetEase(Ease.OutBack);
    }

    public async UniTask PlayPopAnimation(float duration = 0.2f)
    {
        ResetState();

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(_initialScale * 1.3f, duration * 0.25f).SetEase(Ease.OutQuad));
        seq.Append(transform.DOScale(Vector3.zero, duration * 0.75f).SetEase(Ease.InBack));

        _currentTween = seq;

        await seq;
    }

    public void PlayPreFallAnimation(float duration = 0.25f)
    {
        ResetState();

        _currentTween = transform.DOPunchScale(Vector3.one * 0.15f, duration, 10, 1f);
    }

    public void PlayAttachAnimation(float duration = 0.2f)
    {
        ResetState();

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(_initialScale * 1.2f, duration * 0.4f).SetEase(Ease.OutQuad));
        seq.Append(transform.DOScale(_initialScale, duration * 0.6f).SetEase(Ease.OutBounce));
        _currentTween = seq;
    }
}
