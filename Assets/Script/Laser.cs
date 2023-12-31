using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private EdgeCollider2D col;
    [SerializeField] private float rayDistance;
    [SerializeField] private Vector3 rayDirection;
    [SerializeField] private Vector3 startOffset;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
        Vector3 laserStartPos = transform.position;
        laserStartPos += startOffset;
        lr.SetPosition(0, laserStartPos);
        Vector3 rayEnd = laserStartPos;
        rayEnd.x += rayDistance;
        lr.SetPosition(1, rayEnd);

        Vector2[] colliderPoints;
        colliderPoints = col.points;
        colliderPoints[0].x =startOffset.x;
        colliderPoints[0].y = startOffset.y;
        colliderPoints[1].x = rayEnd.x;
        col.points = colliderPoints;
    }

    private void hitRay()
    {
        if (rayDirection.y == 0)
        {
            Vector3 rayEnd = transform.position + startOffset;
            rayEnd.x += rayDistance;

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position + startOffset, rayDirection, rayDistance);
            if (hit.Length != 0)
            {
                foreach (var item in hit)
                {
                    if (item.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        float colliderSizeX = item.point.x - transform.position.x + startOffset.x;
                        rayEnd.x = item.point.x;

                        lr.SetPosition(1, rayEnd);
                        Vector2[] colliderPoints;
                        colliderPoints = col.points;
                        colliderPoints[1].x = colliderSizeX - 0.5f;
                        colliderPoints[1].y = colliderPoints[0].y;
                        col.points = colliderPoints;
                        return;
                    }
                }

            }

            lr.SetPosition(1, rayEnd);
            col.points[1] = rayEnd;
        }
        else
        {
            Vector3 rayEnd = transform.position + startOffset;

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position + startOffset, rayDirection, rayDistance);
            if (hit.Length != 0)
            {
                foreach (var item in hit)
                {
                    if (item.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        float colliderSizeY = item.point.y - transform.position.y + startOffset.y;
                        rayEnd.y = item.point.y;

                        lr.SetPosition(1, rayEnd);
                        Vector2[] colliderPoints;
                        colliderPoints = col.points;
                        colliderPoints[1].y = colliderSizeY - 0.5f;
                        colliderPoints[1].x = colliderPoints[0].x;
                        col.points = colliderPoints;
                        return;
                    }
                }

            }

            lr.SetPosition(1, rayEnd);
            col.points[1] = rayEnd;
        }
    }
    private void FixedUpdate()
    {
        hitRay();
    }
}
