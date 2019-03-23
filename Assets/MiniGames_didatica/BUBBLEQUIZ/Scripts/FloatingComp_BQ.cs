using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using MEC;

public class FloatingComp_BQ : OverridableMonoBehaviour {

    [ReadOnly]
    public float TimeDuration;
    public float TimeDurationMax = 1.2f;
    public float TimeDurationMin = .8f;
    public float Offset;
    public Transform itemTransform;
    public float initLocalYPos;
    public float initLocalCenterYPos = 100f;
    public Sequence floatSequence;

    public void Start() {
        initLocalYPos = 0f;
    }

    [ButtonGroup("Floating")]
    [Button("Start Floating")]
    public void StartFloat() {
        Timing.RunCoroutine(StartFloatRandomTimer());
    }

    public IEnumerator<float> StartFloatRandomTimer() {
        TimeDuration = Random.Range(TimeDurationMax, TimeDurationMin);
        yield return Timing.WaitForSeconds(Random.Range(0f, 1f));
        itemTransform.transform.localPosition = new Vector3(itemTransform.localPosition.x, initLocalCenterYPos, itemTransform.localPosition.z);
        Vector3 valueFixUP = Vector3.zero;
        valueFixUP.y += Offset;
        Vector3 valueFixDow = Vector3.zero;
        valueFixDow.y -= Offset;
        floatSequence = DOTween.Sequence();
        floatSequence.SetId(898);
        floatSequence.SetLoops(-1, LoopType.Yoyo);
        floatSequence.Append(itemTransform.DOBlendableLocalMoveBy(valueFixUP, TimeDuration));
        floatSequence.Append(itemTransform.DOBlendableLocalMoveBy(valueFixDow, TimeDuration));
        floatSequence.Append(itemTransform.DOBlendableLocalMoveBy(valueFixUP, TimeDuration));
        floatSequence.Play();
    }

    [ButtonGroup("Floating")]
    [Button("Stop Floating")]
    public void StopFloat() {
        DOTween.Kill(898);
    }





}
