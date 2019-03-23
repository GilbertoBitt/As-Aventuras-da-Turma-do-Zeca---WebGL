using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix;
using Sirenix.OdinInspector;
//using TMPro.Examples;

namespace QuestionSystem.Scripts{
	public class QuestionBase<T2>{
		public int idPergunta;
		[ReadOnly]
		public T2 Value;
		[ReadOnly]
		public Type ValueType;
		[ReadOnly]
		public int Layout;
		[ListDrawerSettings(IsReadOnly = true)]
		public List<AnswersBase<object>> OptionAnswers = new List<AnswersBase<object>>();

		public AnswersBase<object> ReturnCorrect(){
			int tempCount = OptionAnswers.Count;
			for (int i = 0; i < tempCount; i++){
				if (OptionAnswers[i].IsCorrect){
					return OptionAnswers[i];
				}
			}
			
			return null;
		}

		public void SuffleAnswers(){
			OptionAnswers.Suffle();
		}

		
	}
	
	
}
