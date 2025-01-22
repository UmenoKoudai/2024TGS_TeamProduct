namespace VTNConnect
{
    /// <summary>
    /// 環境系DI用のインタフェース
    /// </summary>
    public interface IEnvironment
    {
        //APIの接続先
        public string APIServerURI { get; }
    }
}