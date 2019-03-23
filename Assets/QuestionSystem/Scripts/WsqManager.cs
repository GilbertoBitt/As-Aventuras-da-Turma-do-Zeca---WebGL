using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Random = System.Random;

namespace QuestionSystem.Scripts{
	[CreateAssetMenu(fileName = "NewQuestionSystem", menuName = "QuestionSystem/Manager")]
	public class WsqManager : SingletonScriptableObject<WsqManager>{

		public GameConfig GlobalConfig;
		public LevelManager LevelManager;
		private DataService _dataService;

		[DictionaryDrawerSettings(IsReadOnly = true, DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
		[ReadOnly]
		public Dictionary<int, List<int>> QuestionSkillsRelation = new Dictionary<int, List<int>>();
		[ListDrawerSettings(IsReadOnly = true, NumberOfItemsPerPage = 5, ShowPaging = true)]
		[ReadOnly]
		public List<QuestionBase<object>> Questions;

		[ReadOnly] public int PlayerAnoLetivo;

		[ButtonGroup("Dict")]
		[Button("Initialize System")]
		public void InitializeSystem(){
			if (GlobalConfig == null) return;
			QuestionSkillsRelation.Clear();
			_dataService = GlobalConfig.openDB();

			var rsult = _dataService.GetAllGamesDidaticos_Habilidades();
			
			Questions = new List<QuestionBase<object>>();

			var tempCount = rsult.Count;
			for (var i = 0; i < tempCount; i++){
				if (!QuestionSkillsRelation.ContainsKey(rsult[i].IdGameDidatico)){
					QuestionSkillsRelation.Add(rsult[i].IdGameDidatico, new List<int>());
				}

				if (!QuestionSkillsRelation[rsult[i].IdGameDidatico].Contains(rsult[i].IdHabilidade)){
					QuestionSkillsRelation[rsult[i].IdGameDidatico].Add(rsult[i].IdHabilidade);
				}
			}
			
			tempCount = QuestionSkillsRelation.Count;
			for (var i = 0; i < tempCount; i++){
				var element = QuestionSkillsRelation.ElementAt(i);
				var tempCount2 = element.Value.Count;
				for (var j = 0; j < tempCount2; j++){
					PlayerAnoLetivo = GlobalConfig.PlayerAnoLetivo;
					var qr = _dataService.GetAllPerguntas_Games(GlobalConfig.PlayerAnoLetivo);
					var tempCountQuestionsR = qr.Count;
					for (var k = 0; k < tempCountQuestionsR; k++){
						var qb = new QuestionBase<object>(){
							Layout = qr[k].layout,
							Value = qr[k].textoPergunta,
							idPergunta = qr[k].idPergunta
						};
						qb.ValueType = qb.Value.GetType();

						var rr = _dataService.GetAllRespostas_Games(qb.idPergunta);
						var rrTempCount = rr.Count;
						for (var l = 0; l < rrTempCount; l++){
							var result = new AnswersBase<object>(rr[l].TextoResposta, rr[l].TextoResposta.GetType(), (rr[l].correta == 1) ? true : false );
							qb.OptionAnswers.Add(result);
						}
						
						Questions.Add(qb);
					}
				}
			}
		}
		
		/// <summary>
		/// Pega questão randomica de acordo com os filtros basicos de habilidade e ano letivo do jogador.
		/// </summary>
		/// <returns></returns>
		public QuestionBase<System.Object> GetQuestionBase(){
			var rIndex = UnityEngine.Random.Range(0, Questions.Count);
			return Questions[rIndex];
		}
		
		/// <summary>
		/// Retorna lista de perguntas não repeditidas.
		/// </summary>
		/// <param name="amount">Quantidade de Questões que irão retornar na lista</param>
		/// <returns></returns>
		public List<QuestionBase<System.Object>> GetListQuestionBases(int amount = 4){
			Questions.Suffle();
			var returnList = new List<QuestionBase<System.Object>>();
			int tempCount = amount;
			for (int i = 0; i < tempCount; i++){
				var rIndex = UnityEngine.Random.Range(0, Questions.Count);
				returnList.Add(Questions[rIndex]);
			}
			return returnList;
		}

		public List<QuestionBase<object>> GetListQuestionLayoutBase(int amount = 4){
			return GetListQuestionLayoutBase(0, amount);
		}

		public List<QuestionBase<object>> GetListQuestionLayoutBase(int layout = 1, int amount = 4){
			Questions.Suffle();
			var returnList = new List<QuestionBase<System.Object>>();
			var questionByLayout = Questions.FindAll(t => t.Layout == layout);
			var nonRepeat = new List<int>();
			do{
				var randomIndex = UnityEngine.Random.Range(0, questionByLayout.Count);
				if (nonRepeat.Contains(randomIndex)) continue;
				nonRepeat.Add(randomIndex);
				returnList.Add(questionByLayout[randomIndex]);
			} while (returnList.Count < amount);
			return returnList;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_type">
		/// String - Puro Texto
		/// </param>
		/// <returns>Questão Randomica do tipo determinado.</returns>
		public QuestionBase<System.Object> GetQuestionBaseByType(Type _type){
			int rIndex;
			do{
				rIndex = UnityEngine.Random.Range(0, Questions.Count);
			} while (Questions[rIndex].ValueType != _type);
			return Questions[rIndex];
		}

		[Button("Close Database Connection")]
		public void CloseConnection(){
			if (_dataService != null) _dataService.CloseConnection();
		}
	}
	
	
}
