using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridGenerator : MonoBehaviour
{
    // This script for the moment is to test how to generate inventories and use a grid to represent these inventories
    // All code used is subject to change as it goes through iterations.

    public GameObject itemSlotPrefab;
    public GameObject elementGridPrefab;
    [Tooltip("This is a container UI that holds the itemGridPrefab")]
    public GameObject containerPrefab;

    public int rows;
    public int columns;
    public float cellSize;
    public const float cellSpacing = 0;

    public void Awake()
    {
        GenerateInventory(rows, columns);
    }

    private void GenerateInventory(int rows, int columns)
    {
        // Instantiate the element grid prefab and set its parent
        GameObject elementPrefab = Instantiate(elementGridPrefab, Vector2.zero, Quaternion.identity);
        elementPrefab.transform.SetParent(containerPrefab.transform, false);

        // Calculate and set the size of the RectTransform on the instantiated object
        RectTransform rectTransform = elementPrefab.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(cellSize * columns, cellSize * rows);

        RectTransform gridRectTransform = elementPrefab.transform.GetChild(0).GetComponent<RectTransform>();
        gridRectTransform.sizeDelta = new Vector2(cellSize * columns, cellSize * rows);

        Debug.Log($"Spawning {rows} is the same as doing {96 * rows}px & Spawning {columns} is the same as doing {96 * columns}px");

        if (elementPrefab != null)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < columns; k++)
                {
                    GameObject newSlot = Instantiate(itemSlotPrefab, Vector2.zero, Quaternion.identity);
                    newSlot.transform.SetParent(elementPrefab.transform.GetChild(0).transform, false);
                }
            }
        }
    }
}
