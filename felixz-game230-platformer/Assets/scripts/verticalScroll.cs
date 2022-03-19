using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalScroll : MonoBehaviour
{

    [Tooltip ("Game units per second")]
    [SerializeField] float scrollSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0.0f, scrollSpeed * Time.deltaTime));
    }
}
