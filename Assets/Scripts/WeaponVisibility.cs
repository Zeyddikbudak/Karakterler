using System.Linq;
using UnityEngine;

public class WeaponVisibility : MonoBehaviour
{
    private SpriteRenderer[] renderers;

    void Awake() { Refresh(); SetVisible(false); }   // oyuna gizli ba�la
    void OnEnable() { Refresh(); }
    void Start() { Refresh(); }                       // Awake s�ras� i�in emniyet
    void OnTransformChildrenChanged() { Refresh(); }   // child eklenince/silinince otomatik

    public void Refresh()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>(true);
        // Debug: ka� renderer bulundu�unu g�r
        Debug.Log($"[WeaponVisibility] Renderer count = {renderers.Length}", this);
    }

    public void SetVisible(bool v)
    {
        if (renderers == null) return;
        foreach (var r in renderers) if (r) r.enabled = v;
    }

    // Animasyon event i�in istersen:
    public void WeaponOn() => SetVisible(true);
    public void WeaponOff() => SetVisible(false);
}
