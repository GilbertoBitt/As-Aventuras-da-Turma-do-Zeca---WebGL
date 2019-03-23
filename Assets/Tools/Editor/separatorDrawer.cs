#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(SeparatorAttribute))]
public class separatorDrawer:DecoratorDrawer
{
	SeparatorAttribute separatorAttribute { get { return ((SeparatorAttribute)attribute); } }


	public override void OnGUI(Rect _position)
	{
		if(separatorAttribute.title == "")
		{
			_position.height = 1;
			_position.y += 20;
			GUI.Box(_position, "");
		} else
		{
			GUIStyle style = GUI.skin.box;
			//style.alignment = TextAnchor.LowerLeft;
			Vector2 textSize = style.CalcSize(new GUIContent(separatorAttribute.title));
			textSize += new Vector2 (40f, 40f);
			float separatorWidth = (_position.width - textSize.x) / 2.0f - 8.0f;
			_position.y += 20;
			var boldtext = new GUIStyle (GUI.skin.label);
			boldtext.fontStyle = FontStyle.Bold;
			boldtext.alignment  = TextAnchor.UpperCenter;
			GUI.Box(new Rect(_position.xMin, _position.yMin, separatorWidth, 1), "");
			GUI.Label(new Rect(_position.xMin + separatorWidth + 5.0f, _position.yMin - 8.0f, textSize.x, 20), separatorAttribute.title, boldtext);
			GUI.Box(new Rect(_position.xMin + separatorWidth + 10.0f + textSize.x, _position.yMin, separatorWidth, 1), "");
		}
	}

	public override float GetHeight()
	{
		return 41.0f;
	}
}
#endif