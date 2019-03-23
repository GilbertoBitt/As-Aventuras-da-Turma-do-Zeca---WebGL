#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOVERSION {

    [PrimaryKey][NotNull][Unique]
    public int id { get; set; }
    public int version { get; set; }

}
