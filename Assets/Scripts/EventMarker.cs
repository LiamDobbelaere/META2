using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMarker : MonoBehaviour {
    private IGameEvent gameEvent;
    private Global global;
    private GameObject eventInfo;

    public string eventName;

    private string type;

    private bool isOnMarker = false;

	// Use this for initialization
	void Start () {
        gameEvent = transform.parent.GetComponent<IGameEvent>();
        global = GameObject.Find("Global").GetComponent<Global>();
        eventInfo = global.eventInfo;
        eventInfo.transform.Find("RaceTitle").GetComponent<Text>().text = eventName;

        var image = transform.Find("Canvas").Find("Image").GetComponent<Image>();
        if (transform.parent.GetComponent<Race>() != null)
        {
            image.sprite = global.raceIcon;
            GetComponent<Renderer>().material = global.raceMarkerMaterial;
            type = "Race";
        }
        else
        {
            image.sprite = global.nullIcon;
            type = "Unknown";
        }

        eventInfo.transform.Find("RaceType").GetComponent<Text>().text = type;
    }

    // Update is called once per frame
    void Update () {
		if (isOnMarker && Input.GetButtonDown("Use"))
        {
            gameEvent.StartEvent();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, transform.eulerAngles.y, player.transform.eulerAngles.z);
            player.GetComponent<ShipController>().inactivityTime = 3f;
            global.countdown.SetActive(true);
            global.countdown.GetComponent<Countdown>().Reset();
            gameObject.SetActive(false);
            eventInfo.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        eventInfo.SetActive(true);
        isOnMarker = true;
    }

    private void OnTriggerExit(Collider other)
    {
        eventInfo.SetActive(false);
        isOnMarker = false;
    }
}
