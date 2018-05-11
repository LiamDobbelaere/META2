using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Race : MonoBehaviour, IGameEvent {
    private Global global;
    private Transform checkpoints;

    public AudioClip music;
    public int laps = 1;
    public int currentLaps = 0;

    // Use this for initialization
    void Start () {
        global = GameObject.Find("Global").GetComponent<Global>();
        checkpoints = transform.Find("Checkpoints");
        HideAllCheckpoints();
    }

    // Update is called once per frame
    void Update () {
		if (currentLaps >= laps)
        {
            transform.Find("EventMarker").gameObject.SetActive(true);
            HideAllCheckpoints();
            GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>().Stop();
            global.objectiveMarker.GetComponent<Image>().enabled = false;
        }
	}

    public void StartEvent()
    {
        HideAllCheckpoints();

        GameObject child = checkpoints.GetChild(0).gameObject;
        child.GetComponent<RaceCheckpoint>().ResetState();
        child.SetActive(true);

        GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>().Play(music);

        currentLaps = 0;
    }

    private void HideAllCheckpoints()
    {
        GameObject child;
        for (int i = 0; i < checkpoints.childCount; i++)
        {
            child = checkpoints.GetChild(i).gameObject;
            child.GetComponent<RaceCheckpoint>().ResetState();
            child.SetActive(false);
        }
    }

    public void AdvanceCheckpoint(int siblingIndex)
    {
        GameObject child = checkpoints.GetChild((siblingIndex + 1) % checkpoints.childCount).gameObject;
        child.GetComponent<RaceCheckpoint>().ResetState();
        child.SetActive(true);

        if (siblingIndex + 1 >= checkpoints.childCount) currentLaps += 1;
    }
}
