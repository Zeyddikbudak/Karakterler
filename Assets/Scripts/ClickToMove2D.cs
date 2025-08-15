using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToMove2D : MonoBehaviour
{
    [Header("Hareket")]
    [SerializeField] float speed = 3.0f;
    [SerializeField] float stoppingDistance = 0.05f;

    [Header("Zemin Filtresi")]
    [SerializeField] LayerMask walkableMask;   // Walkable layer'ını seç

    [Header("Animasyon")]
    [SerializeField] string isWalkingParam = "IsWalking";

    [Header("Görsel Mirror")]
    [SerializeField] Transform visualRoot;      // Visual child'ını bırak

    Rigidbody2D rb;
    Animator anim;
    Camera cam;

    Vector2 targetPos;
    bool hasTarget;
    float baseVisualScaleX = 1f;
    int isWalkingHash;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>(true);  // child'ta da bul
        cam = Camera.main;

        if (visualRoot != null) baseVisualScaleX = Mathf.Abs(visualRoot.localScale.x);
        isWalkingHash = Animator.StringToHash(isWalkingParam);

        // TEŞHİS: Animator ve parametre var mı?
        if (anim == null)
            Debug.LogError("[ClickToMove2D] Animator bulunamadı! Karakter altında bir Animator olmalı.");
        else
        {
            bool hasParam = anim.parameters.Any(p => p.type == AnimatorControllerParameterType.Bool && p.name == isWalkingParam);
            if (!hasParam)
                Debug.LogError($"[ClickToMove2D] Animator'da '{isWalkingParam}' adlı Bool parametre yok!");
        }

        targetPos = transform.position;
    }

    void Update()
    {
        // UI üstüne tıklamayı alma
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 m = cam.ScreenToWorldPoint(Input.mousePosition);
            m.z = 0f;

            // Walkable'da mı?
            Collider2D hit = Physics2D.OverlapPoint(m, walkableMask);
            if (hit == null) hit = Physics2D.OverlapCircle(m, 0.02f, walkableMask);

            if (hit != null)
            {
                targetPos = m;
                hasTarget = true;

                // anında yüze dön
                FaceByDirection(targetPos.x - rb.position.x);
            }
        }

        bool walking = hasTarget && (Vector2.Distance(rb.position, targetPos) > stoppingDistance);
        if (anim) anim.SetBool(isWalkingHash, walking);
    }

    void FixedUpdate()
    {
        if (!hasTarget) return;

        Vector2 pos = rb.position;
        Vector2 toTarget = targetPos - pos;
        float dist = toTarget.magnitude;

        if (dist <= stoppingDistance)
        {
            hasTarget = false;
            rb.linearVelocity = Vector2.zero; // kaymayı kesin durdur
            if (anim) anim.SetBool(isWalkingHash, false);
            return;
        }

        Vector2 dir = toTarget / Mathf.Max(dist, 0.0001f);
        rb.MovePosition(pos + dir * speed * Time.fixedDeltaTime);

        FaceByDirection(dir.x);
    }

    void FaceByDirection(float xDir)
    {
        if (visualRoot == null || Mathf.Abs(xDir) < 0.001f) return;
        var s = visualRoot.localScale;
        s.x = baseVisualScaleX * (xDir < 0f ? -1f : 1f);
        visualRoot.localScale = s;
    }
}
