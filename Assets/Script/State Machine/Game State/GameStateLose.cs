using UnityEngine;
public class GameStateLose : GameState{
    public MapNodeInformation nodeInformation;
    public GameStateLose(MapNodeInformation nodeInformation){
        this.nodeInformation = nodeInformation;
    }
    public override void Enter()
    {
        base.Enter();
        EnemySpawner.Instance.isSpawning = false;
        PopupLoseWin.Show(false);
        NodeProcessOOP nodeProcessOOP = new NodeProcessOOP{
            userId = DataManager.Instance.UserData.userInformation.userID,
            nodeId = this.nodeInformation.nodeId,
            isNodeFinish = false,
            nodeScore = GameManager.Instance.score + GameManager.Instance.TotalCurrentXp()
        };

        NetworkManager.Instance.PostUpdateUserProcess(nodeProcessOOP,(data)=>{
            Debug.Log("node update");
            int nodeCounter = nodeInformation.nextNode.Count;
            NetworkManager.Instance.GetUserInformationFromServer(DataManager.Instance.UserData.userInformation.email);
        }, (data) =>
        {
            Debug.LogWarning(data);
        });
    }
    public override void LogicUpdate(){

    }
}