using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;
using TMPro;
using DG.Tweening;
using JetBrains.Annotations;

[System.Serializable]
public class QuestDenteFurado{

    public string questionString;
   //public List<Alternatives> alternatives = new List<Alternatives>();
    public int AlternativeCorreta;
    public string[] Alternative;

    public QuestDenteFurado(){
    }

    public QuestDenteFurado(string questionString, int alternativeCorreta, string[] alternative){
        if (questionString == null) throw new ArgumentNullException("questionString");
        if (alternative == null) throw new ArgumentNullException("alternative");
        this.questionString = questionString;
        AlternativeCorreta = alternativeCorreta;
        Alternative = alternative;
    }
}
