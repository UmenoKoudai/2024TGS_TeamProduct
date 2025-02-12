namespace VTNConnect
{
    /// <summary>
    /// ローカル環境
    /// </summary>
    public class LocalEnvironment : IEnvironment
    {
        public string APIServerURI => "http://localhost:4649";
    }
}