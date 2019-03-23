using System.Globalization;
using UnityEngine;

public class Word1_4B : ScriptableObject {

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
//	[SeparatorAttribute("Palavra Completa")]
	#endif
	public string completeWord;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
//	[SeparatorAttribute("Número de Letras")]
	#endif
	public int numberOfChars;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
//	[SeparatorAttribute("Sílabas")]
	#endif
	public int syllablesCount;
	public string[] syllables;
    public string[] characters;
    public Type type;

    public bool custom = false;
    [InspectorButton("OnValidate")]
    public bool ForceValidade;

    public enum Type {
        none,
        monosilaba,
        disilaba,
        trisilaba
    }

    
    
	void OnValidate (){

        syllablesCount = syllables.Length;
        type = (Type)syllablesCount;

        if ((type == Type.disilaba || type == Type.monosilaba) && custom == false) {
            completeWord = this.name;
        } else if(custom == false) {
            string[] tempSy = this.name.Split('.');
            completeWord = "";
            syllables = new string[3];
            for (int i = 0; i < tempSy.Length; i++) {
                completeWord += tempSy[i];
                syllables[i] = tempSy[i];
            }

            completeWord = ToTitleCase(completeWord);
        }

		numberOfChars = completeWord.Length;
        characters = new string[name.Length];
        for (int i = 0; i < name.Length; i++) {
            characters[i] = name[i].ToString();
        }

        if(type == Type.monosilaba) {
            syllables = new string[1];
            syllables[0] = name;
        }
    }

    public string ToTitleCase(string str) {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }


}
