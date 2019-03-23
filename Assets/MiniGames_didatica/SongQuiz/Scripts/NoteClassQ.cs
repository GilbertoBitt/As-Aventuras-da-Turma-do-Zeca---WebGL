using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class NoteClassQ{

    [InlineProperty(LabelWidth = 0)]
    [ReadOnly]
    public float positionInBeats;
    [InlineProperty(LabelWidth = 0)]
    public NoteType Type;

    public NoteClassQ(float positionInBeats, NoteType type) {
        this.positionInBeats = positionInBeats;
        this.Type = type;
    }

    public enum NoteType
    {
        Right,
        Left,
        LeftAndRight,
        Question
    }
}
