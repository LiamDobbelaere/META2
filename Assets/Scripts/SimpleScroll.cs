using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleScroll : MonoBehaviour {
    public float speed = 0.1f;

    private Material mat;    

    // Use this for initialization
    void Start () {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update () {
        mat.mainTextureOffset = new Vector2(mat.mainTextureOffset.x, (mat.mainTextureOffset.y + speed * Time.deltaTime) % 1f);
    }
}
