/// <summary>
/// 扩展，必须是静态类&静态方法。
/// </summary>

public static class ArrayExtend  
{
    /// <summary>
    /// 返回数组中第一个n的索引
    /// </summary>
    /// <param name="array">数组</param>
    /// <param name="n">待查找数字</param>
    /// <returns>没找到返回-1</returns>
    public static int FirstIndexof<T>(this T[] array,T n)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(n)) return i;
        }
        return -1;
    }
}
