using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehavior : MonoBehaviour {
    private Material mat;
    private float waitTime = 0f;

    // Use this for initialization
    void Start () {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(0f, 1f, 0f), Space.World);

        if (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            float newVal = mat.mainTextureOffset.y + 0.01f;

            if (newVal > 1f)
            {
                newVal = 0f;
                waitTime = 3.5f;
            }

            mat.mainTextureOffset = new Vector2(mat.mainTextureOffset.x, newVal);
        }
    }
}
