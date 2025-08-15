using UnityEngine;
using UnityEngine.Events;

public class BedClickable : MonoBehaviour
{
    [Header("Inspector'dan ba�lay�n")]
    public UnityEvent onClick;

    void OnMouseDown()
    {
        onClick?.Invoke();
        Debug.Log("Yatak t�kland�!");
    }
}
