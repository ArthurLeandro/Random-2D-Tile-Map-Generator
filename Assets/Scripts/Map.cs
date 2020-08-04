using UnityEngine;
using System.Linq;
public class Map {
  public Tile[] tiles;
  public int columns, row;
  public Tile[] coastTiles{
    get{
      return tiles.Where(t 
        => t.autoTileID < (int)TileType.GRASS).ToArray();
        
    }
  }
  public Tile[] landTiles{
    get{
      return tiles.Where(t=>t.autoTileID == (int)TileType.GRASS).ToArray();
    }
  }
  public Tile castleTile{
    get{
      return tiles.FirstOrDefault(t =>t.autoTileID == (int)TileType.CASTLE);
    }
  }
  public void NewMap (int _width, int _height) {
    this.columns = _width;
    this.row = _height;
    this.tiles = new Tile[this.columns * this.row];
    CreateNewMap();
  }
  public void CreateNewMap () {
    int total = tiles.Length;
    for (int i = 0; i < total; i++) {
      Tile tile = new Tile ();
      tile.id = (ushort)i;
      tiles[i] = tile;

    }
    FindNeighbors();
  }
  public void CreateIsland(float _eroadPercent,int _erodeIterations,float _treePercent,float _hillPercent,float _mountainPercent,float _townPercent,float _monsterPercent,float _lakePercent){
    var land = landTiles;
    var coast = coastTiles;
    DecorateTiles(land,_lakePercent,TileType.EMPTY);
    for (int i = 0; i < _erodeIterations; i++){
      DecorateTiles(coast,_eroadPercent,TileType.EMPTY);
    }    
    Tile[] openTiles = land;
    RandomizeTileArray(openTiles);
      openTiles[0].autoTileID = (int)TileType.CASTLE; //!Way to always generate something that's really important 
    //! A ORDEM QUE OS METODOS É CHAMADO É IMPORTANTE 
    DecorateTiles(land,_treePercent,TileType.TREE);
    DecorateTiles(land,_hillPercent,TileType.HILLS);
    DecorateTiles(land,_mountainPercent,TileType.MOUNTAINS);
    DecorateTiles(land,_townPercent,TileType.TOWNS);
    DecorateTiles(land,_monsterPercent,TileType.MONSTER);
  }
  public void FindNeighbors(){
    for (int r = 0; r < row; r++){
      for (int c = 0; c < columns; c++){
        Tile tile = tiles[columns*r+c];
        if(r<row-1){
          tile.AddNeighbor(Sides.BOTTOM,tiles[columns*(r+1)+c]);
        } 
        if(c<columns-1){
          tile.AddNeighbor(Sides.RIGHT,tiles[columns*r+c+1]);
        } 
        if(c>0){
          tile.AddNeighbor(Sides.LEFT,tiles[columns*r+c-1]);
        } 
        if(r>0){
          tile.AddNeighbor(Sides.TOP,tiles[columns*(r-1)+c]);
        } 
      }
    }
  }

  
  //TODO Tudo está sendo randomizado enquanto tem que ser somente os coast tiles
  public void DecorateTiles(Tile[] _tiles, float _percent, TileType _type){
    var total = Mathf.FloorToInt(tiles.Length*_percent);
    RandomizeTileArray(_tiles);
    for (int i = 0; i < total; i++){
      var tile = tiles[i];
      if(_type==TileType.EMPTY){
        tile.ClearNeighbors();
      }
      tile.autoTileID = (int)_type;
    }
  }
  public  void RandomizeTileArray(Tile[] _tiles){
    for (int i = 0; i < _tiles.Length; i++){
      var tmp = tiles[i];
      var r = Random.Range(i,tiles.Length);
      tiles[i] = tiles[r];
      tiles[r] = tmp;
    }
  }
}