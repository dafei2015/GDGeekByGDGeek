using UnityEngine;
using System.Collections;

/// <summary>
/// 数据模型
/// </summary>
public class Model : MonoBehaviour
{
    public int width = 4;
    public int height = 8;
    public Cube[] _list = null;

    public Cube GetCube(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }
        int index = x + 4 * y;
        return _list[index];
    }
}
