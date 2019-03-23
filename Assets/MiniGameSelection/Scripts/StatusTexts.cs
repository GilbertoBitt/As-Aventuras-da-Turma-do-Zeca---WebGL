using System.Collections.Generic;
using UnityEngine;

public class StatusTexts : ScriptableObject {

	[TextArea(0,2)]
	public List<string> statusMessages = new List<string>();
}
