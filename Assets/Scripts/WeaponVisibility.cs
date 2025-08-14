using System.Linq;
using UnityEngine;

public class WeaponVisibility : MonoBehaviour
{
    private SpriteRenderer[] renderers;

    void Awake() { Refresh(); SetVisible(false); }   // oyuna gizli baþla
    void OnEnable() { Refresh(); }
    void Start() { Refresh(); }                       // Awake sýrasý için emniyet
    void OnTransformChildrenChanged() { Refresh(); }   // child eklenince/silinince otomatik

    public void Refresh()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>(true);
        // Debug: kaç renderer bulunduðunu gör
        Debug.Log($"[WeaponVisibility] Renderer count = {renderers.Length}", this);
    }

    public void SetVisible(bool v)
    {
        if (renderers == null) return;
        foreach (var r in renderers) if (r) r.enabled = v;
    }

    // Animasyon event için istersen:
    public void WeaponOn() => SetVisible(true);
    public void WeaponOff() => SetVisible(false);
}
