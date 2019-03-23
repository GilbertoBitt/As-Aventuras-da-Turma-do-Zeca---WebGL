using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public class QuizQuestions {

	[TextAreaAttribute]
	public string question;
	public List<string> alternatives = new List<string>();
	private char[] charQuestion = new char[20];

	/// <summary>
	/// Called when the script is loaded or a value is changed in the
	/// inspector (Called in the editor only).
	/// </summary>
	void OnValidate(){
		if(alternatives.Count < 4){
			int temp = 4 - alternatives.Count;
			for (int i = 0; i < temp; i++){
				string newString = "null";
				alternatives.Add(newString);
			}
		}

		if(question == null || question == ""){
			question = "Enter a question";
		}

		for (int i = 0; i < question.Length; i++){
			charQuestion[i] = question[i];
		}

		if(charQuestion[charQuestion.Length].ToString() != "?"){
			//Debug.Log(charQuestion[charQuestion.Length].ToString());
		}
	}



}
