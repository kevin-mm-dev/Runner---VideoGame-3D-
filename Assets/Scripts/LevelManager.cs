using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {set; get;}
    public List<GameObject> tiles=new List<GameObject>();
    public Transform playerTransform;
    float spawnZ=0.0f;
    float tileLength=4f;
    int annTiles=20;
    float safeZone=15.0f;
    int lastTileIndex;

    private List<GameObject> activeTiles;



    // Start is called before the first frame update
    void Start()
    {
        // playerTransform =GameObject.FindGameObjectWithTag("Player").transform;
        activeTiles=new List<GameObject>();
        for (int i = 0; i < annTiles; i++){
            // if (i<2)
            // {
                
            // SpawnTile(0);
            // }else{
                SpawnTile(0);

            // }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Comparando pos Player: "+(playerTransform.position.z - safeZone));
        Debug.Log("Con posicion spawn: "+(spawnZ-annTiles*tileLength));
        
        if (playerTransform.position.z - safeZone > (spawnZ-annTiles*tileLength)){
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile(int prefabIndex=-1){
        GameObject go;
        if (prefabIndex==-1){
            go=Instantiate(tiles[RandomTileIndex()] as GameObject);
        }else{
            go=Instantiate(tiles[prefabIndex] as GameObject);
        }
        go.transform.SetParent(transform);
        go.transform.position=new Vector3///// Aqui se podría usar un Vector3.Forward que es igual a (0,0,1)
        (1.45f,          //////   Pero se hizo así para que el personaje quede centrado en el bloque de mapa 
        transform.position.y,
        1.0f*spawnZ);
        // go.transform.position.x=go.transform.position.x+4.5f;
        spawnZ+=tileLength;
        activeTiles.Add(go);
    }

    void DeleteTile(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomTileIndex(){
        if (tiles.Count<=1)
        {
            return 0;
        }
        int randomIndex=lastTileIndex;
        while (randomIndex==lastTileIndex){
            randomIndex=Random.Range(0,tiles.Count);
        }
        lastTileIndex=randomIndex;
        return randomIndex;
    }
}
