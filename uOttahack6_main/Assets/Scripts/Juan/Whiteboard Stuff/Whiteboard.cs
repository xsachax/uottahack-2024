using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D tex;
    public Vector2 texSize = new Vector2(2560, 2560);
    
    // Start is called before the first frame update
    void Start()
    {
        var r = GetComponent<Renderer>();
        tex = new Texture2D((int)texSize.x, (int)texSize.y);
        tex.SetPixels(
            0, 0, (int)texSize.x, (int)texSize.y,
            new Color[(int)texSize.x * (int)texSize.y] // initialize with transparent pixels
            );
        r.material.mainTexture = tex;
    }

    public void Clear()
    {
        Start();
    }
}
