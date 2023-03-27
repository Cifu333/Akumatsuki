using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour {
    public float duration = 1.0f;

    public AnimationCurve curve;
    private void Update() {

    }

    public IEnumerator Shaking() {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}