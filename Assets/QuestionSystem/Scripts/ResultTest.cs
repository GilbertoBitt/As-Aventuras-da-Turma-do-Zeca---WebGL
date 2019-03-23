using UnityEngine;

namespace QuestionSystem.Scripts{
	public class ResultTest : QuestionResult {
		public override void IsCorrect(bool isRight){
			Debug.Log(isRight ? "Correto" : "Errado");
		}
	}
}
