
//これはコードで自動生成されます
//みんなが使用できるイベントのみが表示されます
namespace VTNConnect
{
    public enum EventDefine
    {
        DeathTrap = 102,  //死んだらおもちゃの熊が落ちてくる (ToyBox -> VCMain、GameOnly)
        ActorEffect = 105,  //アクターにランダム効果 (すべてのゲーム -> Confront、GameOnly)
        DarkRoom = 107,  //照明の光度が一定時間低下する。 (すべてのゲーム -> Confront、GameOnly)
        SummonEnemy = 109,  //プレイヤーの頭上から雑魚敵が降ってくる (すべてのゲーム -> Confront、GameOnly)
        DeathScream = 110,  //冒険者が死ぬと悲鳴をだす (VCMain -> ToyBox、GameOnly)
        UserTalk = 114,  //台詞が冒険者に共有される。 (ToyBox -> VCMain、GameOnly)
        KnockWindow = 116,  //窓をたたく音を出す (すべてのゲーム -> ToyBox、GameOnly)
        EnemyEscape = 130,  //敵が逃げた (SampleGame -> すべてのゲーム、GameOnly)
        Cheer = 1001,  //おうえんメッセージ (バンコネシステム -> すべてのゲーム、ALL)
        BonusCoin = 1002,  //コイン増える (バンコネシステム -> すべてのゲーム、ALL)
        Levelup = 1003,  //レベルが上がった (バンコネシステム -> すべてのゲーム、ALL)
        GetArtifact = 1004,  //アーティファクトを獲得 (バンコネシステム -> すべてのゲーム、ALL)
        GameStart = 1006,  //冒険が始まった (バンコネシステム -> すべてのゲーム、ALL)
        GameEnd = 1007,  //冒険が終わった (バンコネシステム -> すべてのゲーム、ALL)

    }
}
