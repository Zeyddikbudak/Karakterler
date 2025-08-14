using UnityEngine;

public class ToggleWeaponSMB : StateMachineBehaviour
{
    public bool visibleOnEnter = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var vis = animator.GetComponentInChildren<WeaponVisibility>(true);
        if (vis) vis.SetVisible(visibleOnEnter);
        // Debug için:
        // Debug.Log($"[SMB ENTER] {stateInfo.shortNameHash} -> visible={visibleOnEnter}");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var vis = animator.GetComponentInChildren<WeaponVisibility>(true);
        if (vis) vis.SetVisible(false);
        // Debug.Log($"[SMB EXIT] {stateInfo.shortNameHash} -> visible=false");
    }
}
