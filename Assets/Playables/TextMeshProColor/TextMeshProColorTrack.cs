using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using TMPro;

[TrackColor(0.9622642f, 0f, 0.05071595f)]
[TrackClipType(typeof(TextMeshProColorClip))]
[TrackBindingType(typeof(TextMeshProUGUI))]
public class TextMeshProColorTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TextMeshProColorMixerBehaviour>.Create (graph, inputCount);
    }

    // Please note this assumes only one component of type TextMeshProUGUI on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        TextMeshProUGUI trackBinding = director.GetGenericBinding(this) as TextMeshProUGUI;
        if (trackBinding == null)
            return;

        // These field names are procedurally generated estimations based on the associated property names.
        // If any of the names are incorrect you will get a DrivenPropertyManager error saying it has failed to register the name.
        // In this case you will need to find the correct backing field name.
        // The suggested way of finding the field name is to:
        // 1. Make sure your scene is serialized to text.
        // 2. Search the text for the track binding component type.
        // 3. Look through the field names until you see one that looks correct.
        driver.AddFromName<TextMeshProUGUI>(trackBinding.gameObject, "m_Color");
#endif
        base.GatherProperties (director, driver);
    }
}
