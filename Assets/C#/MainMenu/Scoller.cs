using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoller : MonoBehaviour
{
    [SerializeField]
    private RawImage img;
    private float x = 0.01f;
    private float y = 0.01f;

    private void Awake()
    {
        img = GetComponent<RawImage>();
    }

    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
    }
}
