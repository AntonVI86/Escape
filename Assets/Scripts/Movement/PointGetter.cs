using UnityEngine;

public class PointGetter
{
    public Vector3 GetPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Ground ground))
            {
                return new Vector3(hit.point.x, 0, hit.point.z);
            }
        }

        return Vector3.zero;
    }
}
