using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalScroll : MonoBehaviour
{

    [Tooltip ("Game units per second")]
    [SerializeField] float scrollSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(processTask());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(scrollSpeed * Time.deltaTime, 0.0f ));
    }

    IEnumerator processTask()
    {
        yield return new WaitForSecondsRealtime(20);

        scrollSpeed = scrollSpeed*1.5f;
    }
}
