using System.Collections.Generic;
using UnityEngine;

public class UICurveDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private RectTransform startPoint;
    [SerializeField] private RectTransform controlPoint1;
    [SerializeField] private RectTransform controlPoint2;
    [SerializeField] private RectTransform endPoint;
    [SerializeField] private RectTransform viewport;
    [SerializeField] private int resolution = 30;

    void Start()
    {
        DrawCubicCurve();
    }

    private void Update()
    {
        DrawCubicCurve();
    }

    void DrawCubicCurve()
    {
        List<Vector3> validPoints = new List<Vector3>();

        Vector2 p0 = startPoint.anchoredPosition;
        Vector2 p1 = controlPoint1.anchoredPosition;
        Vector2 p2 = controlPoint2.anchoredPosition;
        Vector2 p3 = endPoint.anchoredPosition;

        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)(resolution - 1);
            Vector2 point = CalculateCubicBezierPoint(t, p0, p1, p2, p3);

            if (IsPointInsideViewport(point))
            {
                if (point.x != 0 && point.y != 0)
                {
                    validPoints.Add(point);
                }
            }
            else
            {
                Vector2 viewportEdgePoint = ClampToViewport(point);
                if (viewportEdgePoint.x != 0 && viewportEdgePoint.y != 0)
                {
                    validPoints.Add(viewportEdgePoint);
                }
            }
        }

        lineRenderer.positionCount = validPoints.Count;
        lineRenderer.SetPositions(validPoints.ToArray());
    }

    Vector2 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector2 point = uuu * p0;
        point += 3 * uu * t * p1;
        point += 3 * u * tt * p2;
        point += ttt * p3;

        return point;
    }

    bool IsPointInsideViewport(Vector2 point)
    {
        Vector3 worldPoint = transform.TransformPoint(point);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(viewport, worldPoint, null, out localPoint);

        return viewport.rect.Contains(localPoint);
    }

    Vector2 ClampToViewport(Vector2 point)
    {
        Vector3 worldPoint = transform.TransformPoint(point);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(viewport, worldPoint, null, out localPoint);

        localPoint.x = Mathf.Clamp(localPoint.x, viewport.rect.xMin, viewport.rect.xMax);
        localPoint.y = Mathf.Clamp(localPoint.y, viewport.rect.yMin, viewport.rect.yMax);

        Vector2 clampedWorldPoint = viewport.TransformPoint(localPoint);

        return transform.InverseTransformPoint(clampedWorldPoint);
    }
}
