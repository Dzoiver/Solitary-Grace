using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class RaycastGizmo : MonoBehaviour
{
    public float rayLength = 5f;
    public LayerMask layerMask;
    public Color hitColor = Color.red;
    public Color missColor = Color.green;
    [SerializeField] GameObject directionObject;

    private void OnDrawGizmos()
    {
        // Начальная позиция и направление
        Vector3 origin = transform.position;
        Vector3 direction = (directionObject.transform.position - origin).normalized;

        // Если попал во что-то
        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayLength, layerMask))
        {
            Gizmos.color = hitColor;
            Gizmos.DrawLine(origin, hit.point);

            // Дополнительная отрисовка попадания через сферу
            Gizmos.DrawSphere(hit.point, 0.05f); // точка попадания
        }
        else // Если не попал просто отрисовать луч по направлению + расстоянию 
        {
            Gizmos.color = missColor;
            Gizmos.DrawLine(origin, origin + direction * rayLength);
        }
    }
}
