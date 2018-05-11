using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    private Text myText;
    private int count = -1;
    private float time;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
	}

    public void Reset()
    {
        count = 3;
        time = 0f;
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;

        if (time > 1f)
        {
            count -= 1;
            time = 0f;
        }

        if (count > 0)
        {
            myText.text = count.ToString();
        }
        else
        {
            myText.text = "GO!";
        }


        if (count < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
