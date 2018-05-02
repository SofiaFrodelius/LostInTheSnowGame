using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    AnimationCurve ac;
    float duration;
    Vector3 startPos;
    Timer timer = new Timer(0f);

    private void Start()
    {
        startPos = transform.localPosition;
    }
    public void ToggleShake(AnimationCurve intencityCurve, float Duration)
    {
        ac = intencityCurve;
        duration = Duration;
        timer.ResetTimer();
        Debug.Log("print");
        StartCoroutine(ShakeIt());
    }
    IEnumerator ShakeIt()
    {
        
        while (timer.Time < duration) {
            timer.AddTime(Time.deltaTime);
            //wiggle wiggle wiggle

            float magnitude = ac.Evaluate(timer.Time);
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(startPos.z + x, startPos.y + y, startPos.z);

            Debug.Log("Skakar kameran i " + (duration - timer.Time) + " sekunder till");
            yield return Time.deltaTime;
        }
        transform.localPosition = startPos;
        print("shake done");
    }
}
