using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    
    private Vector3 lastMousePosition;
    
    [SerializeField]
    private LayerMask placementLayerMask;


    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = MainCamera.nearClipPlane;
        Ray ray = MainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastMousePosition = hit.point;
        }
        return lastMousePosition;
    }
}
