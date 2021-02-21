using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTracker : MonoBehaviour
{
    private static CatTracker instance;
    public static CatTracker Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CatTracker>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "CatTracker";
                    instance = go.AddComponent<CatTracker>();
                }
            }
            return instance;
        }
    }
    public List<CatData> allCats;
    public List<CatData> currentCats;


    public void AddCat(CatData cat) {
        if (currentCats.Contains(cat)) return;
        currentCats.Add(cat);
        CheckCatProgress();
    }

    public void CheckCatProgress()
    {
        int i = 0;
        foreach (CatData cat in allCats)
        {
            if (currentCats.Contains(cat))
            {
                i++;
            }
        }
        Debug.Log("All cats caught: " + (i == allCats.Count));
    }
}
