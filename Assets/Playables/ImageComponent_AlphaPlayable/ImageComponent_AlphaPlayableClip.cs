using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ImageComponent_AlphaPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public ImageComponent_AlphaPlayableBehaviour template = new ImageComponent_AlphaPlayableBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ImageComponent_AlphaPlayableBehaviour>.Create (graph, template);
        return playable;
    }
}
