namespace VTNConnect
{
    /// <summary>
    /// 本番/開発環境
    /// </summary>
    public class ProductionEnvironment : IEnvironment
    {
        public string APIServerURI => "https://vc.vtn-game.com/vc";
    }
}