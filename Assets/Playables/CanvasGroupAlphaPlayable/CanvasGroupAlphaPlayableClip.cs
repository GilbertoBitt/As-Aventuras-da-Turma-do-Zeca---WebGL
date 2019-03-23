using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class CanvasGroupAlphaPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public CanvasGroupAlphaPlayableBehaviour template = new CanvasGroupAlphaPlayableBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CanvasGroupAlphaPlayableBehaviour>.Create (graph, template);
        return playable;
    }
}
