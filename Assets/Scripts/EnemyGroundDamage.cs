using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyGroundDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision){
        string objectTag=collision.gameObject.tag;
        
        if(objectTag=="Player"){
            collision.gameObject.GetComponent<PlayerLive>().SubstractLives();
            // ContactPoint2D[] contacts= new ContactPoint2D[2];
            // collision.GetContacts(contacts);
            // Tilemap grid=GetComponent<Tilemap>();
            // Vector3Int cellPosition=grid.layoutGrid.WorldToCell(contacts[0].point);
            // Debug.Log((cellPosition));
            ///GameObject cell=grid.GetInstantiatedObject(cellPosition);
            // TileBase cellTile=grid.GetTile(cellPosition);
            // if(cellTile!=null){
            //     damageTile
            // }
            // Debug.Log(cellTile);

        }else if(objectTag=="Enemy"){
            collision.gameObject.GetComponent<EnemyBehavior>().enemySR.flipX=!collision.gameObject.GetComponent<EnemyBehavior>().enemySR.flipX;
            collision.gameObject.GetComponent<EnemyBehavior>().speed*=-1;
        }

    }
    // public class damageTile:TileBase {
    //     public int rotation=0;
    //     public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    //     {
    //         Quaternion rot=Quaternion.Euler(0f, 0f, rotation);
    //         tileData.transform=Matrix4x4.TRS(Vector3.zero, rot, Vector3.one);
    //     }
    // }
}
