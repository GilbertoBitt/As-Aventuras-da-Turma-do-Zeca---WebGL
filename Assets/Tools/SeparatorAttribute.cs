#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
using UnityEngine;


public class SeparatorAttribute:PropertyAttribute
{
    public readonly string title;
	public readonly int sizeWidth;


	public SeparatorAttribute()
	{
		this.title = "";
		this.sizeWidth = 0;
	}

	public SeparatorAttribute(string _title)
	{
        this.title = _title;
		this.sizeWidth = 0;
    }

	public SeparatorAttribute(string _title,int _sizeWidth)
	{
		this.sizeWidth = _sizeWidth;

	}

}
#endif