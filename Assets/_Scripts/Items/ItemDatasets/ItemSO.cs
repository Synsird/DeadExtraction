using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public enum ItemType
{
    Weapon,
    Consumable,
    Medical,
    Throwable,
    Deployable
}

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items", order = 0)]
public class ItemSO : ScriptableObject
{
    [Header("Basic Information Variables")]
    public string id;
    public string itemName;
    public string description;

    [Header("User Interface Variables")]
    public Sprite icon;

    [Header("Item Size Variables")]
    public int columns;
    public int rows;
    public const float cellSize = 64;
}


[CustomEditor(typeof(ItemSO))]
public class IDGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ItemSO item = (ItemSO)target;

        if (GUILayout.Button("Generate Random Item"))
        {
            item.id = GenerateRandomID();
            EditorUtility.SetDirty(item); // Mark the itemSO as dirty so the editor knows to save the changes
        }
    }

    private string GenerateRandomID()
    {
        var length = 18;
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var stringBuilder = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            char c = chars[Random.Range(0, chars.Length)];
            stringBuilder.Append(c);
        }

        return "id" + stringBuilder.ToString();
    }
}