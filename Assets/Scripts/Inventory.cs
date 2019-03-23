using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : ScriptableObject {

    public GameConfig config;
    private int userid;
    public List<DBORECENTITEM> RecentItens = new List<DBORECENTITEM>();

    public void LoadRecentItens() {
        RecentItens.Clear();
        RecentItens = config.openDB().GetAllRecentOfUser(config.GetCurrentUserID());
    }
	
    public void RemoveRecentItem(int _itemID) {
        DBORECENTITEM _removeRecent = RecentItens.Where(x => x.itemId == _itemID).FirstOrDefault();
        config.openDB().removeRecentItem(config.playerID,_removeRecent.itemId);
        RecentItens.Remove(_removeRecent);
    }

    public void AddRecentItem(int _userID, int _itemID) {
        config.openDB().InsertOrReplaceRecentItem(_userID,_itemID);
        LoadRecentItens();
    }

    public bool isItemRecent(int _itemID) {
        if (RecentItens.Any(x => x.itemId == _itemID)) {
            return true;
        } else {
            return false;
        }
    }
}
