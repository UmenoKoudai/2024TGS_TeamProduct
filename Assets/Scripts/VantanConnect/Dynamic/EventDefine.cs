
//これはコードで自動生成されます
//みんなが使用できるイベントのみが表示されます
namespace VTNConnect
{
    public enum EventDefine
    {
        DeathTrap = 102,  //死んだらおもちゃの熊が落ちてくる (ToyBox -> VCMain、GameOnly)
        JengaInfo = 104,  //ジェンガ1本抜くと材質に応じた何かが起きる (Jenga -> VCMain、GameOnly)
        ActorEffect = 105,  //アクターにランダム効果 (すべてのゲーム -> Confront、GameOnly)
        DarkRoom = 107,  //照明の光度が一定時間低下する。 (すべてのゲーム -> Confront、GameOnly)
        PickupItem = 108,  //冒険者がアイテムを取ると… (VCMain -> Confront、GameOnly)
        SummonEnemy = 109,  //プレイヤーの頭上から雑魚敵が降ってくる (すべてのゲーム -> Confront、GameOnly)
        DeathScream = 110,  //冒険者が死ぬと悲鳴をだす (VCMain -> ToyBox、GameOnly)
        DefeatBomb = 111,  //敵を倒すと弾幕を消すボム的なものが出る (VCMain -> BossBattle2D、GameOnly)
        BadJengaInfo = 112,  //ジェンガを倒すと何か起こる(ダメージ) (Jenga -> VCMain、GameOnly)
        SummonHeliCopter = 113,  //ヘリが落ちる (BossBattle2D -> VCMain、GameOnly)
        UserTalk = 114,  //台詞が冒険者に共有される。 (ToyBox -> VCMain、GameOnly)
        ConfrontHeal = 115,  //アイテムを取ると冒険者が回復する。 (Confront -> VCMain、GameOnly)
        KnockWindow = 116,  //窓をたたく音を出す (すべてのゲーム -> ToyBox、GameOnly)
        EnemyEscape = 130,  //敵が逃げた (SampleGame -> すべてのゲーム、GameOnly)
        Cheer = 1001,  //おうえんメッセージ (バンコネシステム -> すべてのゲーム、ALL)
        BonusCoin = 1002,  //コイン増える (バンコネシステム -> すべてのゲーム、ALL)
        Levelup = 1003,  //レベルが上がった (バンコネシステム -> すべてのゲーム、ALL)
        GetArtifact = 1004,  //アーティファクトを獲得 (バンコネシステム -> すべてのゲーム、ALL)
        FieldEvent01 = 1010,  //会場イベント1 (バンコネシステム -> VCMain、ALL)
        FieldEvent02 = 1011,  //会場イベント2 (バンコネシステム -> VCMain、ALL)
        FieldEvent03 = 1012,  //会場イベント3 (バンコネシステム -> VCMain、ALL)
        FieldEvent04 = 1013,  //会場イベント4 (バンコネシステム -> VCMain、ALL)
        FieldEvent05 = 1014,  //会場イベント5 (バンコネシステム -> VCMain、ALL)
        Artifact01 = 1100,  //アーティファクト出現(1) (バンコネシステム -> VCMain、ALL)
        Artifact02 = 1101,  //アーティファクト出現(2) (バンコネシステム -> VCMain、ALL)
        Artifact03 = 1102,  //アーティファクト出現(3) (バンコネシステム -> VCMain、ALL)

    }
}
