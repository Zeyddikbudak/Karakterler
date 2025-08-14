using UnityEngine;
public class WeaponVisTest : MonoBehaviour
{
    void Update()
    {
        var vis = GetComponentInChildren<WeaponVisibility>(true);
        if (Input.GetKeyDown(KeyCode.K)) vis?.SetVisible(true);
        if (Input.GetKeyDown(KeyCode.L)) vis?.SetVisible(false);
    }
}
