using DG.Tweening;
using UnityEngine;

public class MultiPointsAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private Ease ease = Ease.Linear;
    [SerializeField] private Transform[] points;

    private Sequence anim;

    [ContextMenu(nameof(PlayForward), false, 0)]
    public void PlayForward()
    {
        anim?.Kill();
        anim = DOTween.Sequence();

        float partialDuration = duration / points.Length;
        foreach (Transform point in points)
        {
            anim.Append(transform.DOMove(point.position, partialDuration).SetEase(ease));
        }
    }

    [ContextMenu(nameof(PlayBackward), false, 0)]
    public void PlayBackward()
    {
        anim?.Kill();
        anim = DOTween.Sequence();

        float partialDuration = duration / points.Length;
        for (int i = points.Length - 1; i >= 0; i--)
        {
            Transform point = points[i];
            anim.Append(transform.DOMove(point.position, partialDuration).SetEase(ease));
        }
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(PlayBackward), true)]
    [ContextMenu(nameof(PlayForward), true)]
    private bool CheckRunTime()
    {
        return UnityEditor.EditorApplication.isPlaying;
    }
#endif
}
