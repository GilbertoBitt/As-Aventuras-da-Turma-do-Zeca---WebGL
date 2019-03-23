using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CanvasGroupAlphaPlayableMixerBehaviour : PlayableBehaviour
{
    float m_DefaultAlpha;

    float m_AssignedAlpha;

    CanvasGroup m_TrackBinding;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as CanvasGroup;

        if (m_TrackBinding == null)
            return;

        if (!Mathf.Approximately(m_TrackBinding.alpha, m_AssignedAlpha))
            m_DefaultAlpha = m_TrackBinding.alpha;

        int inputCount = playable.GetInputCount ();

        float blendedAlpha = 0f;
        float totalWeight = 0f;
        float greatestWeight = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<CanvasGroupAlphaPlayableBehaviour> inputPlayable = (ScriptPlayable<CanvasGroupAlphaPlayableBehaviour>)playable.GetInput(i);
            CanvasGroupAlphaPlayableBehaviour input = inputPlayable.GetBehaviour ();
            
            blendedAlpha += input.alpha * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }
        }

        m_AssignedAlpha = blendedAlpha + m_DefaultAlpha * (1f - totalWeight);
        m_TrackBinding.alpha = m_AssignedAlpha;
    }
}
