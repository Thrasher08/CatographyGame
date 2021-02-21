using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snappable : MonoBehaviour
{
    public string snapName;

    public CatData cat;

    Snapshot snapshot;
    private void Start()
    {
        snapshot = Snapshot.Instance;
    }

    private void OnBecameVisible()
    {
        if (snapshot == null) return;
        snapshot.AddToInView(this);
    }
    private void OnBecameInvisible()
    {   
        if (snapshot == null) return;
        snapshot.RemoveFromInView(this);
    }
}
