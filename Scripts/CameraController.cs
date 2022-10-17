using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public float lerpSpeed;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);

        transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime);

    }
    /*
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-.5f, .5f) * magnitude;
            float y = Random.Range(-1f, 2f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }*/
}
