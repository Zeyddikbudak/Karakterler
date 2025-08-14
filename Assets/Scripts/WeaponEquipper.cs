using UnityEngine;

public class WeaponEquipper : MonoBehaviour
{
    public Transform weaponSocket;           // Weapon (Transform)
    public WeaponVisibility visibility;      // Weapon üzerindeki component

    private GameObject currentWeapon;

    public void EquipWeapon(GameObject prefab)
    {
        if (currentWeapon) Destroy(currentWeapon);
        if (prefab)
        {
            currentWeapon = Instantiate(prefab, weaponSocket);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            currentWeapon.transform.localScale = Vector3.one;
        }
        visibility.Refresh();                // <-- kritik
    }
}
