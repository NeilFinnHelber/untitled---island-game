using System;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    //[SerializeField] private GameObject mouseIndicator;
        
    [SerializeField]
    private GameObject cellIndicator;
    [SerializeField]
    private MouseInput inputManager;
    [SerializeField]
    private Grid grid;

    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
        //mouseIndicator.transform.position = mousePosition;
    }
}
