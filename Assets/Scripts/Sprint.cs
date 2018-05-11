using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprint : MonoBehaviour, IGameEvent {
    private Global global;
    private Transform checkpoints;

    public int currentLaps = 0;

    // Use this for initialization
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        checkpoints = transform.Find("Checkpoints");
        HideAllCheckpoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLaps >= 1)
        {
            transform.Find("EventMarker").gameObject.SetActive(true);
            transform.Find("EventMarker").GetComponent<EventMarker>().EventOver();
            HideAllCheckpoints();
            global.objectiveMarker.GetComponent<Image>().enabled = false;
            currentLaps = 0;
        }
    }

    public void StartEvent()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>().boostAmount = 200f;
        HideAllCheckpoints();

        GameObject child = checkpoints.GetChild(0).gameObject;
        child.GetComponent<SprintCheckpoint>().ResetState();
        child.SetActive(true);

        currentLaps = 0;
    }

    private void HideAllCheckpoints()
    {
        GameObject child;
        for (int i = 0; i < checkpoints.childCount; i++)
        {
            child = checkpoints.GetChild(i).gameObject;
            child.GetComponent<SprintCheckpoint>().ResetState();
            child.SetActive(false);
        }
    }

    public void AdvanceCheckpoint(int siblingIndex)
    {
        GameObject child = checkpoints.GetChild((siblingIndex + 1) % checkpoints.childCount).gameObject;
        child.GetComponent<SprintCheckpoint>().ResetState();
        child.SetActive(true);

        if (siblingIndex + 1 >= checkpoints.childCount) currentLaps += 1;
    }
}
