
//これはコードで自動生成されます
//みんなが使用できるイベントのみが表示されます
namespace VTNConnect
{
    public enum EventDefine
    {
        DemonUI = 101,  //岩垂UIの憑依 (BossBattle2D -> すべてのゲーム、対象のみ)
        DeathTrap = 102,  //死んだら罠を出す (ToyBox -> VCMain、対象のみ)
        DeathStack = 103,  //死ぬと死体が増える (BossBattle2D -> ToyBox、対象のみ)
        ActorEffect = 105,  //アクターにランダム効果 (すべてのゲーム -> Confront、対象のみ)
        ReviveGimmick = 106,  //ステージギミック復活(罠系) (すべてのゲーム -> Confront、対象のみ)
        DarkRoom = 107,  //照明の光度が一定時間低下する。 (すべてのゲーム -> Confront、対象のみ)
        SummonEnemy = 109,  //プレイヤーの頭上から雑魚敵が降ってくる (すべてのゲーム -> Confront、対象のみ)
        DeathScream = 110,  //冒険者が死ぬと悲鳴をだす (VCMain -> ToyBox、対象のみ)
        Cheer = 1001,  //おうえんメッセージ (バンコネシステム -> すべてのゲーム、対象のみ)
        BonusCoin = 1002,  //コイン増える (バンコネシステム -> すべてのゲーム、自分のみ)

    }
}
