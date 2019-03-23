using System;

namespace QuestionSystem.Scripts{
	[System.Serializable]
	public class AnswersBase<T1>{
		public T1 Value;
		public Type ValueType;
		public bool IsCorrect;

		public AnswersBase(T1 value, Type valueType, bool isCorrect){
			Value = value;
			ValueType = valueType;
			IsCorrect = isCorrect;
		}

		public AnswersBase(T1 value, Type valueType){
			Value = value;
			ValueType = valueType;
		}

		public AnswersBase(){			
		}
	}
}
