using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TransformScalePlayableClip : PlayableAsset, ITimelineClipAsset
{
    public TransformScalePlayableBehaviour template = new TransformScalePlayableBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TransformScalePlayableBehaviour>.Create (graph, template);
        return playable;
    }
}
