using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase whiteTitle;

    public GameObject prefab1, prefab2;

    private PlaceableObject buildingToPlace;

    #region unity methods;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        
        //this adds the objects when a key is pressed on the keyboard
        ///later add the buttons from the dropdown
        if (Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(prefab1);
        } else if (Input.GetKeyDown(KeyCode.B))
        {
            InitializeWithObject(prefab2);
        }

        if (!buildingToPlace)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            buildingToPlace.Rotate();
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanBePlaced(buildingToPlace))
            {
                buildingToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(buildingToPlace.GetStartPosition());
                TakeArea(start, buildingToPlace.Size);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(buildingToPlace.gameObject);
            }
            {
                
            }
        }
    }

    #endregion

    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 worldPosition)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(worldPosition);
        worldPosition = grid.GetCellCenterWorld(cellPos);
        return worldPosition;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y *  area.size.z];
        int counter = 0;
        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }
    
    

    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);
        
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        buildingToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }

    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(buildingToPlace.GetStartPosition());
        area.size = placeableObject.Size;
        
        TileBase[] baseArray = GetTilesBlock(area, tilemap);

        foreach (var b in baseArray)
        {
            if (b == whiteTitle)
            {
                return false;
            }
        }
        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        tilemap.BoxFill(start, whiteTitle, start.x, start.y,start.x + size.x, start.y + size.y);
    }

    #endregion
}
