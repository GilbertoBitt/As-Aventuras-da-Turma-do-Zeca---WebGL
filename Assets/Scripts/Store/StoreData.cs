using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreData : ScriptableObject {

    public List<StoreItem> itensOnStore = new List<StoreItem>();
    public List<int> buyedItens = new List<int>();
    public DBOITENS_CATEGORIAS[] itensCategory;
    public GameConfig config;
    private DataService db;

    public StoreItem GetItem(int id) {
        return itensOnStore.Find(x => x.itemID == id);
    }
    
    public void OnValidate() {
        int count = itensOnStore.Count;

        for (int i = 0; i < count; i++) {
            itensOnStore[i].itemID = i+1;
        }
    }

    public StoreItem dboItensToStoreItens(DBOITENS _item) {
        return new StoreItem {
            itemName = _item.nomeItem,
            itemID = _item.idItem,
            itemBuyValue = _item.valor,
            //itemIcon = LoadPNG(config.fullPatchItemIcon + _item.idItem +".png"),
            itemCategoryID = _item.idCategoriaItem
        };
    }

    public void SetDataService(DataService _db) {
        db = _db;
    }

    public void SaveItemInventory(StoreItem _item) {
        //adicionar suporte para categorias.
        DBOINVENTARIO itemInv = new DBOINVENTARIO() {
            idUsuario = config.GetCurrentUserID(),
            idItem = _item.itemID,
            ativo = 1,
            dataInsert = config.ReturnCurrentDate(),
            dataUpdate = config.ReturnCurrentDate(),
            quantidade = 1,
            deviceQuantity = 1,
        };

        config.openDB().InsertOrReplaceRecentItem(itemInv.idUsuario,itemInv.idItem);        
        
        if (!config.isOnline) {
            itemInv.online = 0;
            db.UpdateOrReplateInventory(itemInv);
            config.UpdateScore(config.BropsAmount, config.TotalPoints);
            //SendToLog(itemInv, _item.itemBuyValue);
        } else {
            itemInv.online = 1;
            //db.UpdateOrReplateInventory(itemInv);
            config.NetHelper.SetInventory(itemInv, _item.itemBuyValue);
            Debug.Log(_item.itemBuyValue.ToString());
        }
    }    



    public static Texture2D LoadPNG(string filePath) {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath)) {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
        }
        return tex;
    }

    public Texture2D LoadPNGbyID(int _itemID) {


        //Verificação da imagem do item dentro do streaming assets.
        var filePath = string.Format("{0}{1}.png", config.fullPatchItemIcon, _itemID);
        StringFast streamingAssetPatch = new StringFast();
        //Verificar se arquivo de imagem existe no PersistentPath.
        //Adicionar FLAG de verificação de download. [ ] [WARNING]
        if (File.Exists(filePath)) {
            return LoadPNG(filePath);
        } else {
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_STANDALONE
            // PC/Unity Editor Streaming Asset.
            streamingAssetPatch.Clear();
            //Gerarando caminho do streaming assets folder para PC.
            streamingAssetPatch.Append(Application.streamingAssetsPath).Append("/medias/item/").Append(_itemID).Append(".png");
            Debug.LogWarning(streamingAssetPatch.ToString(), this);
            //Carregando bytes do arquivo de imagem em streaming assets.
            var itemIconBytesLoaded = File.ReadAllBytes(streamingAssetPatch.ToString());
            //Salvando bytes de streaming asset em PersistentDataPath.
            File.WriteAllBytes(filePath, itemIconBytesLoaded);
#elif UNITY_ANDROID
            //Android Streaming Asset.
            streamingAssetPatch.Append(Application.streamingAssetsPath).Append("/medias/item/").Append(_itemID).Append(".png");
            Debug.LogWarning(streamingAssetPatch.ToString(), this);

            //Carregar do StreamingAssets 'apk' arquivo de imagem.
            var loadDb = new WWW(streamingAssetPatch.ToString());
            while (!loadDb.isDone) { }
            //Copiar dados carregados do streaming assets 
            File.WriteAllBytes(filePath, loadDb.bytes);
            // Android Android Ended.
#elif UNITY_IOS
            streamingAssetPatch.Clear();
            streamingAssetPatch.Append(Application.streamingAssetsPath).Append("/medias/item/").Append(_itemID).Append(".png");
            Debug.LogWarning(streamingAssetPatch.ToString(), this);
            var ItemIconPath = streamingAssetPatch.ToString();
            File.Copy(ItemIconPath, filePath);
#endif
            return StoreData.LoadPNG(filePath);

        }

    }

}
