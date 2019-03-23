using MEC;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Network;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class networkHelper: OverridableMonoBehaviour {
    public bool isDoingOperation = false;
    public SyncTable currentSyncDB;
    public DBOUSUARIOS currentUser;
    public bool HasCredentials = false;
    private string token;
    public int userID;
    public GameConfig config;
    private bool operationFailed = false;
    private string tempToken;
    private StringFast stringfast = new StringFast();
    public static event Action<bool, string, string> LoginFailedEvent;
    public StartSceneManager startScene;

    public string Token {
        get {
            if(this.token == null) {
                return "noToken";
            }
            return token;
        }

        set {
            token = value;
        }
    }

    public void NetworkVeryfier(GameConfig configs) {
        configs.isVerifingNetwork = true;

        switch (Application.internetReachability){
            case NetworkReachability.NotReachable:
                configs.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                configs.isOnline = true;
                break;
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                configs.isOnline = true;
                break;
            default:
                configs.isOnline = false;
                break;
        }
        configs.isVerifingNetwork = false;
    }

    public string GetHtmlFromUri(string resource) {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse()) {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess) {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream())) {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs) {
                            html += ch;
                        }
                    }
                }
            }
        } catch {
            return "";
        }
        return html;
    }

    #region dboSync
    /*public void GetDBOSYNC(string dbosyncURI, int idCliente) {
        isDoingOperation = true;
        //Timing.RunCoroutine(DBOSYNCDOWN(dbosyncURI, idCliente));
    }*/

    public IEnumerator<float> DBOSYNCDOWN(string dbosyncURI, int idCliente, int _idCliente) {
        isDoingOperation = true;
        startScene.MessageStatus("Verificando atualizações");
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(DBOSYNCWEBREQUEST(dbosyncURI, idCliente, _idCliente)));
        
        isDoingOperation = false;
    }

    IEnumerator<float> DBOSYNCWEBREQUEST(string dbosyncURI, int idCliente, int _idClient) {
        //Debug.Log("Getting DBOSYNC");
        WWWForm form = new WWWForm();

        form.AddField("idCliente", idCliente);
        form.AddField("idGame", _idClient);
        form.AddField("gameKey", config.returnDecryptKey());

        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("getSincronizacao"), form);
            request.timeout = 20;
            request.redirectLimit = 1;
            //AsyncOperation async =  as AsyncOperation;
            Debug.Log("Enviado Request!");
            yield return Timing.WaitUntilDone(request.SendWebRequest()); 

        if (request.isNetworkError || request.isHttpError) {
            config.isOnline = false;
            config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
        } else {
                string response = request.downloadHandler.text;
                currentSyncDB = DecodeJson(response, idCliente);
            }
            request.Dispose();
            
    }


    public SyncTable DecodeJson(string JSONs, int idCliente) {
        
        var N = JSON.Parse(JSONs);
        SyncTable temp = new SyncTable() {
            //DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            escola = N["escola"],
            turma = N["turma"],
            usuarios = N["usuarios"],
            minigames = N["miniGames"],
            perguntasG = N["perguntasG"],
            respostasG = N["respostasG"],
            perguntasE = N["perguntasE"],
            respostasE = N["respostasG"],
            itensG = N["itensG"],
            itensE = N["itensE"],
            itens_categorias = N["itensCategorias"],
            ranking = N["ranking"],
            idDelPergunta = N["idDelPergunta"],
            idDelResposta = N["idDelResposta"],
            sincModePerguntas = N["sincModePerguntas"],
            sincModeItens = N["sincModeitens"],
            gamesdidaticos = N["gamesdidaticos"],
            perguntasGames = N["perguntasGames"],
            respostasGames = N["respostasGames"]
        };
        return temp;
    }

    #endregion

    #region retrieveToken

    public void UpdatePlayer(DBOUSUARIOS _user) {
        config.playerID = _user.idUsuario;
        config.nickname = _user.login;
        config.namefull = _user.nomeJogador;
    }

    public IEnumerator<float> GetTokenIE(string uri,string _idCliente, string login, string password, string deviceID, StartSceneManager sceneStart) {
        isDoingOperation = true;
        //Debug.Log("Getting Token");
        WWWForm form = new WWWForm();

        startScene.MessageStatus("Autenticando Credenciais");

        form.AddField("login", login);
        form.AddField("senha", password);
        form.AddField("deviceID", deviceID);
        form.AddField("idGame", config.gameID);
        form.AddField("gameKey", config.returnDecryptKey());
        form.AddField("versao", Application.version);
        form.AddField("DeviceModel", SystemInfo.deviceModel);
        form.AddField("OperatingSystem", SystemInfo.operatingSystem);
        form.AddField("ScreenWidth", Screen.width);
        form.AddField("ScreenHeight", Screen.height);
        form.AddField("SystemMemorySize", SystemInfo.systemMemorySize);
        form.AddField("idCliente", _idCliente);

        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("loginUsuarios"), form);
        request.timeout = 30;
        request.redirectLimit = 1;
        //yield return Timing.WaitUntilDone(request.SendWebRequest());
        //UnityWebRequestAsyncOperation async = request.SendWebRequest();
        yield return Timing.WaitUntilDone(request.SendWebRequest());
        
        string response = request.downloadHandler.text;
        Debug.Log(response);

        var result = JSON.Parse(response);

        if (result == null || request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            //Debug.Log(request.error);

            if(request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                sceneStart.OnlineAcessFailed();
            } else {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                sceneStart.OnlineAcessFailed();
            }
            
            //Timing.KillCoroutines();
            //Timing.KillCoroutines("LoginRoutine");
        } else {           
            Debug.Log("Token not null");           

            yield return Timing.WaitForSeconds(0.2f);
            

            this.Token = result["token"];
            //Debug.Log("Token [" + Token + "]");
            this.userID = result["idUsuario"].AsInt;
            this.config.playerID = result["idUsuario"].AsInt;

            if (!string.IsNullOrEmpty(Token)) {
                HasCredentials = false;
                //string error = result["erro"];
                //create Games_LOGS register;
                //TODO here
               // config.isOnline = false;
                //sceneStart.OnlineAcessFailed();
                //Timing.KillCoroutines();

                DBOUSUARIOS user = config.openDB().GetUser(login);                
                if (user != null) {
                    config.currentUser = user;
                    UpdatePlayer(user);


                }


                //
                //Debug.Log(error);
            } else {
                //sceneStart.OnlineAcessSucess(login, password);
                //HasCredentials = true;
                sceneStart.OnlineAcessFailed();

            }
        }
        isDoingOperation = false;
    }

    #endregion

    #region setMethods

    public void SetJogosLogM(DBOMINIGAMES_LOGS LOGs){
        Timing.RunCoroutine(SetJogosLog(LOGs, Token));
    }

    /*public void SetJogosLogM(List<JogosLogSerializeble> _logToSend, List<rankLogSeriazeble> _rankToSend, List<EstatisticaDidaticaSerialazeble> _statisticaToSend, List<GamesLogSerializable> _logsGames, List<int> userScoreUpdates, List<DBOINVENTARIO> userInventoryUdpdate) {
        Timing.RunCoroutine(QueueSend(_logToSend, _rankToSend, _statisticaToSend, _logsGames, userScoreUpdates, userInventoryUdpdate));
    }*/

    public IEnumerator<float> QueueSend(Action<bool,string,string> failed, List<JogosLogSerializeble> _logToSend, List<rankLogSeriazeble> _rankToSend, List<EstatisticaDidaticaSerialazeble> _statisticaToSend, List<GamesLogSerializable> _logsGames, List<int> userScoreUpdates, List<InventorySerializable> userInventoryUdpdate) {
        LoginFailedEvent = failed;
        startScene.MessageStatus("Sincronizando Jogo");
        int countTemp = _logToSend.Count;
       
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Logs do Jogo");
            do {
                _logToSend[0].token = Token;
                
                //yield return Timing.WaitUntilDone(Timing.RunCoroutine(SetJogosLog(_logToSend[0], true)));
                yield return Timing.WaitForSeconds(0.1f);
                //countTemp--;
            } while (_logToSend.Count >= 1 && config.isOnline);
        }

        
        countTemp = _rankToSend.Count;
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Ranking");
            do {
                _rankToSend[0].token = Token;                
                //yield return Timing.WaitUntilDone(Timing.RunCoroutine(setRanking(_rankToSend[0])));
                yield return Timing.WaitForSeconds(0.1f);
                
            } while (_rankToSend.Count >= 1 && config.isOnline);
        }

        countTemp = _statisticaToSend.Count;
        
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Estatísticas");
            do {
                //_statisticaToSend[0].token = token;
                
                //yield return Timing.WaitUntilDone(Timing.RunCoroutine(SetStatistics(_statisticaToSend[0])));
                yield return Timing.WaitForSeconds(0.1f);
                //countTemp--;
            } while (config.isOnline && _statisticaToSend.Count > 0);
        }

        countTemp = _logsGames.Count;
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Logs");
            do {
                
                //yield return Timing.WaitUntilDone(Timing.RunCoroutine(SendGamesLog(_logsGames[0])));
                yield return Timing.WaitForSeconds(0.1f);
            } while (_logsGames.Count >= 1 && config.isOnline);
        }

        
        countTemp = userScoreUpdates.Count;

        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Pontuações");
            do {
                DBOPONTUACAO scoreToUpdate = config.openDB().GetScore(userScoreUpdates[0]);
                //yield return Timing.WaitUntilDone(Timing.RunCoroutine(setPontuacao(userScoreUpdates[0], token)));
                yield return Timing.WaitForSeconds(0.1f);
            } while (userScoreUpdates.Count >= 1 && config.isOnline);
        }

        /*countTemp = userScoreUpdates.Count;
        if(countTemp >= 1 && config.isOnline) {
            for (int i = 0; i < countTemp; i++) {
                if (config.isOnline) {
                    
                    DBOPONTUACAO scoreToUpdate = config.openDB().GetScore(userScoreUpdates[0]);
                    yield return Timing.WaitForSeconds(0.1f);
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(setPontuacao(scoreToUpdate, token)));

                }
            }
        }*/

        
        countTemp = userInventoryUdpdate.Count;
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Inventário");
            do {
                DBOPONTUACAO scoreToUpdate = config.openDB().GetScore(userScoreUpdates[0]);
                //yield return Timing.WaitUntilDone(Timing.RunCoroutine(SetInventario(userInventoryUdpdate[0])));
                yield return Timing.WaitForSeconds(0.1f);
            } while (userInventoryUdpdate.Count >= 1 && config.isOnline);
        }

        /*countTemp = userInventoryUdpdate.Count;
        if(countTemp >= 1 && config.isOnline) {
            for (int i = 0; i < countTemp; i++) {
                if (config.isOnline) {
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(SetInventario(userInventoryUdpdate[0])));
                }
            }
        }*/


    }

    public void RunStatistics(List<DBOESTATISTICA_DIDATICA> _list) {
        Timing.RunCoroutine(SendStatisticsList(_list));
    }

    public IEnumerator<float> SendStatisticsList(List<DBOESTATISTICA_DIDATICA> _statisticaToSend) {
        yield return Timing.WaitForSeconds(0.1f);

        int countTemp = _statisticaToSend.Count;

        if (countTemp >= 1 && config.isOnline) {
            if (startScene != null) {
                startScene.MessageStatus("Sincronizando Estatísticas");
            }
            do {
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(StatisticSave(_statisticaToSend[0], _statisticaToSend)));
                yield return Timing.WaitForSeconds(0.1f);
            } while (config.isOnline && _statisticaToSend.Count > 0);
            int countTemp2 = _statisticaToSend.Count;
            if (config.isOnline == false) {
                for (int i = 0; i < countTemp2; i++) {
                    _statisticaToSend[i].online = 0;
                    config.openDB().InsertStatistic2(_statisticaToSend[i]);
                }
            }
        }
    }
    //TODO melhorar todo sistema de funcionamento de sincronização.
    public IEnumerator<float> SyncInfo(Action<bool, string, string> failed) {
        LoginFailedEvent = failed;
        startScene.MessageStatus("Sincronizando Game");

        DataService ds = config.openDB();

        List<DBOMINIGAMES_LOGS> _logToSend = ds.GetAllMinigamesLog();

        yield return Timing.WaitForSeconds(0.1f);

        int countTemp = _logToSend.Count;
        Debug.Log("SetJogosLog");
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Logs do Jogo");
            do {
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(SetJogosLog(_logToSend[0], _logToSend)));
                yield return Timing.WaitForSeconds(0.1f);
            } while (_logToSend.Count >= 1 && config.isOnline);

        }

        List<DBORANKING> _rankToSend = ds.GetAllOfflineRanks();
        yield return Timing.WaitForSeconds(0.1f);
        Debug.Log("setRanking");
        countTemp = _rankToSend.Count;
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Ranking");
            do {

                yield return Timing.WaitUntilDone(Timing.RunCoroutine(setRanking(_rankToSend[0], _rankToSend)));
                yield return Timing.WaitForSeconds(0.1f);

            } while (_rankToSend.Count >= 1 && config.isOnline);
        }


        List<DBOESTATISTICA_DIDATICA> _statisticaToSend = ds.GetAllStatisticDidatica();
        yield return Timing.WaitForSeconds(0.1f);

        countTemp = _statisticaToSend.Count;
        Debug.Log("StatisticSaveOffline");
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Estatísticas");
            do {
                if (_statisticaToSend[0] != null && _statisticaToSend != null) {
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(StatisticSave(_statisticaToSend[0], _statisticaToSend)));
                    yield return Timing.WaitForSeconds(0.1f);
                }
            } while (config.isOnline && _statisticaToSend.Count > 0);
        }

        List<DBOGAMES_LOGS> _logsGames = ds.GetAllGamesLOG();
        yield return Timing.WaitForSeconds(0.1f);
        Debug.Log("SendGamesLog");
        countTemp = _logsGames.Count;
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando MinigamesLog");
            do {

                yield return Timing.WaitUntilDone(Timing.RunCoroutine(SendGamesLog(_logsGames[0], _logsGames)));
                yield return Timing.WaitForSeconds(0.1f);
            } while (_logsGames.Count >= 1 && config.isOnline);
        }

        List<DBOPONTUACAO> userScoreUpdates = ds.GetallScoresOffline();
        yield return Timing.WaitForSeconds(0.1f);

        countTemp = userScoreUpdates.Count;
        Debug.Log("setPontuacao");
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Pontuações");
            do {
                //DBOPONTUACAO scoreToUpdate = config.openDB().GetScore(userScoreUpdates[0]);
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(setPontuacao(userScoreUpdates[0], userScoreUpdates)));
                yield return Timing.WaitForSeconds(0.1f);
            } while (userScoreUpdates.Count >= 1 && config.isOnline);
        }

        List<DBOINVENTARIO> userInventoryUdpdate = ds.GetAllInventory();
        yield return Timing.WaitForSeconds(0.1f);
        Debug.Log("SetInventario");
        countTemp = userInventoryUdpdate.Count;
        if (countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Inventário");
            do {
                //DBOPONTUACAO scoreToUpdate = config.openDB().GetScore(userScoreUpdates[0]);
                DBOITENS item = config.openDB().GetItemStore((userInventoryUdpdate[0].idItem));
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(SetInventario(userInventoryUdpdate[0], userInventoryUdpdate, item.valor)));
                yield return Timing.WaitForSeconds(0.1f);
            } while (userInventoryUdpdate.Count >= 1 && config.isOnline);
        }

        List<DBOGAMEDIDATICOS_PROGRESSO> offlineGDPList = ds.GetAllOfflineDidatico_Progresso();
        yield return Timing.WaitForOneFrame;
        Debug.Log("SetDidaticosProgresso");
        countTemp = offlineGDPList.Count;
        if(countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Didaticas Progresso!");
            do {
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(SetDidaticosProgresso(offlineGDPList[0], offlineGDPList)));
            } while (offlineGDPList.Count >= 1 && config.isOnline);
        }

        List<DBOGAMESDIDATICOS_LOGS> GDLList = ds.GetAllDidaticosLog();
        yield return Timing.WaitForOneFrame;

        countTemp = GDLList.Count;

        if(countTemp >= 1 && config.isOnline) {
            startScene.MessageStatus("Sincronizando Logs Didatica");
            do {
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(SetGamesDidaticosLogs(GDLList[0], GDLList)));
            } while (GDLList.Count >= 1 && config.isOnline);
        }

    }

    public IEnumerator<float> SetGamesDidaticosLogs(DBOGAMESDIDATICOS_LOGS _log, List<DBOGAMESDIDATICOS_LOGS> _list) {
        isDoingOperation = true;

        WWWForm form = new WWWForm();

        form.AddField("idUsuario", _log.idUsuario);
        form.AddField("idGameDidatico", _log.idGameDidatico);
        form.AddField("pontos", _log.pontos);
        form.AddField("personagem", _log.personagem);
        form.AddField("dataAcesso", _log.dataAcesso);
        form.AddField("tempo", _log.tempo);
        form.AddField("fase", _log.fase);
        form.AddField("deviceID", _log.deviceID);
        form.AddField("online", _log.online);
        form.AddField("token", Token);
        form.AddField("idUsuarioOnline", config.playerID);

        using(UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setGamesDidaticosLogs"), form)) {
            request.timeout = 20;
            request.redirectLimit = 2;
            yield return Timing.WaitUntilDone(request.SendWebRequest());

            if(request.isNetworkError || request.isHttpError) {
                _list.Remove(_log);
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                //config.openDB().RemoveGamesDidaticos_logs(_log);
            } else {
                // sucess
                _list.Remove(_log);
                config.openDB().RemoveGamesDidaticos_logs(_log);
            }
        }
    }
    public IEnumerator<float> SetGamesDidaticosLogs(DBOGAMESDIDATICOS_LOGS _log) {
        isDoingOperation = true;

        WWWForm form = new WWWForm();

        form.AddField("idUsuario", _log.idUsuario);
        form.AddField("idGameDidatico", _log.idGameDidatico);
        form.AddField("pontos", _log.pontos);
        form.AddField("personagem", _log.personagem);
        form.AddField("dataAcesso", _log.dataAcesso);
        form.AddField("tempo", _log.tempo);
        form.AddField("fase", _log.fase);
        form.AddField("deviceID", _log.deviceID);
        form.AddField("online", _log.online);
        form.AddField("token", Token);
        form.AddField("idUsuarioOnline", config.playerID);

        using (UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setGamesDidaticosLogs"), form)) {
            request.timeout = 20;
            request.redirectLimit = 2;
            yield return Timing.WaitUntilDone(request.SendWebRequest());

            if (request.isNetworkError || request.isHttpError) {
                _log.online = 0;
                config.openDB().InsertGamesDidaticos_Logs(_log);
                //config.openDB().RemoveGamesDidaticos_logs(_log);
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                config.logHandler.LogShow("Erro: Request Games Didaticos Log failed!", "#a01818");
            } else {
                // sucess
                config.openDB().RemoveGamesDidaticos_logs(_log);
                config.logHandler.LogShow("Sucess: Request Games Didaticos Log Sended!", "#15c458");
            }
        }
    }

    public IEnumerator<float> SetDidaticosProgresso(DBOGAMEDIDATICOS_PROGRESSO _GDP, List<DBOGAMEDIDATICOS_PROGRESSO> _list) {

        isDoingOperation = true;
        WWWForm form = new WWWForm();

        form.AddField("idGameDidatico", _GDP.idGameDidatico);
        form.AddField("idUsuario", _GDP.idUsuario);
        form.AddField("highscore", _GDP.highscore);
        form.AddField("estrelas", _GDP.estrelas);
        form.AddField("token", Token);
        form.AddField("idUsuarioOnline", config.playerID);

        using (UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setDidaticosProgresso"), form)) {

            request.timeout = 20;
            request.redirectLimit = 2;

            yield return Timing.WaitUntilDone(request.SendWebRequest());

            if (request.isNetworkError || request.isHttpError) {
                _list.Remove(_GDP);
                _GDP.online = 0;
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                config.openDB().InsertGamesDidatico_Progresso(_GDP);
                config.logHandler.LogShow("SetDidaticosProgresso Failed!", "#a01818");
            } else {
                config.logHandler.LogShow("SetDidaticosProgresso Sucess!", "#15c458");
                _list.Remove(_GDP);
                _GDP.online = 1;
                config.openDB().InsertGamesDidatico_Progresso(_GDP);
            }
            request.Dispose();
        }

        isDoingOperation = false;
    }

    public IEnumerator<float> SetDidaticosProgresso(DBOGAMEDIDATICOS_PROGRESSO _GDP) {

        isDoingOperation = true;
        WWWForm form = new WWWForm();

        form.AddField("idGameDidatico", _GDP.idGameDidatico);
        form.AddField("idUsuario", _GDP.idUsuario);
        form.AddField("highscore", _GDP.highscore);
        form.AddField("estrelas", _GDP.estrelas);
        form.AddField("token", Token);
        form.AddField("idUsuarioOnline", config.playerID);

        using (UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setDidaticosProgresso"), form)) {

            request.timeout = 20;
            request.redirectLimit = 2;

            yield return Timing.WaitUntilDone(request.SendWebRequest());

            if (request.isNetworkError || request.isHttpError) {                
                _GDP.online = 0;
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                config.openDB().InsertGamesDidatico_Progresso(_GDP);
                config.logHandler.LogShow("SetDidaticosProgresso Failed!", "#a01818");
            } else {
                _GDP.online = 1;
                config.openDB().InsertGamesDidatico_Progresso(_GDP);
                config.logHandler.LogShow("SetDidaticosProgresso Sucess!", "#15c458");
            }
            request.Dispose();
        }

        isDoingOperation = false;
    }

    public IEnumerator<float> SetJogosLog(DBOMINIGAMES_LOGS logTemp, string TokenT) {

        isDoingOperation = true;
        WWWForm form = new WWWForm();
        logTemp.online = 1;
        form.AddField("token", Token);
        form.AddField("idUsuario", logTemp.idUsuario);
        form.AddField("idMinigame", logTemp.idMinigame);
        form.AddField("pontosLudica", logTemp.pontosLudica);
        form.AddField("pontosPedagogica", logTemp.pontosPedagogica);
        form.AddField("pontosInteragindo", logTemp.pontosInteragindo);
        form.AddField("personagem", logTemp.personagem);
        form.AddField("dataAcesso", logTemp.dataAcesso);
        form.AddField("tempoLudica", logTemp.tempoLudica);
        form.AddField("tempoDidatica", logTemp.tempoDidatica);
        form.AddField("faseLudica", logTemp.faseLudica);
        form.AddField("deviceID", logTemp.deviceID);
        form.AddField("online", 1);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);

        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setJogosLogs"), form);
        request.redirectLimit = 1;
        request.timeout = 20;

        //yield return Timing.WaitUntilDone(request.SendWebRequest());
        //UnityWebRequestAsyncOperation async = request.SendWebRequest();
        yield return Timing.WaitUntilDone(request.SendWebRequest());
       

        string response = request.downloadHandler.text;
        Debug.Log("SetJogosLog" + response);
        //var result = JSON.Parse(response);
        if (request.isNetworkError || request.isHttpError) {
            //config.isOnline = false;
            config.logHandler.LogShow("Game is Offline!", "#ba0e0e");
            logTemp.online = 0;
            config.openDB().InsertJogosLog(logTemp);
        } else {
            
        }

        isDoingOperation = false;
    }

    public IEnumerator<float> SetJogosLog(DBOMINIGAMES_LOGS jogosTemp, List<DBOMINIGAMES_LOGS> _list) {

        isDoingOperation = true;
        WWWForm form = new WWWForm();
        form.AddField("token", Token);
        form.AddField("idUsuario", jogosTemp.idUsuario);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);
        form.AddField("idMinigame", jogosTemp.idMinigame);
        form.AddField("pontosLudica", jogosTemp.pontosLudica);
        form.AddField("pontosPedagogica", jogosTemp.pontosPedagogica);
        form.AddField("pontosInteragindo", jogosTemp.pontosInteragindo);
        form.AddField("personagem", jogosTemp.personagem);
        form.AddField("dataAcesso", jogosTemp.dataAcesso);
        form.AddField("tempoLudica", jogosTemp.tempoLudica);
        form.AddField("tempoDidatica", jogosTemp.tempoDidatica);
        form.AddField("faseLudica", jogosTemp.faseLudica);
        form.AddField("deviceID", jogosTemp.deviceID);
        form.AddField("online", 0);


        using (UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setJogosLogs"), form)) {
            request.timeout = 10;
            request.redirectLimit = 2;

            //yield return Timing.WaitUntilDone(request.SendWebRequest());
            //UnityWebRequestAsyncOperation async = request.SendWebRequest();
            yield return Timing.WaitUntilDone(request.SendWebRequest());
            

            string response = request.downloadHandler.text;
            Debug.Log("SetJogosLog" + response);
            //var result = JSON.Parse(response);
            if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {

                if (request.isNetworkError || request.isHttpError) {
                    config.isOnline = false;
                    config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                }

                //LoginFailedEvent(true, config.currentUser.login, config.currentUser.senha);
            } else {
                _list.RemoveAt(0);
                config.openDB().DeleteMinigamesLog(jogosTemp);
            }

            isDoingOperation = false;
            request.Dispose();
        }
    }    

    IEnumerator<float> StatisticSave(EstatisticaDidaticaSerialazeble _listStatistc, DBOESTATISTICA_DIDATICA _statistic, string tokens) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();
        _statistic.online = 0;
        
        form.AddField("token", Token);
        form.AddField("idUsuario", _statistic.idUsuario);
        form.AddField("dataInsert", _statistic.dataInsert);
        form.AddField("idHabilidade", _statistic.idGameDidatico);
        form.AddField("idGameDidatico", _statistic.idGameDidatico);
        form.AddField("idDificuldade", _statistic.idDificuldade);
        form.AddField("idLivro", _statistic.idLivro);
        form.AddField("acertou", _statistic.acertou);
        form.AddField("online", 0);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);


        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setEstatisticaDidatica"), form);
        request.redirectLimit = 2;
        request.timeout = 20;

        //yield return Timing.WaitUntilDone(request.SendWebRequest());
        //UnityWebRequestAsyncOperation async = request.SendWebRequest();
        yield return Timing.WaitUntilDone(request.SendWebRequest());
       

        string response = request.downloadHandler.text;
        Debug.Log(response);
        //var result = JSON.Parse(response);

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
            }
        } else {
            _listStatistc.estatisticas.RemoveAt(0);

        }

        isDoingOperation = false;
    }

    IEnumerator<float> StatisticSaveOffline(DBOESTATISTICA_DIDATICA _statistic, List<DBOESTATISTICA_DIDATICA> _list) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();

        form.AddField("token", Token);
        form.AddField("idUsuario", _statistic.idUsuario);
        if(_statistic.dataInsert != null) {
            form.AddField("dataInsert", _statistic.dataInsert);
        } else {
            form.AddField("dataInsert", config.ReturnCurrentDate());
        }        
        form.AddField("idHabilidade", _statistic.idHabilidade);
        form.AddField("idGameDidatico", _statistic.idGameDidatico);
        form.AddField("idDificuldade", _statistic.idDificuldade);
        form.AddField("idLivro", _statistic.idLivro);
        form.AddField("acertou", _statistic.acertou);
        form.AddField("online", 0);
        form.AddField("idUsuarioOnline", config.playerID);

        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setEstatisticaDidatica"), form);
        request.timeout = 20;
        request.redirectLimit = 1;

        //yield return Timing.WaitUntilDone(request.SendWebRequest());
        //UnityWebRequestAsyncOperation async = request.SendWebRequest();
       yield return Timing.WaitUntilDone(request.SendWebRequest());
        

        string response = request.downloadHandler.text;
        Debug.Log(response);
        //var result = JSON.Parse(response);

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            Debug.Log("StatisticSave" + response);
            config.isOnline = false;
            config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
            _list.RemoveAt(0);
        } else {
            Debug.Log("StatisticSave" + response);
            _list.RemoveAt(0);
            config.openDB().DeleteEstatistica(_statistic);
        }

        isDoingOperation = false;
    }
    
    //
    IEnumerator<float> MassSendStatistics(List<DBOESTATISTICA_DIDATICA> ListOfStatistics)
    {
        isDoingOperation = true;
        WWWForm form = new WWWForm();
        form.AddField("token", Token);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);
        var jsonData = JsonHandler.ToJson(ListOfStatistics);
        var HashData = JsonHandler.StringToHash(jsonData);
        form.AddField("Hash", HashData);
        form.AddField("Data", jsonData);
        
        //Criando UnityWebRequest de envio.
        using (UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setEstatisticaDidatica"), form)) {
            
            request.timeout = 20;
            request.redirectLimit = 1;

            yield return Timing.WaitUntilDone(request.SendWebRequest());
            
            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                //_statistic.online = 0;
                //config.openDB().InsertStatistic2(_statistic);
            } else {
                //config.openDB().DeleteEstatistica(_statistic);
                //_list.Remove(_statistic);
            }
            
        }
    }

   

    IEnumerator<float> StatisticSave(DBOESTATISTICA_DIDATICA _statistic,List<DBOESTATISTICA_DIDATICA> _list) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();
        _statistic.online = 1;

        form.AddField("token", Token);
        form.AddField("idUsuario", _statistic.idUsuario);
        form.AddField("dataInsert", _statistic.dataInsert);
        form.AddField("idHabilidade", _statistic.idHabilidade);
        form.AddField("idGameDidatico", _statistic.idGameDidatico);
        form.AddField("idDificuldade", _statistic.idDificuldade);
        form.AddField("idLivro", _statistic.idLivro);
        form.AddField("acertou", _statistic.acertou);
        form.AddField("online", 1);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);

        using (UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setEstatisticaDidatica"), form)) {
            request.timeout = 20;
            request.redirectLimit = 1;

            yield return Timing.WaitUntilDone(request.SendWebRequest());
       
            string response = request.downloadHandler.text;
            var result = JSON.Parse(response);

            if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                _statistic.online = 0;
                config.openDB().InsertStatistic2(_statistic);
            } else {
                config.openDB().DeleteEstatistica(_statistic);
                _list.Remove(_statistic);
            }
        }

        isDoingOperation = false;
    }

    IEnumerator<float> SendGamesLog(DBOGAMES_LOGS _log, List<DBOGAMES_LOGS> _list) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();
        form.AddField("token", Token);
        form.AddField("idUsuario", _log.idUsuario);
        form.AddField("idGame", config.gameID);
        form.AddField("dataAcesso", _log.dataAcesso);
        form.AddField("online", 0);
        form.AddField("versao", _log.versao);
        form.AddField("deviceID", _log.deviceID);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);

        //TODO GamesLOG API LINK
        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setGamesLogs"), form);
        request.timeout = 20;
        request.redirectLimit = 1;

        yield return Timing.WaitUntilDone(request.SendWebRequest());
      
        string response = request.downloadHandler.text;
        Debug.Log("SendGamesLog" + response);
        var result = JSON.Parse(response);

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
            }
        } else {
            _list.RemoveAt(0);
            config.openDB().DeleteGamesLog(_log);
        }

        isDoingOperation = false;
    }

    public void RunSetRanking(DBORANKING ranking, string _token) {
        Timing.RunCoroutine(setRanking(ranking, _token));
    }

    IEnumerator<float> setRanking(DBORANKING ranking, string _token) {
        isDoingOperation = true;
        
        WWWForm form = new WWWForm();
        form.AddField("token", Token);
        form.AddField("idMinigame", ranking.idMinigame);
        form.AddField("idUsuario", ranking.idUsuario);
        form.AddField("highscore", ranking.highscore);
        form.AddField("idGame", config.gameID);
        form.AddField("idCliente", config.clientID);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);
        form.AddField("estrelas", ranking.estrelas);

        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setRanking"), form);
        request.timeout = 20;
        request.redirectLimit = 1;

        yield return Timing.WaitUntilDone(request.SendWebRequest());
        //UnityWebRequestAsyncOperation async = request.SendWebRequest();
       

        string response = request.downloadHandler.text;
        Debug.Log("setRanking" + response);
        //var result = JSON.Parse(response);

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            //Debug.Log(request.error);


            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                ranking.online = 0;
                config.openDB().InsertRanking(ranking);

            } else {
                //config.isOnline = false;
                ranking.online = 1;
                config.openDB().InsertRanking(ranking);
            }

            
        } else {
            Debug.Log("RANKING SAVED");
        }

        isDoingOperation = false;
    }

    IEnumerator<float> setRanking(DBORANKING rankLog, List<DBORANKING> _list) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();
        form.AddField("token", Token);
        form.AddField("idMinigame", rankLog.idMinigame);
        form.AddField("idUsuario", rankLog.idUsuario);
        form.AddField("highscore", rankLog.highscore);
        form.AddField("idCliente", config.clientID);
        form.AddField("idGame", config.gameID);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);
        form.AddField("estrelas", rankLog.estrelas);

        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setRanking"), form);
        request.timeout = 20;
        request.redirectLimit = 1;

        yield return Timing.WaitUntilDone(request.SendWebRequest());
       

        string response = request.downloadHandler.text;
        Debug.Log("setRanking " + response);

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            //Debug.Log(request.error);
            //config.isOnline = false;
            //LoginFailedEvent(true, config.currentUser.login, config.currentUser.senha);

            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
            }

        } else {
           
            //Debug.Log(response);
            Debug.Log("RANKING SAVED");
            _list.RemoveAt(0);
            rankLog.online = 1;
            config.openDB().InsertRanking(rankLog);
        }

        isDoingOperation = false;
    }
    

    public void RunsetPontuacao(DBOPONTUACAO score, string token) {
        Timing.RunCoroutine(setPontuacao(score));
    }

    IEnumerator<float> setPontuacao(DBOPONTUACAO score, List<DBOPONTUACAO> _list) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();
        form.AddField("token", Token);     
        form.AddField("idUsuario", score.idUsuario);
        form.AddField("deviceBrops", score.BropsDevice);
        form.AddField("devicePoints", score.PontuacaoTotalDevice);
        form.AddField("deviceId", SystemInfo.deviceUniqueIdentifier);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);

        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setPontuacao"), form);

        yield return Timing.WaitUntilDone(request.SendWebRequest());
        
        string response = request.downloadHandler.text;
        Debug.Log(response);
        //var result = JSON.Parse(response);

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
        } else {
            if (response.Contains("success")) {
                config.BropsDeviceAmount = 0;
                config.TotalPointsDevice = 0;
                Debug.Log("SetPontuacao: " + response);
                _list.RemoveAt(0);
                score.online = 1;
                score.PontuacaoTotalDevice = 0;
                score.BropsDevice = 0;
                config.openDB().UpdateToOnlineScore(score);
            }            
        }

        isDoingOperation = false;
    }

    IEnumerator<float> setPontuacao(DBOPONTUACAO score) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();

        Debug.Log("Score [Device Brops: " + score.BropsDevice + "]");

        form.AddField("token", Token);
        form.AddField("idUsuario", score.idUsuario);
        form.AddField("deviceBrops", score.BropsDevice);
        form.AddField("devicePoints", score.PontuacaoTotalDevice);
        form.AddField("deviceId", SystemInfo.deviceUniqueIdentifier);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);

        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setPontuacao"), form);

        yield return Timing.WaitUntilDone(request.SendWebRequest());
       
        string response = request.downloadHandler.text;
        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                score.online = 0;
                config.openDB().InsertOrReplateScore(score);
            }
        } else {
            config.BropsDeviceAmount = 0;
            config.TotalPointsDevice = 0;
            Debug.Log("Set Score: " + response);
            Debug.Log("Device Brops: " + config.BropsDeviceAmount + "| Brops: " + config.BropsAmount);
            Debug.Log("Device Points: " + config.TotalPointsDevice + "| Points: " + config.TotalPoints);
        }

        isDoingOperation = false;
    }

    public void SetInventory(DBOINVENTARIO _inv, int valu) {
        Timing.RunCoroutine(SetInventario(_inv,valu));
        config.BropsDeviceAmount -= valu;
        //config.TotalPointsDevice -= valu;
        Debug.Log("Before Send: [Brops Device: " + config.BropsDeviceAmount);
        Timing.RunCoroutine(setPontuacao(new DBOPONTUACAO() {
            brops = config.BropsAmount,
            pontuacaoTotal = config.TotalPoints,
            idUsuario = config.currentUser.idUsuario,
            dataUpdate = config.ReturnCurrentDate(),
            BropsDevice = config.BropsDeviceAmount,
            PontuacaoTotalDevice = config.TotalPointsDevice            
        }));
    }

    IEnumerator<float> SetInventario(DBOINVENTARIO _inv, int value) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();
        
        form.AddField("idUsuario", _inv.idUsuario);
        form.AddField("idItem", _inv.idItem);
        form.AddField("quantidade", _inv.quantidade);
        form.AddField("ativo", _inv.ativo);
        form.AddField("deviceID", SystemInfo.deviceUniqueIdentifier);
        form.AddField("valor", value);
        form.AddField("token", Token);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);

        //TODO adicionar link da API SetInventario.
        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setInventario"), form);

        Debug.Log("Salvando inventário");
        yield return Timing.WaitUntilDone(request.SendWebRequest());        

        string response = request.downloadHandler.text;

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            config.isOnline = false;
            _inv.online = 0;
            config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
            config.openDB().UpdateOrReplateInventory(_inv);
        } else {
            _inv.online = 1;
            _inv.deviceQuantity = 0;
            config.openDB().UpdateOrReplateInventory(_inv);
            Debug.Log("SetInventario - " + response);
        }

        isDoingOperation = false;
    }

    IEnumerator<float> SetInventario(DBOINVENTARIO inv, List<DBOINVENTARIO> _list, int itemValue) {
        isDoingOperation = true;
        WWWForm form = new WWWForm();
        
        form.AddField("idUsuario", inv.idUsuario);
        form.AddField("idItem", inv.idItem);
        form.AddField("quantidade", inv.deviceQuantity);
        form.AddField("ativo", inv.ativo);
        form.AddField("deviceID", SystemInfo.deviceUniqueIdentifier);
        form.AddField("valor", itemValue);
        form.AddField("token", Token);
        form.AddField("idUsuarioOnline", config.currentUser.idUsuario);

        //TODO adicionar link da API SetInventario.
        UnityWebRequest request = UnityWebRequest.Post(config.GetURL("setInventario"), form);

        yield return Timing.WaitUntilDone(request.SendWebRequest());

        string response = request.downloadHandler.text;
        if (request.isNetworkError || request.isHttpError || response.Contains("response")) {
            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
            }
        } else {
            //config.usersIDInventoryUpdate.Remove(inv);
            _list.RemoveAt(0);
            inv.online = 1;
            inv.deviceQuantity = 0;
            config.openDB().UpdateItem(inv);
            Debug.Log(response);
        }
        isDoingOperation = false;
    }
    #endregion

    #region SyncBeforeLogin
    public IEnumerator<float> GettingDBOSBeforeLogin(int idCliente, DataService db, string URI, int idGame) {

        isDoingOperation = true;

        List<string> tables = new List<string>();

        //config.sincModeItens = currentSyncDB.sincModeItens;
        //config.sincModePerguntas = currentSyncDB.sincModePerguntas;
        config.sincModeItens = 1;
        config.sincModePerguntas = 1;

        //yield return Timing.WaitForSeconds(.5f);

        DBOSINCRONIZACAO dboI = db.GetSync(idCliente);
        yield return Timing.WaitForSeconds(.5f);
        DBOSINCRONIZACAO dboG = db.GetSync(1);
        yield return Timing.WaitForSeconds(.5f);
        bool hasTables = false;

        if (config.sincModePerguntas == 1 || config.sincModePerguntas == 3) {
            if (LocalIsOlder(dboG.perguntas, currentSyncDB.perguntasG)) {
                tables.Add("perguntasG");
                hasTables = true;
            }

            if (LocalIsOlder(dboG.respostas, currentSyncDB.respostasG)) {
                tables.Add("respostasG");
                hasTables = true;
            }
        }

        if (config.sincModePerguntas == 1 || config.sincModePerguntas == 2) {
            if (LocalIsOlder(dboI.perguntas, currentSyncDB.perguntasE)) {
                tables.Add("perguntasE");
                hasTables = true;
            }

            if (LocalIsOlder(dboI.respostas, currentSyncDB.respostasE)) {
                tables.Add("respostasE");
                hasTables = true;
            }
        }

        if (config.sincModeItens == 1 || config.sincModeItens == 2) {
            if (LocalIsOlder(dboI.itens, currentSyncDB.itensE)) {
                tables.Add("itensE");
                hasTables = true;
            }
        }

        if (config.sincModeItens == 1 || config.sincModeItens == 3) {
            if (LocalIsOlder(dboG.itens, currentSyncDB.itensG)) {
                tables.Add("itensG");
                hasTables = true;
            }

            if (LocalIsOlder(dboG.itens_categorias, currentSyncDB.itens_categorias)) {
                tables.Add("itens_categorias");
                hasTables = true;
            }
        }

        if (LocalIsOlder(dboG.minigames, currentSyncDB.minigames)) {
            tables.Add("minigames");
            hasTables = true;
        }

        if (LocalIsOlder(dboG.gamesdidaticos, currentSyncDB.gamesdidaticos)) {
            tables.Add("gamesdidaticos");
            hasTables = true;
        }
        
        if (LocalIsOlder(dboG.perguntasGames, currentSyncDB.perguntasGames)) {
            tables.Add("perguntasGames");
            hasTables = true;
        }
        
        if (LocalIsOlder(dboG.respostasGames, currentSyncDB.respostasGames)) {
            tables.Add("respostasGames");
            hasTables = true;
        }

        if (hasTables == false) {
            tables.Add("");
        }

        WWWForm form = new WWWForm();

        form.AddField("idGame", config.gameID);
        form.AddField("idCliente", idCliente);
        form.AddField("gameKey", config.returnDecryptKey());

        for (int i = 0; i < tables.Count; i++) {
            stringfast.Clear();
            stringfast.Append("tables[").Append(i).Append("]");
            string temp = stringfast.ToString();
            form.AddField(temp, tables[i]);
        }
        form.AddField("minigames", dboG.minigames);
        form.AddField("perguntasG", dboG.perguntas);
        form.AddField("respostasG", dboG.respostas);
        form.AddField("perguntasE", dboI.perguntas);
        form.AddField("respostasE", dboI.respostas);
        form.AddField("itensG", dboG.itens);
        form.AddField("itens_categorias", dboG.itens_categorias);
        form.AddField("itensE", dboI.itens);
        if(dboG.gamesdidaticos == null) {
            dboG.gamesdidaticos = "2017-01-01 01:01:01.001";
        }
        form.AddField("gamesDidaticos", dboG.gamesdidaticos);
        form.AddField("perguntasGames", dboG.perguntasGames);
        form.AddField("respostasGames", dboG.respostasGames);

        UnityWebRequest requestData = UnityWebRequest.Post(config.GetURL("getTabsSincronizacao"), form);
        requestData.timeout = 10;
        requestData.redirectLimit = 2;

        //AsyncOperation async = requestData.SendWebRequest();
        yield return Timing.WaitUntilDone(requestData.SendWebRequest());
        //UnityWebRequestAsyncOperation async = requestData.SendWebRequest();
        /*while (!requestData.isDone) {
            yield return Timing.WaitForOneFrame;
        }*/

        if (requestData.isNetworkError || requestData.isHttpError) {
            Debug.Log(requestData.responseCode + ": " + requestData.error);
            config.isOnline = false;
            config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
        } else {
            string response = requestData.downloadHandler.text;
            Debug.Log(requestData.downloadHandler.text);
            var result = JSON.Parse(response);

            var array1 = result["tabelas"];
            var perguntas = array1["perguntas"];
            var respostas = array1["respostas"];
            var itensJson = array1["itens"];
            var minigamesJson = array1["miniGames"];
            var itens_categoriasJson = array1["itensCategorias"];
            var gamesdidaticos = array1["gamesDidaticos"];
            var perguntaGames = array1["perguntasGames"];
            var respostaGames = array1["respostasGames"];

            int size;

            DataService data = config.openDB();

            size = itensJson.Count;

            if (size >= 1) {
                List<DBOITENS> _itensDB = new List<DBOITENS>();
                startScene.MessageStatus("Atualizando Itens");
                for (int i = 0; i < size; i++) {
                    _itensDB.Add(new DBOITENS() {
                        idItem = itensJson[i]["idItem"].AsInt,
                        idCliente = itensJson[i]["idCliente"].AsInt,
                        idCategoriaItem = itensJson[i]["idCategoriaItem"].AsInt,
                        infoItem = itensJson[i]["infoItem"],
                        nomeItem = itensJson[i]["nomeItem"],
                        ativo = itensJson[i]["ativo"].AsBool ? 1 : 0,
                        valor = itensJson[i]["valor"].AsInt,
                        downloaded = 0
                    });
                }               

                for (int i = 0; i < size; i++) {
                    startScene.MessageStatus("Atualizando Itens " + (i + 1) + "/" + size + "");

                    if (_itensDB[i].ativo == 1 && config.isOnline == true) {

                        bool downloaded = false;
                        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadImageItem(MyResult => downloaded = MyResult, _itensDB[i].idItem)));
                        _itensDB[i].downloaded = downloaded ? 1 : 0;
                        //Debug.Log(_itensDB[i].ToString());
                    }
                }

                data.AddItens(_itensDB);
                yield return Timing.WaitForOneFrame;

                if (config.isOnline == true) {
                     if (currentSyncDB.sincModeItens == 1 || currentSyncDB.sincModeItens == 3) {
                         dboG.itens = currentSyncDB.itensG;
                     }

                     if (currentSyncDB.sincModeItens == 1 || currentSyncDB.sincModeItens == 2) {
                         dboI.itens = currentSyncDB.itensE;
                     }
                 }
             }

            //Baixar imagem de itens previamente não baixado por motivos de erro de conexão ou afins.
            List<DBOITENS> itensPreviousNotDownloaded = data.NotDownloadedItens();

            size = itensPreviousNotDownloaded.Count;
            if (size >= 1 && config.isOnline) {
                for (int i = 0; i < size; i++) {
                    startScene.MessageStatus("Download de Itens Restantes " + (i + 1) + "/" + size + "");
                    bool downloaded = false;
                    if (!File.Exists(config.fullPatchItemIcon + itensPreviousNotDownloaded[i].idItem + ".png")) {
                        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadImageItem(MyResult => downloaded = MyResult, itensPreviousNotDownloaded[i].idItem)));
                    } else {
                        downloaded = true;
                    }
                    itensPreviousNotDownloaded[i].downloaded = downloaded ? 1 : 0;
                }

                data.AddItens(itensPreviousNotDownloaded);
                yield return Timing.WaitForOneFrame;
                //Termino do download de imagens de itens previamente não baixados.
            }
                size = itens_categoriasJson.Count;

                if (size >= 1) {

                    List<DBOITENS_CATEGORIAS> itensCategoryDB = new List<DBOITENS_CATEGORIAS>();
                    startScene.MessageStatus("Atualizando Categorias");
                    for (int i = 0; i < size; i++) {
                        itensCategoryDB.Add(new DBOITENS_CATEGORIAS() {
                            idCategoriaItem = itens_categoriasJson[i]["idCategoriaItem"].AsInt,
                            nomeCategoria = itens_categoriasJson[i]["nomeCategoria"],
                            infoCategoria = itens_categoriasJson[i]["infoCategoria"],
                            ativo = itens_categoriasJson[i]["ativo"].AsBool ? 1 : 0,
                            colecionaveis = itens_categoriasJson[i]["colecionaveis"].AsBool ? 1 : 0
                        });

                    }

                    data.AddItensCategory(itensCategoryDB);
                    yield return Timing.WaitForOneFrame;
                    dboG.itens_categorias = currentSyncDB.itens_categorias;

                }

                size = perguntas.Count;

                if (size >= 1) {

                    List<DBOPERGUNTAS> perguntasDB = new List<DBOPERGUNTAS>();
                    startScene.MessageStatus("Atualizando Perguntas");
                    for (int i = 0; i < size; i++) {

                        perguntasDB.Add(new DBOPERGUNTAS() {
                            idCliente = perguntas[i]["idCliente"].AsInt,
                            idPergunta = perguntas[i]["idPergunta"].AsInt,
                            idHabilidade = perguntas[i]["idHabilidade"].AsInt,
                            idLivro = perguntas[i]["idLivro"].AsInt,
                            idDificuldade = perguntas[i]["idDificuldade"].AsInt,
                            textoPergunta = perguntas[i]["textoPergunta"],
                            ativo = perguntas[i]["ativo"].AsBool ? 1 : 0,
                            audio = perguntas[i]["audio"].AsBool ? 1 : 0,
                            downloaded = 0
                        });

                    }



                    for (int i = 0; i < size; i++) {
                        if (perguntasDB[i].ativo == 1 && config.isOnline && perguntasDB[i].audio == 1) {
                            startScene.MessageStatus("Atualizando Perguntas " + i + "/" + size);
                            stringfast.Clear();
                            stringfast.Append(config.fullAudioClipDestinationQuestions).Append(perguntasDB[i].idPergunta).Append(".ogg");
                            bool downloaded = false;
                            yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadQuestionSound(MyResult => downloaded = MyResult, perguntasDB[i].idPergunta)));
                            perguntasDB[i].downloaded = downloaded ? 1 : 0;
                        } 
                    }


                    data.AddAllPerguntas(perguntasDB);
                    yield return Timing.WaitForOneFrame;

                    if (currentSyncDB.sincModePerguntas == 1 || currentSyncDB.sincModePerguntas == 3) {
                        dboG.perguntas = currentSyncDB.perguntasG;
                    }
                    if (currentSyncDB.sincModePerguntas == 1 || currentSyncDB.sincModePerguntas == 2) {
                        dboI.perguntas = currentSyncDB.perguntasE;
                    }

                }

            // Rotina de download de perguntas previamente não baixadas ou por erro ou por falta de conexão com a internet.
            List<DBOPERGUNTAS> notDownloadedPerguntas = data.PerguntasToDownload();
            size = notDownloadedPerguntas.Count;
            for (int i = 0; i < size; i++) {
                stringfast.Clear();
                stringfast.Append(config.fullAudioClipDestinationQuestions).Append(notDownloadedPerguntas[i].idPergunta).Append(".ogg");
                bool downloaded = false;
                if (!File.Exists(stringfast.ToString())) {
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadQuestionSound(MyResult => downloaded = MyResult, notDownloadedPerguntas[i].idPergunta)));
                } else {
                    downloaded = true;
                }

                notDownloadedPerguntas[i].downloaded = downloaded ? 1 : 0;
                data.AddPerguntaOrReplace(notDownloadedPerguntas[i]);
                yield return Timing.WaitForOneFrame;

            }

            //rotina de download das respostas.

                size = respostas.Count;

                if (size >= 1) {

                    List<DBORESPOSTAS> respostasDB = new List<DBORESPOSTAS>();
                    startScene.MessageStatus("Atualizando Respostas");
                    for (int i = 0; i < size; i++) {
                        respostasDB.Add(new DBORESPOSTAS() {
                            idResposta = respostas[i]["idResposta"].AsInt,
                            idPergunta = respostas[i]["idPergunta"].AsInt,
                            textoResposta = respostas[i]["textoResposta"],
                            correta = respostas[i]["correta"].AsBool ? 1 : 0,
                            audio = perguntas[i]["audio"].AsBool ? 1 : 0,
                            downloaded = 0
                        });
                    }

                    for (int i = 0; i < size; i++) {
                        if (respostasDB[i].ativo == 1 && config.isOnline && respostasDB[i].audio == 1) {
                            startScene.MessageStatus("Atualizando Perguntas " + i + "/" + size);
                            bool downloaded = false;
                            yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadAnswerID(MyResult => downloaded = MyResult, respostasDB[i].idResposta)));
                            respostasDB[i].downloaded = downloaded ? 1 : 0;
                        }
                    }

                    
                    data.AddAllRespostas(respostasDB);
                    yield return Timing.WaitForOneFrame;

                    if (currentSyncDB.sincModePerguntas == 1 || currentSyncDB.sincModePerguntas == 3) {
                        dboG.respostas = currentSyncDB.respostasG;
                    }

                    if (currentSyncDB.sincModePerguntas == 1 || currentSyncDB.sincModePerguntas == 2) {
                        dboI.respostas = currentSyncDB.respostasE;
                    }
                }

            // rotina de respostas previamente não baixadas por questões de falha na conexão ou problemas no meio.
            List<DBORESPOSTAS> notDownloadedRespotas = data.RespostaNotDownloaded();
            size = notDownloadedRespotas.Count;

            for (int i = 0; i < size; i++) {
                stringfast.Clear();
                stringfast.Append(config.fullAudioClipDestinationAnswers).Append(notDownloadedRespotas[i].idResposta).Append(".ogg");
                bool downloaded = false;
                if (!File.Exists(stringfast.ToString())) {
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadAnswerID(MyResult => downloaded = MyResult, notDownloadedRespotas[i].idResposta)));
                } else {
                    downloaded = true;
                }
                notDownloadedRespotas[i].downloaded = downloaded ? 1 : 0;
                data.AddRespostaOrReplace(notDownloadedRespotas[i]);
            }

            size = minigamesJson.Count;

            if (size >= 1) {

                //Criar List de Minigames
                //List<MinigameStruct> minigamesDB = new List<MinigameStruct>();
                List<DBOMINIGAMES> minigamesDB = new List<DBOMINIGAMES>();
                startScene.MessageStatus("Atualizando informações do Jogo");

                for (int i = 0; i < size; i++) {
                    minigamesDB.Add(new DBOMINIGAMES() {
                        ativo = minigamesJson[i]["ativo"].AsBool ? 1 : 0,
                        idMinigames = minigamesJson[i]["idMiniGame"].AsInt,
                        idLivro = minigamesJson[i]["idLivro"].AsInt,
                        nomeMinigame = minigamesJson[i]["nomeMiniGame"],
                        infoMinigame = minigamesJson[i]["infoMiniGame"]
                    });
                }

                data.UpdateMinigames(minigamesDB);

                yield return Timing.WaitForOneFrame;

                if (currentSyncDB.sincModePerguntas == 1 || currentSyncDB.sincModePerguntas == 3) {
                    dboG.minigames = currentSyncDB.minigames;
                }

                if (currentSyncDB.sincModePerguntas == 1 || currentSyncDB.sincModePerguntas == 2) {
                    dboI.minigames = currentSyncDB.minigames;
                }


            }

            size = gamesdidaticos.Count;

            if(size >= 1) {

                List<DBOGAMES_DIDATICOS> listGamesDidaticos = new List<DBOGAMES_DIDATICOS>();

                startScene.MessageStatus("Atualizando Games Didaticos");

                for (int i = 0; i < size; i++) {
                    var GD = gamesdidaticos[i];
                    listGamesDidaticos.Add(new DBOGAMES_DIDATICOS() {
                        idGameDidatico = GD["idGameDidatico"].AsInt,
                        idLivro = GD["idLivro"].AsInt,
                        idHabilidade = GD["idHabilidade"].AsInt,
                        nomeGameDidatico = GD["nomeGameDidatico"],
                        descGameDidatico = GD["descGameDidatico"],
                        disciplina = GD["disciplina"],
                        ativo = GD["ativo"].AsInt
                    });
                }

                for (int i = 0; i < size; i++) {
                    data.InsertGamesDidatico(listGamesDidaticos[i]);
                    yield return Timing.WaitForOneFrame;
                }

                dboG.gamesdidaticos = currentSyncDB.gamesdidaticos;

            }
            
            size = perguntaGames.Count;
            List<DBOPERGUNTAS_GAMES> listPerguntasGames = new List<DBOPERGUNTAS_GAMES>();
            startScene.MessageStatus("Atualizando Games Perguntas");
            for (int i = 0; i < size; i++){
                var pergunta = perguntaGames[i];
                listPerguntasGames.Add(new DBOPERGUNTAS_GAMES(){
                    idPergunta = pergunta["idPergunta"].AsInt,
                    idHabilidade = pergunta["idHabilidade"].AsInt,
                    idAnoLetivo = pergunta["idAnoLetivo"].AsInt,
                    idDificuldade = pergunta["idDificuldade"].AsInt,
                    textoPergunta = pergunta["textoPergunta"],
                    layout = pergunta["layout"].AsInt,
                    imagem = pergunta["imagem"].AsInt,
                    audio = pergunta["audio"].AsInt,
                    ativo = pergunta["ativo"].AsBool ? 1 : 0
                });
            }
            
            for (int i = 0; i < size; i++){
                data.InsertPerguntaGames(listPerguntasGames[i]);
                yield return Timing.WaitForOneFrame;
            }
            
            //TODO sistemas de verificação de perguntas com audio/imagem(layouts diferentes).
            
            dboG.perguntasGames = currentSyncDB.perguntasGames;
            
            size = respostaGames.Count;
            List<DBORESPOSTA_GAMES> listrespostaGames = new List<DBORESPOSTA_GAMES>();
            startScene.MessageStatus("Atualizando Games Perguntas");
            for (int i = 0; i < size; i++){
                var res = respostaGames[i];
                listrespostaGames.Add(new DBORESPOSTA_GAMES(){
                    IdResposta = res["idResposta"].AsInt,
                    IdPergunta = res["idPergunta"].AsInt,
                    TextoResposta = res["textoResposta"],
                    correta = res["correta"].AsBool ? 1:0
                });
            }
            
            for (int i = 0; i < size; i++){
                data.InsertRespostaGames(listrespostaGames[i]);
                yield return Timing.WaitForOneFrame;
            }
            //TODO sistema de verificação de respostas com audio/imagens (Layouts Diferentes).
            
            dboG.respostasGames = currentSyncDB.respostasGames;
            
          
            
            if (currentSyncDB.sincModePerguntas == 1 || currentSyncDB.sincModePerguntas == 3) {
                db.UpdateSync(dboG);
            }

            if (currentSyncDB.sincModePerguntas == 1 || currentSyncDB.sincModePerguntas == 2) {
                db.UpdateSync(dboI);
            }



            isDoingOperation = false;
            requestData.Dispose();

        }            
     }


    #endregion

    #region afterLoginSync
    public IEnumerator<float> SyncAfterLogin(int _idCliente, DataService db, string URI, string tokenTemp, StartSceneManager _startScene) {
        isDoingOperation = true;
        startScene.MessageStatus("Sincronizando Informações");
        WWWForm form = new WWWForm();
        DBOSINCRONIZACAO dbo = db.GetSync(_idCliente);
        bool hasTables = false;
        //form.AddField("idUsuario", )
        form.AddField("idCliente", _idCliente);
        form.AddField("token", Token);
        //Debug.Log("afterLoginSync - " + token);
        form.AddField("idUsuario", config.playerID);
        form.AddField("usuarios", dbo.usuarios);
        form.AddField("idGame", config.gameID);
        form.AddField("deviceId", SystemInfo.deviceUniqueIdentifier);

        List<string> tables = new List<string>();

        yield return Timing.WaitForSeconds(0.1f);

        if (LocalIsOlder(dbo.escola, currentSyncDB.escola)) {
            tables.Add("escola");
            hasTables = true;
        }

        if (LocalIsOlder(dbo.turma, currentSyncDB.turma)) {
            tables.Add("turma");
            hasTables = true;
        }

        if (LocalIsOlder(dbo.usuarios, currentSyncDB.usuarios)) {
            tables.Add("usuarios");
            hasTables = true;
        }

        if (LocalIsOlder(dbo.ranking, currentSyncDB.ranking)) {
            tables.Add("ranking");
            hasTables = true;
        }

        if (hasTables == false) {
            tables.Add("");
        }

        for (int i = 0; i < tables.Count; i++) {
            stringfast.Clear();
            stringfast.Append("tables[").Append(i).Append("]");
            string temp = stringfast.ToString();
            form.AddField(temp, tables[i]);
        }

        UnityWebRequest request = UnityWebRequest.Post(URI, form);
        request.timeout = 20;
        request.redirectLimit = 2;
        yield return Timing.WaitUntilDone(request.SendWebRequest());

        string response = request.downloadHandler.text;
        Debug.Log("SyncAfterLogin" + response);
        var result = JSON.Parse(response);

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            //Debug.Log(request.error);
            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                _startScene.OnlineAcessFailed();
            } else {
                _startScene.OnlineAcessFailed();
            }
        } else {

            var tablesJSON = result["tabelas"];
            //yield return Timing.WaitForSeconds(0.2f);
            var score = tablesJSON["pontuacao"];
            var escolas = tablesJSON["escola"];
            var turmas = tablesJSON["turma"];
            var usuarios = tablesJSON["usuarios"];
            var ranking = tablesJSON["ranking"];
            var ranking2 = tablesJSON["rankingusuario"];
            var inventory = tablesJSON["inventario"];
            var gamesdidaticos = tablesJSON["didaticos_progresso"];

            Debug.Log(score.ToString());

            //yield return Timing.WaitForSeconds(0.5f);

            startScene.MessageStatus("Sincronizando Pontuação");
            DBOPONTUACAO newSCORE = new DBOPONTUACAO() {
                pontuacaoTotal = score[0]["pontuacaoTotal"].AsInt,
                brops = score[0]["brops"].AsInt,
                dataUpdate = score[0]["dataUpdate"],
                idUsuario = config.playerID,
                BropsDevice = 0,
                PontuacaoTotalDevice = 0
            };

            config.currentScore = newSCORE;

            config.TotalPoints = newSCORE.pontuacaoTotal;
            config.BropsAmount = newSCORE.brops;
            config.BropsDeviceAmount = 0;
            config.TotalPointsDevice = 0;

            newSCORE.online = 1;
            db.UpdateScore(newSCORE);

            int size = escolas.Count;

            if (size >= 1) {

                List<DBOESCOLA> escolasDB = new List<DBOESCOLA>();
                startScene.MessageStatus("Atualizando Lista de Escolas");
                for (int i = 0; i < size; i++) {
                    escolasDB.Add(new DBOESCOLA() {
                        idEscola = escolas[i]["idEscola"].AsInt,
                        nomeEscola = escolas[i]["nomeEscola"],
                        dataUpdate = escolas[i]["dataUpdate"],
                        idCliente = _idCliente
                    });
                }

                db.ClearEscolas();

                db.AddAllEscolas(escolasDB);

                dbo.escola = currentSyncDB.escola;

            }

            size = turmas.Count;

            if (size >= 1) {
                List<DBOTURMA> turmasDB = new List<DBOTURMA>();
                startScene.MessageStatus("Atualizando Lista de Turmas");
                for (int i = 0; i < size; i++) {
                    turmasDB.Add(new DBOTURMA() {
                        idTurma = turmas[i]["idTurma"].AsInt,
                        idAnoLetivo = turmas[i]["idAnoLetivo"].AsInt,
                        idEscola = turmas[i]["idEscola"].AsInt,
                        descTurma = turmas[i]["descTurma"],
                        dataUpdate = turmas[i]["dataUpdate"],
                    });
                }

                db.ClearTurmas();
                db.AddAllTurmas(turmasDB);

                dbo.turma = currentSyncDB.turma;
            }

            yield return Timing.WaitForSeconds(0.2f);

            size = usuarios.Count;

            if (size >= 1) {

                Debug.Log(usuarios);
                startScene.MessageStatus("Atualizando Lista de Usuários");
                for (int i = 0; i < size; i++) {
                    DBOUSUARIOS userTemp = new DBOUSUARIOS() {
                        idUsuario = usuarios[i]["idUsuario"].AsInt,
                        idCliente = _idCliente,
                        tipoUsuario = (usuarios[i]["tipoUsuario"] == null) ? 0 : usuarios[i]["tipoUsuario"].AsInt,
                        idTurma = usuarios[i]["idTurma"].AsInt,
                        nomeJogador = usuarios[i]["nomeJogador"],
                        login = usuarios[i]["login"],
                        senha = usuarios[i]["senha"],
                        dataUpdate = usuarios[i]["dataUpdate"],
                        ativo = (usuarios[i]["ativo"].AsBool) ? 1 : 0
                    };

                    db.InsertUser(userTemp);

                }

                dbo.usuarios = currentSyncDB.usuarios;

            }

            yield return Timing.WaitForSeconds(0.2f);

            size = ranking.Count;

            if (size >= 1) {

                List<DBORANKING> rankingDB = new List<DBORANKING>();
                startScene.MessageStatus("Atualizando Ranking dos Minigames");
                for (int i = 0; i < size; i++) {
                    rankingDB.Add(new DBORANKING() {
                        idMinigame = ranking[i]["idMiniGame"].AsInt,
                        idUsuario = ranking[i]["idUsuario"].AsInt,
                        highscore = ranking[i]["highScore"].AsInt,
                        posicao = -1,
                        online = 1,
                    });
                }

                db.ClearRanking();
                db.InsertRanking(rankingDB);

                dbo.ranking = currentSyncDB.ranking;

            }

            yield return Timing.WaitForSeconds(0.2f);

            size = ranking2.Count;

            //Own Rank Loop.
            if (size >= 1) {


                List<DBORANKING> rankingDB = new List<DBORANKING>();
                startScene.MessageStatus("Atualizando Ranking dos Minigames");

                for (int i = 0; i < size; i++) {
                    DBORANKING temp = new DBORANKING() {
                        idMinigame = ranking2[i]["idMiniGame"].AsInt,
                        idUsuario = userID,
                        highscore = ranking2[i]["highscore"].AsInt,
                        posicao = ranking2[i]["posicao"].AsInt,
                        estrelas = ranking2[i]["estrelas"].AsInt,
                        online = 1
                    };

                    rankingDB.Add(temp);
                }

                db.UpdateRankings(rankingDB);

            }


            yield return Timing.WaitForSeconds(0.2f);

            size = inventory.Count;

            if (size >= 1) {
                startScene.MessageStatus("Atualizando Inventário");
                List<DBOINVENTARIO> inventoryDB = new List<DBOINVENTARIO>();
                for (int i = 0; i < size; i++) {
                    inventoryDB.Add(new DBOINVENTARIO() {
                        idItem = inventory[i]["idItem"],
                        idUsuario = userID,
                        quantidade = inventory[i]["quantidade"],
                        ativo = inventory[i]["ativo"].AsBool ? 1 : 0,
                        dataUpdate = inventory[i]["dataUpdate"],
                        online = 1,
                        deviceQuantity = 0
                    });
                }

                db.SetInventario(inventoryDB);
            }

            size = gamesdidaticos.Count;

            if(size >= 1) {
                startScene.MessageStatus("Atualizando Progresso Games Didaticos");
                for (int i = 0; i < size; i++) {
                    if (gamesdidaticos[i] != null) {
                        var dp = gamesdidaticos[i];
                        config.openDB().InsertGamesDidatico_Progresso(new DBOGAMEDIDATICOS_PROGRESSO()
                        {
                            idGameDidatico = dp["idGameDidatico"].AsInt,
                            highscore = dp["highscore"].AsInt,
                            idUsuario = config.playerID,
                            estrelas = dp["estrelas"].AsInt,
                            online = 1,
                        });
                    }
                    yield return Timing.WaitForOneFrame;
                }
            }

            db.UpdateSync(dbo);

            _startScene.OnlineAcessSucess();
            request.Dispose();
        }
    }

    public IEnumerator<float> SyncAfterLogin2(int _idCliente, DataService db, string URI, string tokenTemp, StartSceneManager _startScene) {
        isDoingOperation = true;
        startScene.MessageStatus("Sincronizando Informações");
        WWWForm form = new WWWForm();
        DBOSINCRONIZACAO dbo = db.GetSync(_idCliente);
        bool hasTables = false;
        //form.AddField("idUsuario", )
        form.AddField("idCliente", _idCliente);
        form.AddField("token", Token);
        Debug.Log("afterLoginSync - " + Token);
        form.AddField("idUsuario", config.playerID);
        form.AddField("usuarios", dbo.usuarios);
        form.AddField("idGame", config.gameID);
        form.AddField("deviceId", SystemInfo.deviceUniqueIdentifier);

        List<string> tables = new List<string>();

        yield return Timing.WaitForSeconds(0.1f);

        if (LocalIsOlder(dbo.escola, currentSyncDB.escola)) {
            tables.Add("escola");
            hasTables = true;
        }

        if (LocalIsOlder(dbo.turma, currentSyncDB.turma)) {
            tables.Add("turma");
            hasTables = true;
        }

        if (LocalIsOlder(dbo.usuarios, currentSyncDB.usuarios)) {
            tables.Add("usuarios");
            hasTables = true;
        }

        if (LocalIsOlder(dbo.ranking, currentSyncDB.ranking)) {
            tables.Add("ranking");
            hasTables = true;
        }

        if (hasTables == false) {
            tables.Add("");
        }

        for (int i = 0; i < tables.Count; i++) {
            stringfast.Clear();
            stringfast.Append("tables[").Append(i).Append("]");
            string temp = stringfast.ToString();
            form.AddField(temp, tables[i]);
        }

        UnityWebRequest request = UnityWebRequest.Post(URI, form);

        yield return Timing.WaitUntilDone(request.SendWebRequest());

        string response = request.downloadHandler.text;
        Debug.Log("SyncAfterLogin" + response);
        var result = JSON.Parse(response);

        if (request.isNetworkError || request.isHttpError || response.Contains("erro")) {
            //Debug.Log(request.error);
            if (request.isNetworkError || request.isHttpError) {
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                _startScene.OnlineAcessFailed();
            } else {
                _startScene.OnlineAcessFailed();
            }
        } else {

            var tablesJSON = result["tabelas"];
            //yield return Timing.WaitForSeconds(0.2f);
            var score = tablesJSON["pontuacao"];
            var escolas = tablesJSON["escola"];
            var turmas = tablesJSON["turma"];
            var usuarios = tablesJSON["usuarios"];
            var ranking = tablesJSON["ranking"];
            var ranking2 = tablesJSON["rankingusuario"];
            var inventory = tablesJSON["inventario"];
            var gamesdidaticos = tablesJSON["didaticos_progresso"];
            Debug.Log(score.ToString());

            //yield return Timing.WaitForSeconds(0.5f);

            startScene.MessageStatus("Sincronizando Pontuação");
            DBOPONTUACAO newSCORE = new DBOPONTUACAO() {
                pontuacaoTotal = score[0]["pontuacaoTotal"].AsInt,
                brops = score[0]["brops"].AsInt,
                dataUpdate = score[0]["dataUpdate"],
                idUsuario = config.playerID,
                BropsDevice = 0,
                PontuacaoTotalDevice = 0
            };

            config.currentScore = newSCORE;

            config.TotalPoints = newSCORE.pontuacaoTotal;
            config.BropsAmount = newSCORE.brops;
            config.BropsDeviceAmount = 0;
            config.TotalPointsDevice = 0;

            newSCORE.online = 1;
            db.UpdateScore(newSCORE);

            int size = escolas.Count;

            if (size >= 1) {

                List<DBOESCOLA> escolasDB = new List<DBOESCOLA>();
                startScene.MessageStatus("Atualizando Lista de Escolas");
                for (int i = 0; i < size; i++) {
                    escolasDB.Add(new DBOESCOLA() {
                        idEscola = escolas[i]["idEscola"].AsInt,
                        nomeEscola = escolas[i]["nomeEscola"],
                        dataUpdate = escolas[i]["dataUpdate"],
                        idCliente = _idCliente
                    });
                }

                db.ClearEscolas();

                db.AddAllEscolas(escolasDB);

                dbo.escola = currentSyncDB.escola;

            }

            size = turmas.Count;

            if (size >= 1) {
                List<DBOTURMA> turmasDB = new List<DBOTURMA>();
                startScene.MessageStatus("Atualizando Lista de Turmas");
                for (int i = 0; i < size; i++) {
                    turmasDB.Add(new DBOTURMA() {
                        idTurma = turmas[i]["idTurma"].AsInt,
                        idAnoLetivo = turmas[i]["idAnoLetivo"].AsInt,
                        idEscola = turmas[i]["idEscola"].AsInt,
                        descTurma = turmas[i]["descTurma"],
                        dataUpdate = turmas[i]["dataUpdate"],
                    });
                }

                db.ClearTurmas();
                db.AddAllTurmas(turmasDB);

                dbo.turma = currentSyncDB.turma;
            }

            yield return Timing.WaitForSeconds(0.2f);

            size = usuarios.Count;

            if (size >= 1) {

                Debug.Log(usuarios);
                startScene.MessageStatus("Atualizando Lista de Usuarios");
                for (int i = 0; i < size; i++) {
                    DBOUSUARIOS userTemp = new DBOUSUARIOS() {
                        idUsuario = usuarios[i]["idUsuario"].AsInt,
                        idCliente = _idCliente,
                        tipoUsuario = (usuarios[i]["tipoUsuario"] == null) ? 0 : usuarios[i]["tipoUsuario"].AsInt,
                        idTurma = usuarios[i]["idTurma"].AsInt,
                        nomeJogador = usuarios[i]["nomeJogador"],
                        login = usuarios[i]["login"],
                        senha = usuarios[i]["senha"],
                        dataUpdate = usuarios[i]["dataUpdate"],
                        ativo = (usuarios[i]["ativo"].AsBool) ? 1 : 0
                    };

                    db.InsertUser(userTemp);

                }

                dbo.usuarios = currentSyncDB.usuarios;

            }

            yield return Timing.WaitForSeconds(0.2f);

            size = ranking.Count;

            if (size >= 1) {

                List<DBORANKING> rankingDB = new List<DBORANKING>();
                startScene.MessageStatus("Atualizando Ranking dos Minigames");
                for (int i = 0; i < size; i++) {
                    rankingDB.Add(new DBORANKING() {
                        idMinigame = ranking[i]["idMiniGame"].AsInt,
                        idUsuario = ranking[i]["idUsuario"].AsInt,
                        highscore = ranking[i]["highScore"].AsInt,
                        posicao = -1,
                        online = 1,
                    });
                }

                db.ClearRanking();
                db.InsertRanking(rankingDB);

                dbo.ranking = currentSyncDB.ranking;

            }

            yield return Timing.WaitForSeconds(0.2f);

            size = ranking2.Count;

            //Own Rank Loop.
            if (size >= 1) {


                List<DBORANKING> rankingDB = new List<DBORANKING>();
                startScene.MessageStatus("Atualizando Ranking dos Minigames");

                for (int i = 0; i < size; i++) {
                    DBORANKING temp = new DBORANKING() {
                        idMinigame = ranking2[i]["idMiniGame"].AsInt,
                        idUsuario = userID,
                        highscore = ranking2[i]["highscore"].AsInt,
                        posicao = ranking2[i]["posicao"].AsInt,
                        estrelas = ranking2[i]["estrelas"].AsInt,
                        online = 1
                    };

                    rankingDB.Add(temp);
                }

                db.UpdateRankings(rankingDB);

            }


            yield return Timing.WaitForSeconds(0.2f);

            size = inventory.Count;

            if (size >= 1) {
                startScene.MessageStatus("Atualizando Inventário");
                List<DBOINVENTARIO> inventoryDB = new List<DBOINVENTARIO>();
                for (int i = 0; i < size; i++) {
                    inventoryDB.Add(new DBOINVENTARIO() {
                        idItem = inventory[i]["idItem"],
                        idUsuario = userID,
                        quantidade = inventory[i]["quantidade"],
                        ativo = inventory[i]["ativo"].AsBool ? 1 : 0,
                        dataUpdate = inventory[i]["dataUpdate"],
                        online = 1,
                        deviceQuantity = 0
                    });
                }

                db.SetInventario(inventoryDB);
            }

            size = gamesdidaticos.Count;

            if (size >= 1) {
                startScene.MessageStatus("Atualizando Progresso Games Didaticos");
                for (int i = 0; i < size; i++) {
                    var dp = gamesdidaticos[i];
                    config.openDB().InsertGamesDidatico_Progresso(new DBOGAMEDIDATICOS_PROGRESSO()
                    {
                        idGameDidatico = dp["idGameDidatico"].AsInt,
                        highscore = dp["highscore"].AsInt,
                        idUsuario = config.playerID,
                        estrelas = dp["estrelas"].AsInt,
                        online = 1,
                    });
                    yield return Timing.WaitForOneFrame;
                }
            }

            db.UpdateSync(dbo);

            _startScene.OnlineAcessSucess2();

        }

    }

    #endregion

    #region DelPergunta e Resposta

    public IEnumerator<float> DelsSync(int idCliente) {
        isDoingOperation = true;
        idCliente = 1;
        DataService db = config.openDB();
        startScene.MessageStatus("Verificando Remoção de Perguntas");
        DBOSINCRONIZACAO dbo = db.GetSync(idCliente);

        WWWForm form = new WWWForm();

        form.AddField("idCliente", idCliente);
        form.AddField("idPerguntaDel", dbo.idDelPergunta);
        form.AddField("idRespostaDel", dbo.idDelResposta);
        form.AddField("gameKey", config.returnDecryptKey());

        using (UnityWebRequest request = UnityWebRequest.Post(config.GetURL("GetPRRemovidas"), form)) {
            request.timeout = 10;
            request.redirectLimit = 2;
            Debug.Log("Esperando request");

            yield return Timing.WaitUntilDone(request.SendWebRequest());
            //UnityWebRequestAsyncOperation async = request.SendWebRequest();
            /*while (!request.isDone) {
                yield return Timing.WaitForOneFrame;
            }*/

            if (request.isNetworkError || request.isHttpError) {
                Debug.Log(request.responseCode + ": " + request.error);
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
            } else {
                Debug.Log(request.downloadHandler.text);

                var jsonDecode = JSON.Parse(request.downloadHandler.text);
                var decode = jsonDecode["PR_Removidas"];
                var delPergunta = decode["perguntasDel"];
                var delRespsota = decode["respostasDel"];

                int size = delPergunta.Count;

                for (int i = 0; i < size; i++) {
                    db.DeleteQuestion(delPergunta[i]["idPergunta"].AsInt);
                }

                size = delRespsota.Count;

                for (int i = 0; i < size; i++) {
                    db.DelAnswer(delRespsota[i]["idResposta"].AsInt);
                }

                dbo.idDelPergunta = currentSyncDB.idDelPergunta;
                dbo.idDelResposta = currentSyncDB.idDelResposta;

                db.UpdateSync(dbo);
            }

            request.Dispose();
        }
        
        isDoingOperation = false;
    }

    #endregion


    public DateTime DateFromString(string dateString) {
        //Debug.Log(dateString);
        if(dateString == null) {
            dateString = "2017-01-01 13:31:23.937";
        }
        return DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
    }

    public bool LocalIsOlder(string onlineDate, string offlineDate) {
        if (DateTime.Compare(DateFromString(onlineDate), DateFromString(offlineDate)) < 0) {
            return true;
        } else {
            return false;
        }
    }

    public string Md5Sum(string strToEncrypt) {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++) {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public void RegisterLogLogin(DBOUSUARIOS _user) {
        if (_user != null) {
            DBOGAMES_LOGS temp = new DBOGAMES_LOGS {
                dataAcesso = config.ReturnCurrentDate(),
                deviceID = SystemInfo.deviceUniqueIdentifier,
                idGame = config.gameID,
                idUsuario = _user.idUsuario,
                online = 0,
                versao = Application.version,
                ID = 0,
            };

            config.openDB().InserGamesLOG(temp);
        }
    }

    public void LoadImageItemIcon(int itemID) {
        //Timing.RunCoroutine(LoadImageItem(itemID));
    }


    IEnumerator<float> LoadImageItem(Action<bool> MyResult, int idItem) {
        stringfast.Clear();
        stringfast.Append("https://api.eduqbrinq.com.br/midias/itens/").Append(idItem).Append(".png");
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(stringfast.ToString())){
            www.redirectLimit = 2;
            www.timeout = 30;
            yield return Timing.WaitUntilDone(www.SendWebRequest());
           // UnityWebRequestAsyncOperation async = www.SendWebRequest();
            while (!www.isDone) {
                yield return Timing.WaitForOneFrame;
            }

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.responseCode);
                config.isOnline = false;
                config.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
                MyResult(false);
                www.Dispose();
            } else {
                var bytes = ((DownloadHandlerTexture)www.downloadHandler).texture.EncodeToPNG();
                File.WriteAllBytes(config.fullPatchItemIcon + idItem + ".png", bytes);
                MyResult(true);
                www.Dispose();
            }
        }
       
    }

    IEnumerator<float> LoadQuestionSound(Action<bool> MyResult, int QuestionID)
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://api.eduqbrinq.com.br/midias/perguntas/" + QuestionID + ".ogg", AudioType.OGGVORBIS);
        www.timeout = 20;
        www.redirectLimit = 2;
        yield return Timing.WaitUntilDone(www.SendWebRequest());
        //UnityWebRequestAsyncOperation async = www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            //config.isOnline = false;
            //config.QuestionIDtoDownload.Add(QuestionID);
            MyResult(false);
        }
        else {
            var bytes = ((DownloadHandlerAudioClip)www.downloadHandler).data;
            if (bytes.Length > 2000) {
                Debug.Log(bytes.Length.ToString());
                File.WriteAllBytes(config.fullAudioClipDestinationQuestions + QuestionID + ".ogg", bytes);
                MyResult(true);
            } else {
                if (!config.QuestionIDtoDownload.Contains(QuestionID)) {
                    config.QuestionIDtoDownload.Add(QuestionID);
                    MyResult(false);
                }
            }
        }

        www.Dispose();
    }

    IEnumerator<float> LoadAnswerID(Action<bool> MyResult, int AnswerID)
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://api.eduqbrinq.com.br/midias/respostas/" + AnswerID + ".ogg", AudioType.OGGVORBIS);
        www.timeout = 20;
        www.redirectLimit = 2;
        yield return Timing.WaitUntilDone(www.SendWebRequest());
       

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            //config.isOnline = false;
            //config.AnswersIDtoDoownload.Add(AnswerID);
            MyResult(false);
        } else
        {
            var bytes = ((DownloadHandlerAudioClip)www.downloadHandler).data;
            if (bytes.Length > 2000)
            {
                Debug.Log(bytes.Length.ToString());
                File.WriteAllBytes(config.fullAudioClipDestinationAnswers + AnswerID + ".ogg", bytes);
                MyResult(true);
            } else
            {
                MyResult(false);
            }
        }
        www.Dispose();
    }


}
