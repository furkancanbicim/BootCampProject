using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveDemo : MonoBehaviour
{
    [SerializeField] AnimationCurve _animationCurve;
    [SerializeField] float animTime;

    private void Start()
    {
        StartCoroutine(AnimationCurveCor());
    }
    IEnumerator AnimationCurveCor()
    {
        float currentTime = 0f;
        _animationCurve = new AnimationCurve(new Keyframe(0.5f, transform.position.x + 10), new Keyframe(1f, transform.position.x + 15));
        while (currentTime < animTime)
        {
            transform.position = new Vector2(_animationCurve.Evaluate(currentTime/animTime),transform.position.y);
            currentTime+=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
