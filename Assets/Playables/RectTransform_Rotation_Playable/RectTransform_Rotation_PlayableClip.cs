using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class RectTransform_Rotation_PlayableClip : PlayableAsset, ITimelineClipAsset
{
    public RectTransform_Rotation_PlayableBehaviour template = new RectTransform_Rotation_PlayableBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<RectTransform_Rotation_PlayableBehaviour>.Create (graph, template);
        return playable;
    }
}
