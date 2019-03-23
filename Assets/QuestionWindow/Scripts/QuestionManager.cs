using System.Collections.Generic;
using Sirenix.OdinInspector;

using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif

namespace QuestionWindow.Scripts{
    [CreateAssetMenu(fileName = "QuestionManager", menuName = "Manager/QuestionManager")]
    public class QuestionManager : SingletonScriptableObject<QuestionManager>{

        public GameConfig config;
        public LevelManager levelManager;
        private DataService ds;
        [Title("Todas as questões")]
        [ListDrawerSettings(IsReadOnly = true, OnBeginListElementGUI = "BeginDrawListElement", OnEndListElementGUI = "EndDrawListElement", ShowPaging = true, NumberOfItemsPerPage = 3)]
        public List<QuestionItem> allQuestions = new List<QuestionItem>();

        [Button("Load Questions")]
        public void LoadQuestions() {
            ds = config.openDB();
            allQuestions.Clear();
            List<DBOPERGUNTAS> listDePerguntas;
            if (config.sincModePerguntas == 3 && config.clientID != 1) {
                listDePerguntas = ds.GetQuestionList();
            } else if (config.sincModePerguntas == 2) {
                listDePerguntas = ds.GetQuestionListE(config.clientID);
            } else {
                listDePerguntas = ds.GetQuestionListE(1);
            }

            int tempCount = listDePerguntas.Count;
            for (int i = 0; i < tempCount; i++) {
                allQuestions.Add(ToQuestionItem(listDePerguntas[i]));
            }

            LoadAlternatives();
        }

        private void BeginDrawListElement(int index) {
#if UNITY_EDITOR
            SirenixEditorGUI.BeginBox(this.allQuestions[index].questionText);
#endif
        }

        private void EndDrawListElement(int index) {
#if UNITY_EDITOR
            SirenixEditorGUI.EndBox();
#endif
        }

        public static QuestionItem ToQuestionItem(DBOPERGUNTAS pergunta) {
            return new QuestionItem()
            {
                templateQuestion = 1,
                templateAnswer = 1,
                questionText = pergunta.textoPergunta,
                habilidade = pergunta.idHabilidade,
                idPergunta = pergunta.idPergunta
            };
        }

        public void LoadAlternatives() {
            ds = config.openDB();
            int tempCount = allQuestions.Count;
            for (int i = 0; i < tempCount; i++) {
                DBORESPOSTAS[] respostas = ds.GetAnswersList(allQuestions[i].idPergunta).ToArray();
                if (respostas.Length >= 4) {
                    allQuestions[i].alternativesText = new AltItem[4];
                    for (int j = 0; j < respostas.Length; j++) {
                        allQuestions[i].alternativesText[j] = new AltItem()
                        {
                            text = respostas[j].textoResposta,
                            isCorrect = respostas[j].correta == 1 ? true : false,
                        };
                        if (allQuestions[i].alternativesText[j].isCorrect == false) {
                            allQuestions[i].alternativesText[j].hideToggle = true;
                        } else {
                            allQuestions[i].alternativesText[j].hideToggle = false;
                        }
                    }                
                }
            }
        }
    }
}
