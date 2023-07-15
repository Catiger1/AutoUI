using DG.Tweening;
using UnityEngine.UI;

public class ScreenMaskFadeIn : MonoSingleton<ScreenMaskFadeIn>
{
    public float duration = 1f;
    void Start()
    {
        GetComponent<Image>().DOFade(1, duration);
    }
}
