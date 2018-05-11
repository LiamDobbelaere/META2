using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintCheckpoint : MonoBehaviour {
    Image checkpointMark;
    private bool scored = false;
    private Global global;

    // Use this for initialization
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        checkpointMark = global.objectiveMarker.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = Camera.main.WorldToScreenPoint(transform.position);
        Rect pr = Camera.main.pixelRect;
        if (p.x > 0 && p.y > 0 && p.x < pr.width && p.y < pr.height && p.z > 0f)
        {
            checkpointMark.transform.position = p; //new Vector3(Mathf.Clamp(Mathf.Abs(p.x * Mathf.Sign(p.z)), 0f, pr.width), Mathf.Clamp(Mathf.Abs(p.y * Mathf.Sign(p.z)), 0f, pr.height), p.z);
            checkpointMark.enabled = true;
        }
        else
        {
            checkpointMark.enabled = false;
        }

        if (scored && !GetComponent<AudioSource>().isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    public void Score(string scoreName)
    {
        if (scored) return;

        if (scoreName.Contains("ScoreHigh"))
        {
            GetComponent<AudioSource>().pitch = 1f;
            Global.global.CoinBurst(transform.position, 16);
        }
        else if (scoreName.Contains("ScoreNormal"))
        {
            GetComponent<AudioSource>().pitch = 0.8f;
            Global.global.CoinBurst(transform.position, 8);
        }

        GetComponent<AudioSource>().Play();

        scored = true;
        GetComponentInParent<Sprint>().AdvanceCheckpoint(transform.GetSiblingIndex());
    }

    public void ResetState()
    {
        scored = false;
    }
}
