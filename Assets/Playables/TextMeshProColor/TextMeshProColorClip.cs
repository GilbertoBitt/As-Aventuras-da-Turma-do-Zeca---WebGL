using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TextMeshProColorClip : PlayableAsset, ITimelineClipAsset
{
    public TextMeshProColorBehaviour template = new TextMeshProColorBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextMeshProColorBehaviour>.Create (graph, template);
        return playable;
    }
}
