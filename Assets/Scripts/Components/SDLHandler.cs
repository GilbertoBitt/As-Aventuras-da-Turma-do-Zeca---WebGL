using System.Collections.Generic;
using UnityEngine;

public class SDLHandler : MonoBehaviour {

    private List<DBOESTATISTICA_DIDATICA> statistics = new List<DBOESTATISTICA_DIDATICA>();
    public int idDificuldade;
    public GameConfig config;

    public void SaveEstatistica(bool _isRight) {
        DBOESTATISTICA_DIDATICA statisticTemp = new DBOESTATISTICA_DIDATICA()
        {
            acertou = (_isRight) ? 1 : 0,
            idDificuldade = idDificuldade,
            idGameDidatico = config.levelManager.IDDidatico,
            idHabilidade = config.levelManager.level.levelType == LevelType.completo ? config.levelManager.level.LevelDidatico.idHabilidade : config.levelManager.level.idHabilidade,
            idLivro = config.levelManager.currentCategory.idLivro,
            dataInsert = config.ReturnCurrentDate()
        };
        if (config.currentUser != null) {
            statisticTemp.idUsuario = config.playerID;
        }
        statistics.Add(statisticTemp);
    }

    public void SaveEstatistica(int _idHabilidade, bool _isRight) {
        DBOESTATISTICA_DIDATICA statisticTemp = new DBOESTATISTICA_DIDATICA()
        {
            acertou = (_isRight) ? 1 : 0,
            idDificuldade = idDificuldade,
            idGameDidatico = config.levelManager.IDDidatico,
            idHabilidade = _idHabilidade,
            idLivro = config.levelManager.currentCategory.idLivro,
            dataInsert = config.ReturnCurrentDate()
        };
        if (config.currentUser != null) {
            statisticTemp.idUsuario = config.playerID;
        }
        statistics.Add(statisticTemp);
    }

    public void SaveEstatistica(int _idDificuldade, int _idHabilidade, bool _isRight) {
        DBOESTATISTICA_DIDATICA statisticTemp = new DBOESTATISTICA_DIDATICA()
        {
            acertou = (_isRight) ? 1 : 0,
            idDificuldade = _idDificuldade,
            idGameDidatico = config.levelManager.IDDidatico,
            idHabilidade = _idHabilidade,
            idLivro = config.levelManager.currentCategory.idLivro,
            dataInsert = config.ReturnCurrentDate()
        };
        if (config.currentUser != null) {
            statisticTemp.idUsuario = config.playerID;
        }
        statistics.Add(statisticTemp);
    }

    public void SaveEstatistica(DBOESTATISTICA_DIDATICA temp) {
        DBOESTATISTICA_DIDATICA statisticTemp = temp;
        statisticTemp.dataInsert = config.ReturnCurrentDate();
        if (config.currentUser != null) {
            statisticTemp.idUsuario = config.playerID;
        }
        statistics.Add(statisticTemp);
    }

    public void Send() {
        int count = statistics.Count;
        if (count >= 1) {
            config.SaveAllStatistic(statistics);
        }
    }
}
