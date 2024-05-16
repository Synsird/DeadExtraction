using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public ContainerData containerData;

    public GameObject inventoryPrefab;
    public GameObject itemSlotPrefab;

    // cellSize represents 1 tile in the grid
    public const float cellSize = 96.0f;

    public Item[,] inventoryGrid;

    private void Start()
    {
        if(containerData != null)
        {
            rows = containerData._rows;
            columns = containerData._columns;
        }
    }

    public void OnContainerInteract()
    {
        GenerateInventoryGrid(rows, columns);
    }

    private void GenerateInventoryGrid(int rows, int columns)
    {
        GameObject newInventory = Instantiate(inventoryPrefab, Vector2.zero, Quaternion.identity);
        newInventory.transform.SetParent(GameObject.Find("Panel_Container").transform, false);

        if(newInventory != null)
        {
            TextMeshProUGUI containerTextLabel = inventoryPrefab.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            containerTextLabel.text = containerData._name;

            RectTransform borderTransform = newInventory.transform.GetChild(1).transform.GetChild(0).GetComponent<RectTransform>();
            borderTransform.sizeDelta = new Vector2(cellSize * columns, cellSize * rows);

            for(int x = 0; x < rows; x++)
            {
                for(int y = 0; y < columns; y++)
                {
                    GameObject newSlot = Instantiate(itemSlotPrefab);
                    newSlot.transform.SetParent(borderTransform.transform.GetChild(0).transform, false);
                }
            }
        }
    }
}
