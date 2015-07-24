using UnityEngine;
using System.Collections;
using GDGeek;

/// <summary>
/// 管理所有的小方块
/// </summary>
public class Play : MonoBehaviour
{
    private Square[] _list;
    public Square origin;
    void Awake()
    {
        _list = transform.GetComponentsInChildren<Square>();
    }
	void Start () 
	{
	    //在开始的时候讲所有方块隐藏
	    foreach (Square square in _list)
	    {
	        square.Hide();
	    }
	}

    /// <summary>
    /// 获得指定位置的方块
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Square GetSquare(int x, int y)
    {
        int index = x + 4 * y;
        return _list[index];
    }

    /// <summary>
    /// 移动坐标位置
    /// </summary>
    /// <param name="number">方块中的数字</param>
    /// <param name="begin">移动的开始位置</param>
    /// <param name="end">移动后的结束位置</param>
    /// <returns></returns>
    public GDGeek.Task MoveTask(int number, Vector2 begin, Vector2 end)
    {
        
        Square temp = (Square)Instantiate(origin);
        Square b = this.GetSquare((int) begin.x, (int) begin.y);
        Square e = this.GetSquare((int) end.x, (int) end.y);

        temp.transform.parent = b.transform.parent;
        temp.transform.localScale = b.transform.localScale;
        temp.transform.localPosition = b.transform.localPosition;

        temp.Show();
        temp.Number = number.ToString();
        b.Hide();           //将开始的隐藏

        //利用缓动任务将此运动加入该任务中
        TweenTask tt = new TweenTask(delegate {
            return TweenLocalPosition.Begin(temp.gameObject, 0.5f, e.transform.localPosition);
        });

        TaskManager.PushBack(tt, () => { Destroy(temp.gameObject); });
        return tt;
    }
}
