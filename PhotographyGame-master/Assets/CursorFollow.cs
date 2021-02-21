using UnityEngine;

public class CursorFollow : MonoBehaviour
{

    Vector3 cursorPosition; //essentially 2D worldspace
    Vector3 position = new Vector3(0f, 0f, 0f);

    Rigidbody rb;
    
    public float trailSpeed = 0.1f;

    public GameObject[] cats;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(new Vector3(cursorPosition.x, cursorPosition.y, 5));
        position = Vector3.Lerp(transform.position, cursorPosition, trailSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
        for (int i = 0; i < cats.Length; i++)
        {
            Vector3 catPosition = new Vector3(position.x + (i + 3), position.y + (i), position.z);
            cats[i].GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(catPosition, cursorPosition, trailSpeed + (i + trailSpeed/2)));

        }
    }
}
