using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArrow : MonoBehaviour
{
    public Vector3 arrowOrigin;                 // Star position of arrow.
    public Vector3 arrowTarget;                 // End position of arrow.
    public LineRenderer cachedLineRenderer;     // Line renderer.

    void Start()
    {
        cachedLineRenderer = this.GetComponent<LineRenderer>();
        cachedLineRenderer.enabled = false;
    }
    private void Update()
    {
        updateArrow();
    }

    // Draws arrow.
    void updateArrow()
    {
        float adaptiveSize = 0.5f / Vector3.Distance(arrowOrigin, arrowTarget);
        cachedLineRenderer.SetPositions(new Vector3[] {
               arrowOrigin
               , Vector3.Lerp(arrowOrigin, arrowTarget, 0.999f - adaptiveSize)
               , Vector3.Lerp(arrowOrigin, arrowTarget, 1 - adaptiveSize)
               , arrowTarget });
    }
}
