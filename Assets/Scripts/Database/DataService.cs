using UnityEngine;
using System.Linq;
using System.Globalization;
#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif
using System.Collections.Generic;
using System.IO;
using System;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN

#endif

public class DataService {

    public bool removeLinq = false;

	private SQLiteConnection _connection;
    public string _databaseName;

	public DataService(string DatabaseName){
        _databaseName = DatabaseName;
        // check if file exists in Application.persistentDataPath
        Debug.Log("Streaming Location - " + Application.streamingAssetsPath);
        Debug.Log("Persistent Location - " + Application.persistentDataPath);
        Debug.Log("Data Location - " + Application.dataPath);

        var filepath = Application.persistentDataPath + "/" + DatabaseName;
		if (File.Exists(filepath)){
            Debug.Log("filepath - " + filepath);
            _connection = new SQLiteConnection(filepath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);  
            

            /*if(_connection._open == false){
                Debug.Log("problem opening");
                File.Delete(filepath);
                CreateDBOnPath(DatabaseName,filepath);
            }*/
            //Debug.Log("<color=#ffffff>Final PATH:</color> <color=#33bfea>" + filepath + "</color>"); 
        } else
        {
            Debug.Log("not found");
            CreateDBOnPath(DatabaseName,filepath);
        }

        

    }

    public DataService(string DatabaseName, string _pass) {
        _databaseName = DatabaseName;
        // check if file exists in Application.persistentDataPath
        Debug.Log("Streaming Location - " + Application.streamingAssetsPath);
        Debug.Log("Persistent Location - " + Application.persistentDataPath);
        Debug.Log("Data Location - " + Application.dataPath);

        var filepath = Application.persistentDataPath + "/" + DatabaseName;
        if (File.Exists(filepath)) {
            Debug.Log("filepath - " + filepath);
            //_connection = new SQLiteConnection(_pass,filepath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

            /*if(_connection._open == false){
                Debug.Log("problem opening");
                File.Delete(filepath);
                CreateDBOnPath(DatabaseName,filepath);
            }*/
            //Debug.Log("<color=#ffffff>Final PATH:</color> <color=#33bfea>" + filepath + "</color>"); 
        } else {
            Debug.Log("not found");
            CreateDBOnPath(DatabaseName, filepath);
        }



    }

    public void SetPassword(string _pass) {
        //_connection.SetDbKey(_pass);
        _connection.Close();
    }

    public void RemakeConnection() {
        //_databaseName = _databaseName;
        // check if file exists in Application.persistentDataPath
        Debug.Log("Streaming Location - " + Application.streamingAssetsPath);
        Debug.Log("Persistent Location - " + Application.persistentDataPath);
        Debug.Log("Data Location - " + Application.dataPath);

        var filepath = Application.persistentDataPath + "/" + _databaseName;
        if (File.Exists(filepath)) {
            Debug.Log("filepath - " + filepath);
            _connection = new SQLiteConnection(filepath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        } else {
            Debug.Log("not found");
            CreateDBOnPath(_databaseName, filepath);
        }

    }

    public void ReplaceDB() {
        var persistentPath = string.Format("{0}/{1}", Application.persistentDataPath, _databaseName);
        var streamingPath = string.Format("{0}/{1}", Application.streamingAssetsPath, _databaseName);
        _connection.Dispose();
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
            var bytesTOLoad = File.ReadAllBytes(streamingPath);
            File.WriteAllBytes(persistentPath, bytesTOLoad);
        }
    }

    public void UpdateOverrideWithStreaming() {
        _connection.Dispose();
        var persistentPath = string.Format("{0}/{1}", Application.persistentDataPath, _databaseName);
        var streamingPath = string.Format("{0}/{1}", Application.streamingAssetsPath, _databaseName);
        _connection.Dispose();
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
            var bytesTOLoad = File.ReadAllBytes(streamingPath);
            File.WriteAllBytes(persistentPath, bytesTOLoad);  
        } else if(Application.platform == RuntimePlatform.Android) {
            var loadDb = new WWW(streamingPath);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(persistentPath, loadDb.bytes);
        } else if(Application.platform == RuntimePlatform.OSXPlayer && Application.platform == RuntimePlatform.IPhonePlayer) {
            File.Copy(streamingPath, persistentPath);
        }

