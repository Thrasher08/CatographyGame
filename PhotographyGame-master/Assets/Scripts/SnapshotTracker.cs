using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SnapshotTracker : MonoBehaviour
{
    private static SnapshotTracker instance;
    public static SnapshotTracker Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SnapshotTracker>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "SnapshotTracker";
                    instance = go.AddComponent<SnapshotTracker>();
                }
            }
            return instance;
        }
    }

    public List<SnapData> snaps;
    public Dictionary<CatData, List<SnapData>> cats;

    public int fileCounter = 0;
    public static string SnapshotPathPrefix { get { return Application.dataPath + "/Snapshots/"; } }

    private void Start()
    {
        snaps = new List<SnapData>();
        cats = new Dictionary<CatData, List<SnapData>>();
    }

    public void AddSnappableWithTexture(List<Snappable> snappables, Texture2D snapshotTexture) {

        var relativePath = SaveSnapshot(snapshotTexture);
        var snapData = new SnapData(relativePath, snappables);
        snaps.Add(snapData);
        foreach (Snappable snap in snappables)
        {
            if (snap.cat == null) return;
            if (cats.ContainsKey(snap.cat) == false || cats[snap.cat] == null)
            {
                cats.Add(snap.cat, new List<SnapData>());
            }
            cats[snap.cat].Add(snapData);

            CatTracker.Instance.AddCat(snap.cat);
        }
    }

    public void AddTextureOnly(Texture2D snapshotTexture) {
        var relativePath = SaveSnapshot(snapshotTexture);
        snaps.Add(new SnapData(relativePath, new List<Snappable>()));
    }

    string SaveSnapshot(Texture2D image)
    {
        var bytes = image.EncodeToPNG();

        var relativePath = fileCounter + ".png";
        var path = SnapshotPathPrefix + relativePath;

        File.WriteAllBytes(path, bytes);
        fileCounter++;
        return relativePath;
    }
}

public struct SnapData
{
    public string relativePath;
    public List<Snappable> snappables;

    public SnapData(string _relativePath, List<Snappable> _snappables)
    {
        this.relativePath = _relativePath;
        this.snappables = _snappables;
    }
}
