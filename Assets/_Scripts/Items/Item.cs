using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemData;

    private void Awake()
    {
        if(itemData == null)
        {
            Debug.LogError($"No data is being passed, please assign a scriptable object and try again.");
        }
    }
}
