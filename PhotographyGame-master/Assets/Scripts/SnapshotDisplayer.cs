using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SnapshotDisplayer : MonoBehaviour
{
    public GameObject prefab;

    public void StartLoadSnapshots() { StartCoroutine(LoadSnapshots()); }

    Dictionary<SnapData, GameObject> displayedSnaps;

    private void Start()
    {
        displayedSnaps = new Dictionary<SnapData, GameObject>();
    }

    public IEnumerator LoadSnapshots()
    {
        yield return new WaitForEndOfFrame();
        SnapshotTracker snapshotTracker = SnapshotTracker.Instance;

        if (snapshotTracker.snaps == null) yield break;

        this.GetComponent<Image>().enabled = true;

        foreach(SnapData snapData in snapshotTracker.snaps)
        {
            if (displayedSnaps.ContainsKey(snapData)) yield break;
            GameObject imageGO = (GameObject)Instantiate(prefab, this.transform);
            //var rectTransform = imageGO.GetComponent<RectTransform>();
            //var texture2D = new Texture2D(rectTransform.rect.width, rectTransform.rect.height);

            string url =  SnapshotTracker.SnapshotPathPrefix + snapData.relativePath;
            var bytes = File.ReadAllBytes(url);
            Texture2D texture = new Texture2D(2, 2);
            bool imageLoadSuccess = texture.LoadImage(bytes);
            while (!imageLoadSuccess)
            {
                print("image load failed");
                bytes = File.ReadAllBytes(url);
                imageLoadSuccess = texture.LoadImage(bytes);
            }
            print("Image load success: " + imageLoadSuccess);
            imageGO.GetComponent<Image>().overrideSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0f), 100f);

            displayedSnaps.Add(snapData, imageGO);
        }
    }
}
