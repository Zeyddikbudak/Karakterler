using UnityEngine;
using UnityEngine.Events;

public class BedClickable : MonoBehaviour
{
    [Header("Inspector'dan baðlayýn")]
    public UnityEvent onClick;

    void OnMouseDown()
    {
        onClick?.Invoke();
        Debug.Log("Yatak týklandý!");
    }
}
