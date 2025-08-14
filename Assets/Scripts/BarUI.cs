using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    // 0..1 arasý deðer bekler
    public void SetNormalized(float t)
    {
        t = Mathf.Clamp01(t);
        fillImage.fillAmount = t;
    }
}
