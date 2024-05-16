using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContainerData", menuName = "ScriptableObjects/Container/ContainerDataAsset", order = 0)]
public class ContainerData : ScriptableObject
{
    public string _name;
    public int _rows;
    public int _columns;
}
