using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour {
    public float duration = 1.0f;
    public bool shakebb;

    public AnimationCurve curve;
    private void Update() {
        if (shakebb) {
            StartCoroutine(Shaking());
        }
        shakebb = false;
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

    public void Shake() {
        StartCoroutine(Shaking());

        return;
    }
}