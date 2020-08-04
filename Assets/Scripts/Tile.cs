using System;
using System.Text;
using UnityEngine;
public class Tile {
  public ushort id = 0;
  public Tile[] neighbors = new Tile[4];
  public int autoTileID;
  public void AddNeighbor (Sides _side, Tile _tile) {
    neighbors[(int) _side] = _tile;
    CalculateAutoTileID ();
  }
  public void RemoveNeighbor(Tile tile){
    var total = neighbors.Length;
    for (int i = 0; i < total; i++){
      if (neighbors[i]!=null){
        if (neighbors[i].id==tile.id){
          neighbors[i] = null;
        }
      }
    }
    CalculateAutoTileID();
  }
  public void ClearNeighbors(){
    var total = neighbors.Length;
    for (int i = 0; i < total; i++){
      var tile = neighbors[i];
      if (tile!=null){
        tile.RemoveNeighbor(this);
        neighbors[i] = null;
      }
    }
    CalculateAutoTileID();
  }
  public void CalculateAutoTileID () {
    var sideValues = new StringBuilder ();
    foreach (Tile tile in neighbors) {
      sideValues.Append (tile == null ? "0": "1");
    }
    autoTileID = Convert.ToInt32 (sideValues.ToString (), 2);
  }
}