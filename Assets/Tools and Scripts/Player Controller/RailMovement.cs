using System;
using UnityEngine;

public class RailMovement : MonoBehaviour
{
    static readonly Color GizmoColor = Color.cyan;
    const float GizmoTriangleAngle = 140f;
    const float GizmoTriangleSize = 0.1f;

    [Header("Parameters")]
    [SerializeField] Position2D startPos;
    [SerializeField] Position2D endPos;
    [Range(0f, 2f)]
    [SerializeField] float curvatureSoftness = 0.25f;
    [Range(-180f, 180f)]
    [SerializeField] float objectInnateRotation = 0f;

    public void MoveAt(float t)
    {
        t = Mathf.Clamp01(t);

        Vector2 p0 = startPos.position;
        Vector2 p3 = endPos.position;

        Vector2 dir0 = Quaternion.Euler(0, 0, startPos.rotation + objectInnateRotation) * Vector2.right;
        Vector2 dir3 = Quaternion.Euler(0, 0, endPos.rotation + objectInnateRotation) * Vector2.left;

        float dynamicHandleLength = Vector2.Distance(p0, p3) * curvatureSoftness;

        Vector2 p1 = p0 + dir0.normalized * dynamicHandleLength;
        Vector2 p2 = p3 + dir3.normalized * dynamicHandleLength;

        transform.position = CalculateBezierPoint(t, p0, p1, p2, p3);
        transform.eulerAngles = new Vector3(0, 0, GetAngleOnCurve(t, p0, p1, p2, p3) - objectInnateRotation);
    }

    Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        return uuu * p0 +
               3 * uu * t * p1 +
               3 * u * tt * p2 +
               ttt * p3;
    }

    Vector2 CalculateBezierTangent(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;

        return 3 * u * u * (p1 - p0) +
               6 * u * t * (p2 - p1) +
               3 * t * t * (p3 - p2);
    }

    float GetAngleOnCurve(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        Vector2 tangent = CalculateBezierTangent(t, p0, p1, p2, p3);
        return Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
    }

#if UNITY_EDITOR
    [Header("Gizmo Settings")]
    [Range(2, 128)]
    [SerializeField] int gizmoCurveResolution = 16;

    void OnDrawGizmos() // Code duplication.
    {
        Vector2 p0 = startPos.position;
        Vector2 p3 = endPos.position;

        Vector2 dir0 = Quaternion.Euler(0, 0, startPos.rotation + objectInnateRotation) * Vector2.right;
        Vector2 dir3 = Quaternion.Euler(0, 0, endPos.rotation + objectInnateRotation) * Vector2.left;

        float dynamicHandleLength = Vector2.Distance(p0, p3) * curvatureSoftness;

        Vector2 p1 = p0 + dir0.normalized * dynamicHandleLength;
        Vector2 p2 = p3 + dir3.normalized * dynamicHandleLength;

        Gizmos.color = GizmoColor;

        Vector2 prevPoint = p0;
        for (int i = 1; i <= gizmoCurveResolution; i++)
        {
            float t = i / (float)gizmoCurveResolution;
            Vector2 point = CalculateBezierPoint(t, p0, p1, p2, p3);
            Gizmos.DrawLine(prevPoint, point);
            prevPoint = point;
        }

        float triangleSizeRatio = Vector2.Distance(p0, p3);

        DrawTriangleGizmo(p0, startPos.rotation + objectInnateRotation, triangleSizeRatio);
        DrawTriangleGizmo(p3, endPos.rotation + objectInnateRotation, triangleSizeRatio);
    }

    void DrawTriangleGizmo(Vector3 position, float rotation, float sizeRatio)
    {
        float size = GizmoTriangleSize * sizeRatio;
        Vector3 tip = position + (Quaternion.Euler(0, 0, rotation) * Vector2.right) * size;
        
        Vector3 baseLeft = position + 0.5f * size * (Quaternion.Euler(0, 0, rotation + GizmoTriangleAngle) * Vector2.right);
        Vector3 baseRight = position + 0.5f * size * (Quaternion.Euler(0, 0, rotation - GizmoTriangleAngle) * Vector2.right);

        Gizmos.DrawLine(tip, baseLeft);
        Gizmos.DrawLine(tip, baseRight);
        Gizmos.DrawLine(baseLeft, baseRight);
    }
#endif
}
