using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TransformScalePlayableMixerBehaviour : PlayableBehaviour
{
    Vector3 m_DefaultLocalScale;

    Vector3 m_AssignedLocalScale;

    Transform m_TrackBinding;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as Transform;

        if (m_TrackBinding == null)
            return;

        if (m_TrackBinding.localScale != m_AssignedLocalScale)
            m_DefaultLocalScale = m_TrackBinding.localScale;

        int inputCount = playable.GetInputCount ();

        Vector3 blendedLocalScale = Vector3.zero;
        float totalWeight = 0f;
        float greatestWeight = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<TransformScalePlayableBehaviour> inputPlayable = (ScriptPlayable<TransformScalePlayableBehaviour>)playable.GetInput(i);
            TransformScalePlayableBehaviour input = inputPlayable.GetBehaviour ();
            
            blendedLocalScale += input.localScale * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }
        }

        m_AssignedLocalScale = blendedLocalScale + m_DefaultLocalScale * (1f - totalWeight);
        m_TrackBinding.localScale = m_AssignedLocalScale;
    }
}
