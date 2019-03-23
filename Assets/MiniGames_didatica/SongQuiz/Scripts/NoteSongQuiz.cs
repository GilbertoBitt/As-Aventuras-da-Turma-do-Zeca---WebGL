using System;
using UnityEngine;
using UnityEngine.UI;

public class NoteSongQuiz : OverridableMonoBehaviour {

    public Vector2 SpawnPos;
    public Vector2 toPos;
    public Vector2 toPos2;
    public float beatOfThisNote;
    public ManagerSongQuiz mngr;
    public bool canLerp = false;
    public bool achivePos = false;
    public NoteClassQ.NoteType NoteType = NoteClassQ.NoteType.Right;
    public Sprite redArrowSprite;
    public Sprite blueArrowSprite;
    public Sprite SideToSIdeArrow;
    public Sprite QuestionIcon;
    public Image componentImage;
    // Update is called once per frame
    private void Update () {
        if (!canLerp) return;
        if (achivePos) {
            transform.position = Vector2.Lerp(toPos, toPos2, (mngr.beatsShownInAdvance - ((beatOfThisNote + mngr.beatsShownInAdvance) - mngr.songPosInBeats)) / mngr.beatsShownInAdvance);
        } else if (transform.position.x > toPos.x && achivePos == false) {
            transform.position = Vector2.Lerp(SpawnPos, toPos, (mngr.beatsShownInAdvance - (beatOfThisNote - mngr.songPosInBeats)) / mngr.beatsShownInAdvance);
            if(Math.Abs(transform.position.x - toPos.x) < 0.1f) {
                achivePos = true;
            }
        }

    }

    public void UpdateType(NoteClassQ.NoteType type)
    {
        NoteType = type;
        switch (NoteType)
        {
            case NoteClassQ.NoteType.Right:
                componentImage.sprite = redArrowSprite;
                break;
            case NoteClassQ.NoteType.Left:
                componentImage.sprite = blueArrowSprite;
                break;
            case NoteClassQ.NoteType.LeftAndRight:
                componentImage.sprite = SideToSIdeArrow;
                break;
            case NoteClassQ.NoteType.Question:
                componentImage.sprite = QuestionIcon;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ResetToQueue() {
        canLerp = false;
        achivePos = false;
        mngr.notePool.Enqueue(this);
        transform.position = SpawnPos;
        transform.localScale = Vector3.one;        
    }

    public void OnBecameInvisible() {
        ResetToQueue();
    }
}

