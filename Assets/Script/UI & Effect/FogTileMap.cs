    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;

public class FogTileMap : MonoBehaviour
{
    [SerializeField] Tilemap fogTilemap;
    [SerializeField] float effectDuration = 0.15f;
    private List<TileBase> listTileFog;
    private void Start()
    {
        listTileFog = GetTilesFromTilemap(fogTilemap);
        SetColorForAllTiles(fogTilemap,Color.black);
        StartCoroutine(IEStartCleanFog());
    }
    List<TileBase> GetTilesFromTilemap(Tilemap tilemap)
    {
        List<TileBase> tiles = new List<TileBase>();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        foreach (TileBase tile in allTiles)
        {
            if (tile != null)
            {
                tiles.Add(tile);
            }
        }
        return tiles;
    }
    private IEnumerator IEStartCleanFog(){
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < listTileFog.Count; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                // SetTileColor(new Vector3Int(i,j));
                StartCoroutine(SetTileColor(new Vector3Int(i,j)));
                StartCoroutine(SetTileColor(new Vector3Int(-i,j)));
                StartCoroutine(SetTileColor(new Vector3Int(i,-j)));
                StartCoroutine(SetTileColor(new Vector3Int(-i,-j)));
                StartCoroutine(SetTileColor(new Vector3Int(j,i)));
                StartCoroutine(SetTileColor(new Vector3Int(-j,i)));
                StartCoroutine(SetTileColor(new Vector3Int(j,-i)));
                StartCoroutine(SetTileColor(new Vector3Int(-j,-i)));
            }
            yield return new WaitForSeconds(effectDuration);
        }
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    private IEnumerator SetTileColor(Vector3Int position)
    {
        fogTilemap.SetTileFlags(position,TileFlags.None);
        float timeElapse = 0;
        while (timeElapse < effectDuration)
        {
            timeElapse += Time.deltaTime;
            if (timeElapse > effectDuration) timeElapse = effectDuration;
            fogTilemap.SetColor(position, new Color(1 * timeElapse / effectDuration, 1 * timeElapse / effectDuration, 1 * timeElapse / effectDuration, 1));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        timeElapse = 0;
        while (timeElapse < effectDuration)
        {
            timeElapse += Time.deltaTime;
            if (timeElapse > effectDuration) timeElapse = effectDuration;
            fogTilemap.SetColor(position, new Color(1, 1, 1, 1 * (1 - timeElapse / effectDuration)));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    void SetColorForAllTiles(Tilemap tilemap, Color color)
    {
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int position = new Vector3Int(x, y);
                if (tilemap.HasTile(position))
                {
                    tilemap.SetTileFlags(position, TileFlags.None);
                    tilemap.SetColor(position, color);
                }
            }
        }
    }
}
