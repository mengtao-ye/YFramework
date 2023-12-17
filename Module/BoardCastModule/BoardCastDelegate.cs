namespace YFramework
{
    ///封装里了系统所使用到的委托
    ///定义委托的类
    ///定义多少参数都是可以的
    ///定义了多少委托，EventCenter添加多少委托，过多添加会报错

    /// <summary>
    /// 无参委托
    /// </summary>
    public delegate void CallBack();


    /// <summary>
    /// 有参委托,一个参数,需要指定一个泛型，（ 泛型类型 变量名 ）
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="arg">变量名</param>
    public delegate void CallBack<T>(T arg);


    /// <summary>
    /// 多个参数
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <typeparam name="X">泛型类型</typeparam>
    /// <param name="arg1">变量名</param>
    /// <param name="arg2">变量名</param>
    /// 一般只用到5个，如果还需要可以往后加
    public delegate void CallBack<T, X>(T arg1, X arg2);
    public delegate void CallBack<T, X, Y>(T arg1, X arg2, Y arg3);
    public delegate void CallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);
    public delegate void CallBack<T, X, Y, Z, W>(T arg1, X arg2, Y arg3, Z arg4, W arg5); 
}