using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class RectTransform_Rotation_PlayableMixerBehaviour : PlayableBehaviour
{
    Vector3 m_DefaultLocalPosition;

    Vector3 m_AssignedLocalPosition;

    RectTransform m_TrackBinding;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as RectTransform;

        if (m_TrackBinding == null)
            return;

        if (m_TrackBinding.localPosition != m_AssignedLocalPosition)
            m_DefaultLocalPosition = m_TrackBinding.localPosition;

        int inputCount = playable.GetInputCount ();

        Vector3 blendedLocalPosition = Vector3.zero;
        float totalWeight = 0f;
        float greatestWeight = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<RectTransform_Rotation_PlayableBehaviour> inputPlayable = (ScriptPlayable<RectTransform_Rotation_PlayableBehaviour>)playable.GetInput(i);
            RectTransform_Rotation_PlayableBehaviour input = inputPlayable.GetBehaviour ();
            
            blendedLocalPosition += input.localPosition * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }
        }

        m_AssignedLocalPosition = blendedLocalPosition + m_DefaultLocalPosition * (1f - totalWeight);
        m_TrackBinding.localPosition = m_AssignedLocalPosition;
    }
}
