using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatData", menuName = "Cat Data", order =0)]
public class CatData : ScriptableObject
{
    public string catName;
    public string description;
}
