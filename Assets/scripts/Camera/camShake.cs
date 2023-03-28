using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour {
    public float duration = 0.5f;
    public bool pressToShake1;
    public bool pressToShake2;
    public bool pressToShake3;

    public AnimationCurve curve1;
    public AnimationCurve curve2;
    public AnimationCurve curve3;
    private void Update() {
        if (pressToShake1) {
            Shake1();
            pressToShake1 = false;
        }
        if (pressToShake2) {
            Shake2();
            pressToShake2 = false;
        }
        if (pressToShake3) {
            Shake3();
            pressToShake3 = false;
        }
    }

    public IEnumerator Shaking(AnimationCurve curve) {
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

    public void Shake1() {
        StartCoroutine(Shaking(curve1));

        return;
    }

    public void Shake2() {
        StartCoroutine(Shaking(curve2));

        return;
    }
    public void Shake3() {
        StartCoroutine(Shaking(curve3));

        return;
    }
}