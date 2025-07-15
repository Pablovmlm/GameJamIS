#if UNITY_EDITOR
using UnityEngine;

[ExecuteAlways]                 // opcional, pero útil
[RequireComponent(typeof(BoxCollider2D))]
public class HitboxGizmo : MonoBehaviour
{
    [SerializeField] Color color = new(1f, 0f, 0f, 0.8f);

    void OnDrawGizmos()         // ← NO OnDrawGizmosSelected
    {
        var box = GetComponent<BoxCollider2D>();
        if (!box) return;

        Gizmos.color  = color;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(box.offset, box.size);
    }
}
#endif
