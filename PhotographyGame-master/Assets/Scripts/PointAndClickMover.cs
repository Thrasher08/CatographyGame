using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickMover : MonoBehaviour
{

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GoToClickPoint();
        }
    }

    void GoToClickPoint() {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Floor");

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            //if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit, 100, mask, QueryTriggerInteraction.Ignore))
        {
            agent.destination = hit.point;
        }
    }
}
