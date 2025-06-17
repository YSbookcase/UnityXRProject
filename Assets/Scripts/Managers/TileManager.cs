using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class TileManager : MonoBehaviour
{
    [SerializeField] private float cellSize = 3f;

    private Dictionary<Vector2Int, Tile> tileMap = new();

    /// <summary>
    /// Ÿ�� ��� (Tile.cs���� ȣ��)
    /// </summary>
    public void RegisterTile(Tile tile)
    {
        if (tile == null)
        {
            Debug.LogError("����Ϸ��� Ÿ���� null�Դϴ�.");
            return;
        }

        Vector2Int gridPos = WorldToGrid(tile.transform.position);
        if (!tileMap.ContainsKey(gridPos))
        {
            tileMap.Add(gridPos, tile);
        }
        else
        {
            Debug.LogWarning($"�̹� ��ϵ� Ÿ��: {gridPos}");
        }
    }

    /// <summary>
    /// ���� ��ǥ�κ��� �ش� Ÿ���� ������
    /// </summary>
    public Tile GetTileAt(Vector3 worldPosition)
    {
        Vector2Int gridPos = WorldToGrid(worldPosition);
        tileMap.TryGetValue(gridPos, out Tile tile);
        return tile;
    }

    /// <summary>
    /// �׸��� ��ǥ ��ȯ (x, z ����)
    /// </summary>
    private Vector2Int WorldToGrid(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x / cellSize);
        int z = Mathf.RoundToInt(pos.z / cellSize);
        return new Vector2Int(x, z);
    }

    /// <summary>
    /// ��� Ÿ�� ��ȯ (���� ���� ǥ�� � ��� ����)
    /// </summary>
    public IEnumerable<Tile> GetAllTiles() => tileMap.Values;
}
