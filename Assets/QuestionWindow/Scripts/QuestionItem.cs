using Sirenix.OdinInspector;
using UnityEngine;

namespace QuestionWindow.Scripts{
    [System.Serializable]
    public class QuestionItem {

        [OnValueChanged("ChangeTemplateQuestion")]
        [LabelText("Template do Enunciado")]
        public int templateQuestion;
        [OnValueChanged("ChangeTemplateAwnsers")]
        [LabelText("Template da Resposta")]
        public int templateAnswer;
        [LabelText("Ano Letivo da Pergunta")]
        public int anoLetivo;
        [LabelText("Habilidade abordada")]
        public int habilidade;
        [LabelText("Disciplina [PT = 1, MT = 2]")]
        public int disciplina;
        //[LabelText("Texto do Enunciado")]
        [HideInInspector]
        public string questionText;
        [HideInInspector]
        public int idPergunta;
        [ShowIf("ShowQuestionImage", true)]
        [LabelText("Imagem do Enunciado")]
        public Texture2D questionImage;
        [ShowIf("ShowquestionAudioClip", true)]
        [LabelText("Audio do Enunciado")]
        public AudioClip questionAudioClip;
        [ListDrawerSettings(IsReadOnly = true, DraggableItems = false)]
        public AltItem[] alternativesText;
        [ShowIf("showAlternativeImages", true)]
        //Melhorar condições com as funções externas.
        [LabelText("Imagen das Alternativas")]
        public Texture2D[] alternativesImages;
        [ShowIf("showAlternativesAudioClips", true)]
        //Melhorar condições com as funções externas.
        [LabelText("Audio das Alternativas")]
        public AudioClip[] alternativesAudioClips;

        private bool ShowQuestionImage;
        private bool ShowquestionAudioClip;

        private bool showAlternativeImages;
        private bool showAlternativesAudioClips;

        public void ChangeTemplateQuestion() {

            if(templateQuestion == 0) {
                templateQuestion = 1;
            }

            //Verificação de Template de Questão
            if(templateQuestion == 2) {
                ShowQuestionImage = true;
            } else {
                ShowQuestionImage = false;
            }

            if(templateQuestion == 3) {
                ShowquestionAudioClip = true;
            } else {
                ShowquestionAudioClip = false;
            }
        
        }

        public void ChangeTemplateAwnsers() {

            if(templateAnswer == 0) {
                templateAnswer = 1;
            }

            if(templateAnswer == 2) {
                showAlternativeImages = true;
            } else {
                showAlternativeImages = false;
            }

            if(templateAnswer == 3) {
                showAlternativesAudioClips = true;
            } else {
                showAlternativesAudioClips = false;
            }

        }
	
    }

    [System.Serializable]
    public class AltItem {
        [HideLabel]
        [HorizontalGroup("001")]
        [ReadOnly]
        public bool isCorrect;

        [HideLabel]
        [HorizontalGroup("001")]
        [ReadOnly]
        public string text;
    
   
        [HideInInspector]
        public bool hideToggle;   

        public AltItem() {
            this.text = "";
        }

        public AltItem(bool isCorrect, string text) {
            this.isCorrect = isCorrect;
            this.text = text;
        }
    }
}