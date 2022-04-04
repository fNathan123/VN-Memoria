using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField]
    float shakeTime;
    [SerializeField]
    float origShakeStrength;
    [SerializeField]
    float shakeRange;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(ShakeUI());
        //}
    }

    public void ShakeEffect()
    {
        StartCoroutine(ShakeUI());
    }

    private IEnumerator ShakeUI()
    {

        float elapsed = 0.0f;
        Quaternion originalRotation = transform.rotation;
        Vector3 originalPos = transform.position;
        float shakeStrengh = origShakeStrength;

        while (elapsed < shakeTime)
        {

            elapsed += Time.deltaTime;
            float z = Random.value * shakeRange - (shakeRange / 2);
            transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z + z);
            
            //shake pos
            Vector3 pos = originalPos+(Vector3)(Random.insideUnitCircle * shakeStrengh);
            pos.z = originalPos.z;
            transform.position = pos;

            Mathf.Clamp(shakeStrengh -= Time.deltaTime, 0, 1);
            yield return null;
        }

        transform.rotation = originalRotation;
        transform.position = originalPos;
    }
}