        RemakeConnection();
    }

    public void UpdateStreamingAssetDB(string DatabaseName) {
        var persistentPath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
        var streamingPath = string.Format("{0}/{1}", Application.streamingAssetsPath, DatabaseName);

        if (File.Exists(persistentPath)) {
            File.Copy(persistentPath, streamingPath, true);
            //File.Delete(persistentPath);
        }
    }

    private void CreateDBOnPath(string DatabaseName, string filepath){

        Debug.Log("creating db on persistent Data path");

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_STANDALONE
        var _loaddb = string.Format("{0}/{1}",Application.streamingAssetsPath,DatabaseName);
        Debug.Log(_loaddb);
        var filepathTo = string.Format("{0}/{1}",Application.persistentDataPath,DatabaseName);
        var bytesTOLoad = File.ReadAllBytes(_loaddb);
        Debug.Log(filepathTo);
        //File.Copy(_loaddb,filepathTo,true);
        File.WriteAllBytes(filepathTo,bytesTOLoad);
        filepath = filepathTo;
#elif UNITY_STANDALONE_OSX
        var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;
        File.Copy(loadDb, filepath);
#elif UNITY_IOS
           var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
           // then save to Application.persistentDataPath
           File.Copy(loadDb, filepath);
#elif UNITY_ANDROID
         var loadDb = new WWW(Application.streamingAssetsPath + "/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#endif

        _connection = new SQLiteConnection(filepath,SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        //Debug.Log("<color=#ffffff>Final PATH:</color> <color=#33bfea>" + filepath + "</color>"); 
        
    }

    #region DBOUSUARIO

    /// <summary>
    /// Gets the user.
    /// </summary>
    /// <returns>The user.</returns>
    /// <param name="login">Login.</param>
    public DBOUSUARIOS GetUser(string loginUser){
		    return _connection.Table<DBOUSUARIOS>().FirstOrDefault(x => x.login == loginUser);
            //test                           
	    }

        public DBOUSUARIOS GetUser(int userID) {

            if (!removeLinq) {
                return _connection.Table<DBOUSUARIOS>().FirstOrDefault(x => x.idUsuario == userID);
            } else {
                object[] argss = new object[1];
                argss[0] = userID;
                return _connection.Query<DBOUSUARIOS>("SELECT * FROM DBOUSUARIOS WHERE idUsuario = ? AND ativo = 1 LIMIT 1;", argss).FirstOrDefault();
            }

        }


        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <returns>The user list.</returns>
        /// <param name="ClassID">Class I.</param>
        public List<DBOUSUARIOS> GetUserList (int ClassID){
            if (!removeLinq) {
                IEnumerable<DBOUSUARIOS> result = _connection.Table<DBOUSUARIOS>().Where(x => x.idTurma == ClassID);
                List<DBOUSUARIOS> resultList = result.ToList();
                return resultList;
            } else {
                string command = "SELECT * FROM DBOUSUARIOS WHERE idTurma == ?";
                object[] variables = new object[1];
                variables[0] = ClassID;
                //IEnumerable<DBOUSUARIOS> result = _connection.Query(sqliteCommand, variables)
                List<DBOUSUARIOS> result = _connection.Query<DBOUSUARIOS>(command, variables);               
                return result;
            }
	    }


    public void InsertListOfUser(List<DBOUSUARIOS> _userList) {
        int tempCount = _userList.Count;
        for (int i = 0; i < tempCount; i++) {
            _connection.InsertOrReplace(_userList[i]);
        }
    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        /*public List<DBOUSUARIOS> GetUserList(int ClassID, int _escolaID, int clientID) {
            IEnumerable<DBOUSUARIOS> result = _connection.Table<DBOUSUARIOS>().Where(x => x.idTurma == ClassID && x.idEscola == _escolaID && x.idCliente == clientID);
            List<DBOUSUARIOS> resultList = result.ToList();
            return resultList;
        }*/

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="USER">USE.</param>
        public void UpdateUser(DBOUSUARIOS USER){
		    _connection.Update(USER);
	    }

        public void InsertUser(DBOUSUARIOS user) {
            _connection.InsertOrReplace(user);
        }

    #endregion

    #region DBOPONTUACAO

	    /// <summary>
	    /// Gets the score.
	    /// </summary>
	    /// <returns>The score.</returns>
	    /// <param name="userID">User I.</param>
	    public DBOPONTUACAO GetScore(int userID){
		    DBOPONTUACAO score = _connection.Table<DBOPONTUACAO>().FirstOrDefault(x => x.idUsuario == userID);
            if(score == null) {
                score = new DBOPONTUACAO();
            }
		    return score;
	    }

        public List<DBOPONTUACAO> GetallScoresOffline() {
            IEnumerable<DBOPONTUACAO> inventory = _connection.Table<DBOPONTUACAO>().Where(x => x.online == 0);
            return inventory.ToList();
        }

        public void DeleteScore(DBOPONTUACAO _delete) {
            _connection.Delete(_delete);
        }



        /// <summary>
        /// Update Score of user.
        /// </summary>
        /// <param name="score"></param>
        public void UpdateScore(DBOPONTUACAO score) {
            score.dataUpdate = ReturnCurrentDate();
            InsertOrReplateScore(score);
        }

        public void InsertScores(DBOPONTUACAO scory) {
            scory.dataUpdate = ReturnCurrentDate();
            InsertOrReplateScore(scory);
        }

        public void InsertOrReplateScore(DBOPONTUACAO _score) {
            //Connection.Query("UPDATE Book SET Name = '" + inputfield.text + "' WHERE Id = " + intBookNo);
            object[] argss = new object[7];
            argss[0] = _score.idUsuario;
            argss[1] = _score.brops;
            argss[2] = _score.pontuacaoTotal;
            argss[3] = _score.dataUpdate;
            argss[4] = _score.BropsDevice;
            argss[5] = _score.PontuacaoTotalDevice;
            argss[6] = _score.online;
            //object[] argss02 = new object[6];


            String command01 = "REPLACE INTO DBOPONTUACAO (idUsuario, brops, pontuacaoTotal, dataUpdate, BropsDevice, PontuacaoTotalDevice, online) VALUES(?,?,?,?,?,?,?);";
            //String commandFull = command01 + command02;
            // _connection.Query(commansd, argss);
            //_connection.CreateCommand(commansd,argss);
            _connection.Execute(command01, argss);
            //_connection.Execute(command02, argss2);
        }

        public void UpdateToOnlineScore(DBOPONTUACAO _score) {
            //Connection.Query("UPDATE Book SET Name = '" + inputfield.text + "' WHERE Id = " + intBookNo);
            object[] argss = new object[7];
            argss[0] = _score.idUsuario;
            argss[1] = _score.brops;
            argss[2] = _score.pontuacaoTotal;
            argss[3] = _score.dataUpdate;
            argss[4] = _score.BropsDevice;
            argss[5] = _score.PontuacaoTotalDevice;
            argss[6] = 1;
            //object[] argss02 = new object[6];


            String command01 = "REPLACE INTO DBOPONTUACAO (idUsuario, brops, pontuacaoTotal, dataUpdate, BropsDevice, PontuacaoTotalDevice, online) VALUES(?,?,?,?,?,?,?);";
            //String commandFull = command01 + command02;
            // _connection.Query(commansd, argss);
            //_connection.CreateCommand(commansd,argss);
            _connection.Execute(command01, argss);
            //_connection.Execute(command02, argss2);
        }

        #endregion

    #region DBESCOLA

    /// <summary>
    /// Gets the school list.
    /// </summary>
    /// <returns>The school list.</returns>
    /// <param name="idCliente">Identifier cliente.</param>
    public List<DBOESCOLA> GetSchoolList (int idCliente){
		IEnumerable<DBOESCOLA> result = _connection.Table<DBOESCOLA>().Where(x => x.idCliente == idCliente);
		List<DBOESCOLA> resultList = result.ToList();
		return resultList;
	}


	/// <summary>
	/// Gets the school.
	/// </summary>
	/// <returns>The school.</returns>
	/// <param name="idEscola">Identifier escola.</param>
	public DBOESCOLA GetSchool (int idEscola){
		DBOESCOLA result = _connection.Table<DBOESCOLA>().FirstOrDefault(x => x.idEscola == idEscola);
		return result;
	}

    public void ClearEscolas() {
        _connection.DropTable<DBOESCOLA>();
        _connection.CreateTable<DBOESCOLA>();
    }

    public void AddAllEscolas(List<DBOESCOLA> escolas) {
        _connection.InsertAll(escolas);
    }

#endregion

    #region DBOCLIENTE

	    /// <summary>
	    /// Gets the name of the client.
	    /// </summary>
	    /// <returns>The client name.</returns>
	    /// <param name="clientID">Client I.</param>
	    public string GetClientName(int clientID){
		    DBOCLIENTES cliente = _connection.Table<DBOCLIENTES>().FirstOrDefault(x => x.idCliente == clientID);
		    if (cliente != null) {
			    return cliente.nomeCliente;
		    } else {
			    return "";
		    }
	    }

	    /// <summary>
	    /// Gets the client.
	    /// </summary>
	    /// <returns>The client.</returns>
	    /// <param name="clientID">Client I.</param>
	    public DBOCLIENTES GetClient(int clientID){
		    DBOCLIENTES cliente = _connection.Table<DBOCLIENTES>().FirstOrDefault(x => x.idCliente == clientID);
		    return cliente;
	    }

    #endregion

    #region DBOANOLETIVO

	    /// <summary>
	    /// Gets the years list.
	    /// </summary>
	    /// <returns>The years list.</returns>
	    /// <param name="SchoolID">School ID.</param>
	    public List<DBOANOLETIVO> GetYearsList (){
            IEnumerable<DBOANOLETIVO> result = _connection.Table<DBOANOLETIVO>().AsEnumerable();
		    List<DBOANOLETIVO> resultList = result.ToList();
		    return resultList;
	    }


	    /// <summary>
	    /// Gets the class.
	    /// </summary>
	    /// <returns>The class.</returns>
	    /// <param name="idTurma">Identifier turma.</param>
	    public DBOANOLETIVO GetYears (int idAnoLetivo){
		    DBOANOLETIVO result = _connection.Table<DBOANOLETIVO>().FirstOrDefault(x => x.idAnoLetivo == idAnoLetivo);
		    return result;
	    }

        public void ClearAnoLetivo() {
            _connection.DropTable<DBOANOLETIVO>();
            _connection.CreateTable<DBOANOLETIVO>();
        }

        public void AddAllAnoLetivo(List<DBOANOLETIVO> anosLetivos) {
            _connection.InsertAll(anosLetivos);
        }

    #endregion

    #region DBOTURMA

	    /// <summary>
	    /// Gets the class list.
	    /// </summary>
	    /// <returns>The class list.</returns>
	    /// <param name="YearID">Year I.</param>
	    public List<DBOTURMA> GetClassList (int YearID){
		    IEnumerable<DBOTURMA> result = _connection.Table<DBOTURMA>().Where(x => x.idAnoLetivo == YearID);
		    List<DBOTURMA> resultList = result.ToList();
		    return resultList;
	    }


	    /// <summary>
	    /// Gets the class.
	    /// </summary>
	    /// <returns>The class.</returns>
	    /// <param name="idTurma">Identifier turma.</param>
	    public DBOTURMA GetClass (int idTurma){
            if (!removeLinq) {
                DBOTURMA result = _connection.Table<DBOTURMA>().FirstOrDefault(x => x.idTurma == idTurma);
                return result;
            } else {
                object[] argss = new object[1];
                argss[0] = idTurma;
                return _connection.Query<DBOTURMA>("SELECT * FROM DBOTURMA WHERE idTurma = ? LIMIT 1", argss).FirstOrDefault();
            }
	    }

        public void ClearTurmas() {
            _connection.DropTable<DBOTURMA>();
            _connection.CreateTable<DBOTURMA>();
        }

        public void AddAllTurmas(List<DBOTURMA> turmas) {
            _connection.InsertAll(turmas);
        }

    #endregion

    #region DBORANKING

	    /// <summary>
	    /// Gets the ranking.
	    /// </summary>
	    /// <returns>The ranking.</returns>
	    /// <param name="idMinigame">Identifier minigame.</param>
	    /// <param name="idUsuario">Identifier usuario.</param>
	    public DBORANKING GetRanking(int _idMinigame, int _idUsuario){
		    DBORANKING ranking = _connection.Table<DBORANKING>().FirstOrDefault(x => x.idMinigame == _idMinigame && x.idUsuario == _idUsuario);
		    return ranking;
	    }

        public List<DBORANKING> GetMinigameRanking(int idMinigame) {
            IEnumerable<DBORANKING> minigamesRankingDB = _connection.Table<DBORANKING>().Where(x => x.idMinigame == idMinigame && x.idUsuario != 0).OrderByDescending(x => x.highscore).Take(10);
            return minigamesRankingDB.ToList();
        }

        public List<DBORANKING> GetAllUserRanks(int idUsuario) {
            IEnumerable<DBORANKING> minigamesRankingDB = _connection.Table<DBORANKING>().Where(x => x.idUsuario == idUsuario).OrderBy(x => x.idMinigame).Take(5);
            return minigamesRankingDB.ToList();
        }

        public List<DBORANKING> GetAllOfflineRanks() {
            IEnumerable<DBORANKING> minigamesRankingDB = _connection.Table<DBORANKING>().Where(x => x.online == 0);
            return minigamesRankingDB.ToList();
        }

        public List<DBORANKFILTER> GetAllRankingsOfSchool(int idSchool, int idMinigame) {

            object[] objs = new object[2];
            objs[0] = idMinigame;
            objs[1] = idSchool;
            string cmdText = "SELECT * FROM DBORANKFILTER WHERE idMinigame = ? AND idEscola = ? ORDER BY DBORANKFILTER.highscore DESC LIMIT 10";
            return _connection.Query<DBORANKFILTER>(cmdText, objs);
            
        }

        public List<DBORANKFILTER> GetAllRankingsOfSchool(int idSchool,int idMinigame, int _idTurma)
        {

            object[] objs = new object[3];
            objs[0] = idMinigame;
            objs[1] = idSchool;
            objs[2] = _idTurma;
            string cmdText = "SELECT * FROM DBORANKFILTER WHERE idMinigame = ? AND idEscola = ? AND idTurma = ? ORDER BY DBORANKFILTER.highscore DESC LIMIT 10";
            return _connection.Query<DBORANKFILTER>(cmdText,objs);

        }


    /// <summary>
    /// Inserts the ranking.
    /// </summary>
    /// <param name="_p">P.</param>
    public void InsertRanking(DBORANKING _p){
		    _p.dataInsert = ReturnCurrentDate ();
		    _p.dataUpdate = ReturnCurrentDate ();
            InserRanking2(_p);
	    }

        public void InsertRanking(List<DBORANKING> _list) {
            int _countTemp = _list.Count;
            for (int i = 0; i < _countTemp; i++) {
                if (_list[i] != null) {
                    InserRanking2(_list[i]);
                }
            }
        }

        public void UpdateRankings(List<DBORANKING> _list) {
            int _countTemp = _list.Count;
            for (int i = 0; i < _countTemp; i++) {
                InserRanking2(_list[i]);
            }
        }

        public void UpdateRankings2(List<DBORANKING> _list) {
            int _countTemp = _list.Count;
            for (int i = 0; i < _countTemp; i++) {
                InserRanking2(_list[i]);
            }
        }

        public void UpdateOrReplaceDBORANKING(DBORANKING _ranking) {
            //Connection.Query("UPDATE Book SET Name = '" + inputfield.text + "' WHERE Id = " + intBookNo);
            object[] argss = new object[8];
            argss[0] = _ranking.idMinigame;
            argss[1] = _ranking.idUsuario;
            argss[2] = _ranking.highscore;
            argss[3] = _ranking.dataInsert;
            argss[4] = _ranking.dataUpdate;
            argss[5] = _ranking.posicao;
            argss[6] = _ranking.estrelas;
            argss[7] = _ranking.online;
            //object[] argss02 = new object[6];


            String command01 = "REPLACE INTO DBORANKING (idMinigame, idUsuario, highscore, dataInsert, dataUpdate, posicao, estrelas, online) VALUES(?,?,?,?,?,?,?,?);";
            //String commandFull = command01 + command02;
            // _connection.Query(commansd, argss);
            //_connection.CreateCommand(commansd,argss);
            _connection.Execute(command01, argss);
            //_connection.Execute(command02, argss2);
        }

        public void UpdateRanking(DBORANKING _ranking) {
            object[] argss = new object[8];
            argss[0] = _ranking.highscore;
            argss[1] = _ranking.dataInsert;
            argss[2] = _ranking.dataUpdate;
            argss[3] = _ranking.posicao;
            argss[4] = _ranking.estrelas;
            argss[5] = _ranking.online;
            argss[6] = _ranking.idMinigame;
            argss[7] = _ranking.idUsuario;

            String command02 = "UPDATE DBORANKING SET highscore=?,dataInsert=?,dataUpdate=?,posicao=?,estrelas=?,online=? WHERE idMinigame=? AND idUsuario=?;";
            _connection.Execute(command02, argss);
        }

        public void InserRanking2(DBORANKING _ranking) {
            object[] argss = new object[8];
            argss[0] = _ranking.idMinigame;
            argss[1] = _ranking.idUsuario;
            argss[2] = _ranking.highscore;
            argss[3] = _ranking.dataInsert;
            argss[4] = _ranking.dataUpdate;
            argss[5] = _ranking.posicao;
            argss[6] = _ranking.estrelas;
            argss[7] = _ranking.online;

            String command01 = "REPLACE INTO DBORANKING (idMinigame, idUsuario, highscore, dataInsert, dataUpdate, posicao, estrelas, online) VALUES(?,?,?,?,?,?,?,?);";
            _connection.Execute(command01, argss);
        }

        public void ClearRanking() {
            //_connection.DropTable<DBORANKING>();
            //_connection.CreateTable<DBORANKING>();
            String command01 = "DELETE FROM DBORANKING;";
            _connection.Execute(command01);

        }

	    /*public void UpdateRanking(DBORANKING _rank){
		    _rank.dataUpdate = ReturnCurrentDate ();
            UpdateOrReplaceDBORANKING(_rank);
	    }*/

    #endregion

    #region DBOPERGUNTAS
	    /// <summary>
	    /// Gets the question list.
	    /// </summary>
	    /// <returns>The question list.</returns>
	    /// <param name="idBook">Identifier book.</param>
	    public List<DBOPERGUNTAS> GetQuestionList (int idBook){
		    IEnumerable<DBOPERGUNTAS> result = _connection.Table<DBOPERGUNTAS>().Where(x => x.idLivro == idBook);
		    List<DBOPERGUNTAS> resultList = result.ToList();
		    return resultList;
	    }

        public List<DBOPERGUNTAS> GetQuestionList() {
            IEnumerable<DBOPERGUNTAS> result = _connection.Table<DBOPERGUNTAS>();
            List<DBOPERGUNTAS> resultList = result.ToList();
            return resultList;
        }

        public List<DBOPERGUNTAS> GetQuestionListE(int idBook, int _idCliente) {
            IEnumerable<DBOPERGUNTAS> result = _connection.Table<DBOPERGUNTAS>().Where(x => x.idLivro == idBook && x.idCliente == _idCliente);
            List<DBOPERGUNTAS> resultList = result.ToList();
            return resultList;
        }

        public List<DBOPERGUNTAS> GetQuestionListE(int _idCliente) {
            IEnumerable<DBOPERGUNTAS> result = _connection.Table<DBOPERGUNTAS>().Where(x => x.idCliente == _idCliente);
            List<DBOPERGUNTAS> resultList = result.ToList();
            return resultList;
        }

    public List<DBOPERGUNTAS> GetQuestionList(int idBook, int _idCliente) {
            IEnumerable<DBOPERGUNTAS> result = _connection.Table<DBOPERGUNTAS>().Where(x => x.idLivro == idBook && (x.idCliente == 1 || x.idCliente == _idCliente));
            List<DBOPERGUNTAS> resultList = result.ToList();
            return resultList;
        }

        public void DeleteQuestion(int _idPerguntas) {
            DBOPERGUNTAS temp = new DBOPERGUNTAS() {
                idPergunta = _idPerguntas
            };
            _connection.Delete(temp);
            //_connection.Delete(idPerguntas);
        }

    #endregion

    #region DBORESPOSTAS

	    /// <summary>
	    /// Gets the answers list.
	    /// </summary>
	    /// <returns>The answers list.</returns>
	    /// <param name="idPergunta">Identifier pergunta.</param>
	    public List<DBORESPOSTAS> GetAnswersList (int idPergunta){
		    IEnumerable<DBORESPOSTAS> result = _connection.Table<DBORESPOSTAS>().Where(x => x.idPergunta == idPergunta);
		    List<DBORESPOSTAS> resultList = result.ToList();
		    return resultList;
	    }

        public void DelAnswer(int _idRespostas) {
            DBORESPOSTAS temp = new DBORESPOSTAS() {
                idResposta = _idRespostas
            };
            _connection.Delete(temp);
        }

    #endregion

    #region DBOJOGOS_LOGS

	    public void InsertJogosLog(DBOMINIGAMES_LOGS LOG){
		    LOG.dataAcesso = ReturnCurrentDate ();
            InsertMinigamesLOG(LOG);
		    //Debug.Log(LOG.ToString());
	    }

        public List<DBOMINIGAMES_LOGS> GetAllMinigamesLog() {
            IEnumerable<DBOMINIGAMES_LOGS> result = _connection.Table<DBOMINIGAMES_LOGS>();
            List<DBOMINIGAMES_LOGS> resultList = result.ToList();
            return resultList;
        }

        public void DeleteMinigamesLog(DBOMINIGAMES_LOGS _minigameLog) {
            _connection.Delete(_minigameLog);
        }

        public void InsertMinigamesLOG(DBOMINIGAMES_LOGS _minigamesLog) {
            //Connection.Query("UPDATE Book SET Name = '" + inputfield.text + "' WHERE Id = " + intBookNo);
            object[] argss = new object[12];
            argss[0] = _minigamesLog.idUsuario;
            argss[1] = _minigamesLog.idMinigame;
            argss[2] = _minigamesLog.pontosLudica;
            argss[3] = _minigamesLog.pontosPedagogica;
            argss[4] = _minigamesLog.pontosInteragindo;
            argss[5] = _minigamesLog.personagem;
            argss[6] = _minigamesLog.dataAcesso;
            argss[7] = _minigamesLog.tempoLudica;
            argss[8] = _minigamesLog.tempoDidatica;
            argss[9] = _minigamesLog.faseLudica;
            argss[10] = _minigamesLog.deviceID;
            argss[11] = _minigamesLog.online;
            //object[] argss02 = new object[6];


            String command01 = "INSERT INTO DBOMINIGAMES_LOGS (idUsuario, idMinigame, pontosLudica, pontosPedagogica, pontosInteragindo, personagem, dataAcesso, tempoLudica, tempoDidatica, faseLudica, deviceID, online) VALUES(?,?,?,?,?,?,?,?,?,?,?,?);";
            //String command02 = "UPDATE DBOINVENTARIO SET quantidade=?,dataUpdate=?,dataInsert=?,ativo=?  WHERE idItem=? AND idUsuario=?;";
            //String commandFull = command01 + command02;
            // _connection.Query(commansd, argss);
            //_connection.CreateCommand(commansd,argss);
            _connection.Execute(command01, argss);
        }

        #endregion

    #region DBOGAMESLOG

    public void InserGamesLOG(DBOGAMES_LOGS LOG) {
        LOG.dataAcesso = ReturnCurrentDate();
        InsertDBOGAMES_LOG(LOG);
    }

    public List<DBOGAMES_LOGS> GetAllGamesLOG() {
        IEnumerable<DBOGAMES_LOGS> result = _connection.Table<DBOGAMES_LOGS>();
        List<DBOGAMES_LOGS> resultList = result.ToList();
        return resultList;
    }

    public void DeleteGamesLog(DBOGAMES_LOGS _minigameLog) {
        _connection.Delete(_minigameLog);
    }

    public void InsertDBOGAMES_LOG(DBOGAMES_LOGS _gamesLog) {
        //Connection.Query("UPDATE Book SET Name = '" + inputfield.text + "' WHERE Id = " + intBookNo);
        object[] argss = new object[6];
        argss[0] = _gamesLog.idUsuario;
        argss[1] = _gamesLog.idGame;
        argss[2] = _gamesLog.dataAcesso;
        argss[3] = _gamesLog.versao;
        argss[4] = _gamesLog.deviceID;
        argss[5] = _gamesLog.online;
        //object[] argss02 = new object[6];


        String command01 = "INSERT INTO DBOGAMES_LOGS (idUsuario, idGame, dataAcesso, versao, deviceID, online) VALUES(?,?,?,?,?,?);";
        //String command02 = "UPDATE DBOINVENTARIO SET quantidade=?,dataUpdate=?,dataInsert=?,ativo=?  WHERE idItem=? AND idUsuario=?;";
        //String commandFull = command01 + command02;
        // _connection.Query(commansd, argss);
        //_connection.CreateCommand(commansd,argss);
        _connection.Execute(command01, argss);
    }

    #endregion

    #region DBOESTATISTICA_DITATICA

    /// <summary>
    /// Inserts the statistic.
    /// </summary>
    /// <param name="Statistic">Statistic.</param>
    public void InsertStatistic(DBOESTATISTICA_DIDATICA Statistic){
		//Statistic.dataInsert = System.DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss'.'fff");
		Statistic.dataInsert = ReturnCurrentDate();
        InsertStatistic2(Statistic);
		Debug.Log(Statistic.ToString());
	}

    public void InsertAllStatistic(List<DBOESTATISTICA_DIDATICA> statistics) {
        //_connection.InsertAll(statistics);
        int countTemp = statistics.Count;
        for (int i = 0; i < countTemp; i++) {
            InsertStatistic2(statistics[i]);
        }
    }

    public void InsertStatistic2(DBOESTATISTICA_DIDATICA _statistic) {
        //Connection.Query("UPDATE Book SET Name = '" + inputfield.text + "' WHERE Id = " + intBookNo);
        object[] argss = new object[8];
        argss[0] = _statistic.idUsuario;
        argss[1] = _statistic.dataInsert;
        argss[2] = _statistic.idGameDidatico;
        argss[3] = _statistic.idHabilidade;
        argss[4] = _statistic.idDificuldade;
        argss[5] = _statistic.idLivro;
        argss[6] = _statistic.acertou;
        argss[7] = _statistic.online;
        //object[] argss02 = new object[6];


        String command01 = "INSERT INTO DBOESTATISTICA_DIDATICA (idUsuario, dataInsert, idGameDidatico, idHabilidade, idDificuldade, idLivro, acertou, online) VALUES(?,?,?,?,?,?,?,?);";
        //String command02 = "UPDATE DBOINVENTARIO SET quantidade=?,dataUpdate=?,dataInsert=?,ativo=?  WHERE idItem=? AND idUsuario=?;";
        //String commandFull = command01 + command02;
        // _connection.Query(commansd, argss);
        //_connection.CreateCommand(commansd,argss);
        _connection.Execute(command01, argss);
    }

    public List<DBOESTATISTICA_DIDATICA> GetAllStatisticDidatica() {
        IEnumerable<DBOESTATISTICA_DIDATICA> result = _connection.Table<DBOESTATISTICA_DIDATICA>();
        List<DBOESTATISTICA_DIDATICA> resultList = result.ToList();
        return resultList;
    }

    public void DeleteEstatistica(DBOESTATISTICA_DIDATICA _minigameLog) {
        _connection.Delete(_minigameLog);
    }



    #endregion

    #region DBOSINCRONIZACAO

    public DBOSINCRONIZACAO GetSync(int idCliente) {
        DBOSINCRONIZACAO sinc = _connection.Table<DBOSINCRONIZACAO>().Where(x => x.idCliente == idCliente).FirstOrDefault();
        if(sinc == null) {
            sinc = new DBOSINCRONIZACAO();
        }
        return sinc;
    }

    public void UpdateSync(DBOSINCRONIZACAO syncTemp) {
        _connection.Update(syncTemp);
    }

    /// <summary>
    /// Returns the current date.
    /// </summary>
    /// <returns>The current date.</returns>
    string ReturnCurrentDate(){
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        //return System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
    }

#endregion

    #region DBOPERGUNTAS

        public void ClearAllPerguntas() {
            _connection.DropTable<DBOPERGUNTAS>();
            _connection.CreateTable<DBOPERGUNTAS>();
        }

        public void AddAllPerguntas(List<DBOPERGUNTAS> perguntas) {
            int size = perguntas.Count;
            for (int i = 0; i < size; i++) {
                _connection.InsertOrReplace(perguntas[i]);
            }
            //_connection.InsertAll(perguntas);
        }
        
    public void AddPerguntaOrReplace(DBOPERGUNTAS _pergunta) {
        _connection.InsertOrReplace(_pergunta);
    }

        public List<DBOPERGUNTAS> PerguntasToDownload() {

            return _connection.Query<DBOPERGUNTAS>("SELECT * FROM DBOPERGUNTAS WHERE downloaded == 0 AND ativo = 1 AND audio = 1", new object[0]);

        }
    #endregion

    #region DBORESPOSTAS

        public void ClearRespostas() {
            _connection.DropTable<DBORESPOSTAS>();
            _connection.CreateTable<DBORESPOSTAS>();
        }

        public void AddAllRespostas(List<DBORESPOSTAS> respostas) {
            //_connection.InsertAll(respostas);
            int size = respostas.Count;
            for (int i = 0; i < size; i++) {
                _connection.InsertOrReplace(respostas[i]);
            }
        }

    public void AddRespostaOrReplace(DBORESPOSTAS _resposta) {
        _connection.InsertOrReplace(_resposta);
    }

    public List<DBORESPOSTAS> RespostaNotDownloaded() {
        return _connection.Query<DBORESPOSTAS>("SELECT * FROM DBORESPOSTAS WHERE downloaded == 0 AND ativo = 1 AND audio = 1", new object[0]);
    }

    #endregion
    #region category_item

    public DBOITENS_CATEGORIAS[] GetCategoryItem() {
    IEnumerable<DBOITENS_CATEGORIAS> categorys = _connection.Table<DBOITENS_CATEGORIAS>();
    return categorys.ToArray();
    }

    public DBOITENS_CATEGORIAS[] GetCategoryItem(int _idGame) {
    IEnumerable<DBOITENS_CATEGORIAS> categorys = _connection.Table<DBOITENS_CATEGORIAS>().Where(x => x.idGame == _idGame);
    return categorys.ToArray();
    }

    public void AddItensCategory(List<DBOITENS_CATEGORIAS> _itensCategory) {
    int _tempCount = _itensCategory.Count;
    for (int i = 0; i < _tempCount; i++) {
        _connection.InsertOrReplace(_itensCategory[i]);
    }
    //_connection.InsertAll(_itensCategory);
    }

    #endregion

    #region storeItem

    public DBOITENS[] GetItensStore() {
        IEnumerable<DBOITENS> storeItens = _connection.Table<DBOITENS>().Where(x => x.ativo == 1);
        return storeItens.ToArray();
    }

    public DBOITENS GetItemStore(int _idItem) {
        DBOITENS storeItens = _connection.Table<DBOITENS>().Where(x => x.idItem == _idItem).FirstOrDefault() ;
        return storeItens;
    }

    public List<DBOITENS> GetItensStoreList() {
        IEnumerable<DBOITENS> storeItens = _connection.Table<DBOITENS>().Where(x => x.ativo == 1);
        return storeItens.ToList();
    }

    public void AddItens(List<DBOITENS> _itens) {
        int _countTemp = _itens.Count;
        for (int i = 0; i < _countTemp; i++) {

            if (!removeLinq) {
                _connection.InsertOrReplace(_itens[i]);
            } else {
                string commandSQLITE = "REPLACE INTO DBOITENS (idItem, idCliente, idCategoriaItem, nomeItem, infoItem, valor, ativo, dataUpdate, Downloaded) values (?,?,?,?,?,?,?,?,?)";
                object[] variables = new object[9];
                variables[0] = _itens[i].idItem;
                variables[1] = _itens[i].idCliente;
                variables[2] = _itens[i].idCategoriaItem;
                variables[3] = _itens[i].nomeItem;
                variables[4] = _itens[i].infoItem;
                variables[5] = _itens[i].valor;
                variables[6] = _itens[i].ativo;
                variables[7] = _itens[i].dataUpdate;
                variables[8] = _itens[i].downloaded;
                _connection.Execute(commandSQLITE, variables);
            }


        }
    }

    public List<DBOITENS> NotDownloadedItens() {
        string commandSQLITE = "SELECT * FROM DBOITENS WHERE downloaded == 0 AND ativo = 1";
        return _connection.Query<DBOITENS>(commandSQLITE,new object[0]);
    }

    #endregion

    #region Inventario

    public List<DBOINVENTARIO> GetAllInventory() {
        IEnumerable<DBOINVENTARIO> inventory = _connection.Query<DBOINVENTARIO> ("select * from DBOINVENTARIO where online = ?", 1);
        //IEnumerable<DBOINVENTARIO> inventory = _connection.Table<DBOINVENTARIO>().Where(x => x.online == 0);
        return inventory.ToList();
    }

    public DBOINVENTARIO[] GetInventory(int _idUser) {
        IEnumerable<DBOINVENTARIO> inventory = _connection.Table<DBOINVENTARIO>().Where(x => x.idUsuario == _idUser && x.quantidade >= 1);
        return inventory.ToArray();
    }    

    public void SetInventario(DBOINVENTARIO _item) {
        UpdateOrReplateInventory(_item);
    }

    public void SetInventario(List<DBOINVENTARIO> _itens) {
        int tempCount = _itens.Count;
        for (int i = 0; i < tempCount; i++) {
            UpdateOrReplateInventory(_itens[i]);
        }
    }

    public void UpdateItem(DBOINVENTARIO _temp) {
        UpdateOrReplateInventory(_temp);
    }

    public void UpdateOrReplateInventory(DBOINVENTARIO _itemUpdate){
        //Connection.Query("UPDATE Book SET Name = '" + inputfield.text + "' WHERE Id = " + intBookNo);
        object[] argss = new object[8];
        argss[0] = _itemUpdate.idItem;
        argss[1] = _itemUpdate.deviceQuantity;
        argss[2] = _itemUpdate.idUsuario;
        argss[3] = _itemUpdate.quantidade;
        argss[4] = _itemUpdate.dataUpdate;
        argss[5] = _itemUpdate.dataInsert;
        argss[6] = _itemUpdate.ativo;
        argss[7] = _itemUpdate.online;
        //object[] argss02 = new object[6];


        String command01 = "REPLACE INTO DBOINVENTARIO (idItem,deviceQuantity , idUsuario, quantidade, dataUpdate, dataInsert, ativo, online) VALUES(?,?,?,?,?,?,?,?);";
        //String command02 = "UPDATE DBOINVENTARIO SET quantidade=?,dataUpdate=?,dataInsert=?,ativo=?  WHERE idItem=? AND idUsuario=?;";
        //String commandFull = command01 + command02;
       // _connection.Query(commansd, argss);
       //_connection.CreateCommand(commansd,argss);
       _connection.Execute(command01, argss);
    }
    #endregion

    public void InsertEstatisticaDidatica(DBOESTATISTICA_DIDATICA _ed) {

        object[] arguments = new object[8];
        arguments[0] = _ed.idUsuario;
        arguments[1] = _ed.dataInsert;
        arguments[2] = _ed.idGameDidatico;
        arguments[3] = _ed.idHabilidade;
        arguments[4] = _ed.idDificuldade;
        arguments[5] = _ed.idLivro;
        arguments[6] = _ed.acertou;
        arguments[7] = _ed.online;

        string queryCommand = "INSERT INTO DBOESTATISTICA_DIDATICA VALUES (?,?,?,?,?,?,?,?);";

        _connection.Execute(queryCommand, arguments);
    }

    public DBOGAMEDIDATICOS_PROGRESSO GetGameDidatico_PROGRESSO(int _idUsuario, int idGameDidatico) {

        object[] arguments = new object[2];
        arguments[0] = _idUsuario;
        arguments[1] = idGameDidatico;

        string queryCommand = "SELECT * FROM DBOGAMEDIDATICOS_PROGRESSO WHERE idUsuario = ? AND idGameDidatico = ?;";

        return _connection.Query<DBOGAMEDIDATICOS_PROGRESSO>(queryCommand, arguments).FirstOrDefault();
    }

    public List<DBOGAMEDIDATICOS_PROGRESSO> GetAllOfflineDidatico_Progresso() {
        object[] arguments = new object[0];

        string queryCommand = "SELECT * FROM DBOGAMEDIDATICOS_PROGRESSO WHERE online = 0";

        return _connection.Query<DBOGAMEDIDATICOS_PROGRESSO>(queryCommand, arguments).ToList();
    }


    public List<DBOGAMESDIDATICOS_HABILIDADES> GetAllGamesDidaticos_Habilidades(int idGameDidatico = -1){
        var arguments = new object[0];
        var query = idGameDidatico != -1 ? string.Format(
                "SELECT * FROM DBOGAMESDIDATICOS_HABILIDADES WHERE IdGameDidatico = {0};", idGameDidatico.ToString())
            : "SELECT * FROM DBOGAMESDIDATICOS_HABILIDADES;";
        return _connection.Query<DBOGAMESDIDATICOS_HABILIDADES>(query, arguments).ToList();
    }
    
    public List<DBOPERGUNTAS_GAMES> GetAllPerguntas_Games(int idAnoLetivo){
        var arguments = new object[0];
        var query = string.Format("SELECT * FROM DBOPERGUNTAS_GAMES WHERE idAnoLetivo = {0} AND ativo = 1;", idAnoLetivo.ToString());
        return _connection.Query<DBOPERGUNTAS_GAMES>(query, arguments).ToList();
    }
    
    public List<DBOPERGUNTAS_GAMES> GetAllPerguntas_Games(int idHabilidade, int idAnoLetivo){
        var arguments = new object[0];
        var query = string.Format("SELECT * FROM DBOPERGUNTAS_GAMES WHERE idHabilidade = {0} AND ativo = 1 and idAnoLetivo = {1};", idHabilidade.ToString(), idAnoLetivo.ToString());
        return _connection.Query<DBOPERGUNTAS_GAMES>(query, arguments).ToList();
    }
    
    public List<DBORESPOSTA_GAMES> GetAllRespostas_Games(int idPergunta){
        var arguments = new object[0];
        var query = string.Format("SELECT * FROM DBORESPOSTA_GAMES WHERE IdPergunta = {0}", idPergunta.ToString());
        return _connection.Query<DBORESPOSTA_GAMES>(query, arguments).ToList();
    }


    public void InsertGamesDidatico_Progresso(DBOGAMEDIDATICOS_PROGRESSO _GDP) {
        object[] arguments = new object[5];
        arguments[0] = _GDP.idGameDidatico;
        arguments[1] = _GDP.idUsuario;
        arguments[2] = _GDP.highscore;
        arguments[3] = _GDP.estrelas;
        arguments[4] = _GDP.online;

        string queryCommand = "REPLACE INTO DBOGAMEDIDATICOS_PROGRESSO VALUES(?,?,?,?,?);";

        _connection.Execute(queryCommand, arguments);
    }

    public void InsertPerguntasGames(List<DBOPERGUNTAS_GAMES> listPerguntasGames){
        foreach (var e in listPerguntasGames){
            InsertPerguntaGames(e);
        }
    }

    public void InsertPerguntaGames(DBOPERGUNTAS_GAMES pergunta){
        object[] argments = new object[11];
        argments[0] = pergunta.idPergunta;
        argments[1] = pergunta.idHabilidade;
        argments[2] = pergunta.idCliente;
        argments[3] = pergunta.idDificuldade;
        argments[4] = pergunta.textoPergunta;
        argments[5] = pergunta.layout;
        argments[6] = pergunta.imagem;
        argments[7] = pergunta.audio;
        argments[8] = pergunta.ativo;
        argments[9] = pergunta.downloaded;
        argments[10] = pergunta.idAnoLetivo;
        
        string queryCommand = "REPLACE INTO DBOPERGUNTAS_GAMES VALUES(?,?,?,?,?,?,?,?,?,?,?);";

        _connection.Execute(queryCommand, argments);
    }
    
    public void InserRespostasGame(List<DBORESPOSTA_GAMES> listRespostasGames){
        foreach (var e in listRespostasGames){
            InsertRespostaGames(e);
        }
    }
    
    public void InsertRespostaGames(DBORESPOSTA_GAMES respostas){
        object[] argments = new object[7];
        argments[0] = respostas.IdResposta;
        argments[1] = respostas.IdPergunta;
        argments[2] = respostas.TextoResposta;
        argments[3] = respostas.correta;
        argments[4] = respostas.audio;
        argments[5] = respostas.imagem;
        argments[6] = respostas.downloaded;
        
        string queryCommand = "REPLACE INTO DBORESPOSTA_GAMES VALUES(?,?,?,?,?,?,?);";

        _connection.Execute(queryCommand, argments);
    }

    public DBOGAMES_DIDATICOS GetGameDidatico(int _idGameDidatico) {
        object[] arguments = new object[1];
        arguments[0] = _idGameDidatico;

        string queryCommand = "SELECT * FROM DBOGAMES_DIDATICOS WHERE idGameDidatico = ?;";

        return _connection.Query<DBOGAMES_DIDATICOS>(queryCommand, arguments).FirstOrDefault();

    }

    public List<DBOGAMES_DIDATICOS> GetAllGamesDidaticos() {
        object[] arguments = new object[0];

        string queryCommand = "SELECT * FROM DBOGAMES_DIDATICOS";

        return _connection.Query<DBOGAMES_DIDATICOS>(queryCommand, arguments).ToList();
    }

    public void InsertGamesDidatico(DBOGAMES_DIDATICOS _GD) {
        _connection.InsertOrReplace(_GD);
    }

    public List<DBOGAMESDIDATICOS_LOGS> GetAllDidaticosLog() {
        object[] arguments = new object[0];

        string queryCommand = "SELECT * FROM DBOMINIGAMES_LOGS";

        return _connection.Query<DBOGAMESDIDATICOS_LOGS>(queryCommand, arguments).ToList();
    }

    public void InsertGamesDidaticos_Logs(DBOGAMESDIDATICOS_LOGS _logs) {

        _connection.Insert(_logs);

        /*object[] arguments = new object[9];
        arguments[0] = _logs.idUsuario;
        arguments[1] = _logs.idGameDidatico;
        arguments[2] = _logs.pontos;
        arguments[3] = _logs.personagem;
        arguments[4] = _logs.tempo;
        arguments[5] = _logs.fase;
        arguments[6] = _logs.deviceID;
        arguments[7] = _logs.dataAcesso;
        arguments[8] = _logs.online;

        string queryCommand = "INSERT INTO DBOGAMESDIDATICOS_LOGS VALUES(?,?,?,?,?,?,?,?,?);";

        _connection.Execute(queryCommand, arguments);*/
    }

    public void RemoveGamesDidaticos_logs(DBOGAMESDIDATICOS_LOGS _logs) {
        _connection.Delete(_logs);
    }

    public void InsertPermissao_Didaticos(DBOPERMISSOES_GAMESDIDATICOS _PGD) {

        object[] arguments = new object[5];

        arguments[0] = _PGD.idGameDidatico;
        arguments[1] = _PGD.idLivro;
        arguments[2] = _PGD.idAnoLetivo;
        arguments[3] = _PGD.idTurma;
        arguments[4] = _PGD.acesso;

        string queryCommand = "INSERT INTO DBOPERMISSOES_GAMESDIDATICOS VALUES(?,?,?,?,?);";

        _connection.Execute(queryCommand, arguments);
    }  
    
    

    public void InsertPermissao_Livro(DBOPERMISSOES_LIVROS _PL) {

        object[] arguments = new object[4];

        arguments[0] = _PL.idLivro;
        arguments[1] = _PL.idAnoLetivo;
        arguments[2] = _PL.idTurma;
        arguments[3] = _PL.acesso;

        string queryCommand = "INSERT INTO DBOPERMISSOES_LIVROS VALUES(?,?,?,?);";

        _connection.Execute(queryCommand, arguments);
    }

    public void UpdateMinigames(List<DBOMINIGAMES> _minigames) {
        int tempCount = _minigames.Count;
        for (int i = 0; i < tempCount; i++) {
            _connection.InsertOrReplace(_minigames[i]);
        }
    }

    public List<DBOMINIGAMES> GetAllMinigames() {
        IEnumerable<DBOMINIGAMES> inventory = _connection.Table<DBOMINIGAMES>().Where(x => x.ativo == 1);
        return inventory.ToList();
    }

    public DBOMINIGAMES GetMinigameInfo(int _idMinigame)
    {
        DBOMINIGAMES _minigame = _connection.Table<DBOMINIGAMES>().FirstOrDefault(x => x.ativo == 1 && x.idMinigames == _idMinigame);
        
        return _minigame;
    }


    public void RunQuery(string query) {
        object[] argsss = new object[1];
        //var cmd = _connection.CreateCommand(query, argsss);
        _connection.Execute(query, argsss);
        //_connection.Query(query, argsss);
    }

    public void ExecuteSQLITECommmand(string sqliteQuery, object[] variables) {
        //object[] argsss = new object[1];
        //var cmd = _connection.CreateCommand(query, argsss);
        _connection.Execute(sqliteQuery, variables);
        //_connection.Query(query, argsss);
    }

    public DBOVERSION GetDBVersion() {
        return _connection.Table<DBOVERSION>().FirstOrDefault(x => x.id == 1);       
    }

    public void UpdateDBVersion(DBOVERSION _dbv) {
        _connection.Update(_dbv);
    }

    public void InsertOrReplaceRecentItem(int userID, int itemId) {

        object[] argss = new object[2];

        argss[0] = userID;
        argss[1] = itemId;

        string command01 = "REPLACE INTO DBORECENTITEM VALUES(?,?);";

        _connection.Execute(command01, argss);
    }

    public void removeRecentItem(int _userID, int _itemID) {

        object[] argumentValues = new object[2];
        argumentValues[0] = _userID;
        argumentValues[1] = _itemID;

        string queryCommand = "DELETE FROM DBORECENTITEM WHERE userId=? AND itemId=?";

        _connection.Execute(queryCommand,argumentValues);
    }

    public List<DBORECENTITEM> GetAllRecentOfUser(int userID) {
        IEnumerable<DBORECENTITEM> recentItens = _connection.Table<DBORECENTITEM>().Where(x => x.userId == userID);
        return recentItens.ToList();
    }

    public void CloseConnection(){
        _connection.Close();
    }



}
