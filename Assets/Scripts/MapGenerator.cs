using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour {
  public int mapWidth =12;
  public int mapHeight=12;
  public GameObject mapContainer ;
  public GameObject tilePrefab;
  public Vector2 tileSize = new Vector2 (16, 16);
  public Texture2D texture;
  [Range(0,.9f)] public float erodePercent = 0.5f;
  [Range(0 ,4)] public  int erodeIterations=0;
  [Range(0,.9f)] public  float treePercent = 0.5f;
  [Range(0,.9f)] public  float hillsPercent = 0.5f;
  [Range(0,.9f)] public  float mountainsPercent = 0.5f;
  [Range(0,.9f)] public  float townPercent = 0.5f;
  [Range(0,.9f)] public  float monsterPercent = 0.5f;
  [Range(0,.9f)] public  float lakePercent = 0.5f;
  [Range(0,.9f)] public Map map;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  public void Start () {
    this.mapContainer = this.gameObject;
    tilePrefab = (GameObject)Resources.Load("Tile Prefab");
    texture= (Texture2D) Resources.Load("4-side-island-autotiles");
    this.map = new Map ();
  }
  public void InstantiateNewMap () {
    map.NewMap (mapWidth, mapHeight);
    map.CreateIsland(erodePercent,erodeIterations,treePercent,hillsPercent,mountainsPercent,townPercent,monsterPercent,lakePercent);
    CreateGrid ();
    CenterMap(map.castleTile.id);
  }
  public void CreateGrid () {
    ClearMapContainer ();
    Sprite[] allSprites = Resources.LoadAll<Sprite> (texture.name);
    int totalSize = (int) this.map.tiles.Length;
    int maxColumns = (int) map.columns;
    int columnControler = 0;
    int rowControler = 0;
    for (int i = 0; i < totalSize; i++) {
      columnControler = (int) (i % maxColumns);
      var newX = columnControler * this.tileSize.x;
      var newY = -rowControler * this.tileSize.x;
      GameObject prefab = Instantiate (tilePrefab);
      prefab.transform.SetParent (mapContainer.transform);
      prefab.transform.position = new Vector3 (newX, newY, 0);
      var tile = map.tiles[i];
      prefab.name = GetTileName(tile.autoTileID);
      int spriteID = tile.autoTileID;      
      if(spriteID>=0){
        // spriteID = 0;
        var sr = prefab.GetComponent<SpriteRenderer> ();
        sr.sprite = allSprites[spriteID];
      }
      if (columnControler == (maxColumns - 1)) {
        rowControler++;
      }
    }
  }
  public void ClearMapContainer () {
    var children = mapContainer.transform.GetComponentsInChildren<Transform> ();
    for (int i = children.Length - 1; i > 0; i--) {
      Destroy (children[i].gameObject);
    }
  }
  public void CenterMap(int _index){
    var camPos = Camera.main.transform.position;
    var width = map.columns;
    camPos.x = (_index % width) * tileSize.x;
		camPos.y = -((_index / width) * tileSize.y);
		Camera.main.transform.position = camPos;
  }
  public string GetTileName(int _tile){
    if(_tile<=(int)TileType.EMPTY){
      return TileType.EMPTY.ToString();
    }
    else if(_tile<=(int)TileType.GRASS){return TileType.GRASS.ToString();}
    else if(_tile<=(int)TileType.TREE){return TileType.TREE.ToString();}
    else if(_tile<=(int)TileType.HILLS){return TileType.HILLS.ToString();}
    else if(_tile<=(int)TileType.MOUNTAINS){return TileType.MOUNTAINS.ToString();}
    else if(_tile<=(int)TileType.TOWNS){return TileType.TOWNS.ToString();}
    else if(_tile<=(int)TileType.MONSTER){return TileType.MONSTER.ToString();}
    else{return "CASTLE";}
  }
}