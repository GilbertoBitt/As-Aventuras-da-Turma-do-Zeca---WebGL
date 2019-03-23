using UnityEngine;
using MEC;

public class GDLHandler : MonoBehaviour {

    public GameConfig config;
    public DataService ds;
    private float startTime;
    public float timeSpend;
    private bool isTimerStoped = false;
    private bool isTimerStart = false;
    public int currentLevel;
    public string characterSelected;
    public int scoreAmount;
    public int starAmount;

    public void Start() {
        ds = config.openDB();
    }

    public void StartTimer() {
        if (isTimerStart == false) {
            isTimerStart = true;
            isTimerStoped = true;
            startTime = Time.realtimeSinceStartup;
        }
    }

    public void StopTimer() {
        if (isTimerStoped == false) {
            timeSpend = Time.realtimeSinceStartup - startTime;
            isTimerStoped = true;
            isTimerStart = false;
        }
    }

    public void SaveLog() {
        StopTimer();
        DBOGAMESDIDATICOS_LOGS GDL = new DBOGAMESDIDATICOS_LOGS()
        {
            dataAcesso = config.ReturnCurrentDate(),
            deviceID = SystemInfo.deviceUniqueIdentifier,
            fase = currentLevel,
            idGameDidatico = config.levelManager.level.levelType == LevelType.completo ? config.levelManager.level.LevelDidatico.idGameDidatico : config.levelManager.level.idGameDidatico,
            idUsuario = config.playerID,
            online = 1,
            pontos = scoreAmount,
            personagem = config.levelManager.level.useCharacterSelection ? config.levelManager.currentCharacter.name : "none",
            tempo = Mathf.FloorToInt(timeSpend)
        };

        if (config.isOnline) {
            Timing.RunCoroutine(config.NetHelper.SetGamesDidaticosLogs(GDL));
        } else {
            GDL.online = 0;
            config.openDB().InsertGamesDidaticos_Logs(GDL);
        }
        SaveProgresso();
    }

    public void SaveLog(int _level, int _amountScore) {
        StopTimer();
        DBOGAMESDIDATICOS_LOGS GDL = new DBOGAMESDIDATICOS_LOGS()
        {
            dataAcesso = config.ReturnCurrentDate(),
            deviceID = SystemInfo.deviceUniqueIdentifier,
            fase = _level,
            idGameDidatico = config.levelManager.level.levelType == LevelType.completo ? config.levelManager.level.LevelDidatico.idGameDidatico : config.levelManager.level.idGameDidatico,
            idUsuario = config.playerID,
            online = 1,
            pontos = _amountScore,
            personagem = config.levelManager.level.useCharacterSelection ? config.levelManager.currentCharacter.name : "none",
            tempo = Mathf.FloorToInt(timeSpend)
        };

        if (config.isOnline) {
            Timing.RunCoroutine(config.NetHelper.SetGamesDidaticosLogs(GDL));
        } else {
            GDL.online = 0;
            config.openDB().InsertGamesDidaticos_Logs(GDL);
        }
        SaveProgresso();
    }

    public void SaveProgresso() {

        DBOGAMEDIDATICOS_PROGRESSO GDP = new DBOGAMEDIDATICOS_PROGRESSO(){
            highscore = this.scoreAmount > config.levelManager.level.highscore ? this.scoreAmount : config.levelManager.level.highscore,
            estrelas = starAmount > config.levelManager.level.starAmount ? starAmount : config.levelManager.level.starAmount,
            idGameDidatico = config.levelManager.level.levelType == LevelType.completo ? config.levelManager.level.LevelDidatico.idGameDidatico : config.levelManager.level.idGameDidatico,
            idUsuario = config.playerID,
            online = 1
        };

        if (config.isOnline) {
            Timing.RunCoroutine(config.NetHelper.SetDidaticosProgresso(GDP));
        } else {
            GDP.online = 0;
            config.openDB().InsertGamesDidatico_Progresso(GDP);
        }
    }

    public void AddScore(int _scoreAmount) {
        scoreAmount += _scoreAmount;
    }    

    public void SetPersonagem(string _charName) {
        characterSelected = _charName;
    }

    public void AddStars(int _starAmount) {
        starAmount += _starAmount;
    }

    public void SetStars(int _starAmount) {
        starAmount = _starAmount;
    }
}
