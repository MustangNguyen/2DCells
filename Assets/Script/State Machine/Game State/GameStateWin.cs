using UnityEngine;
public class GameStateWin : GameState{
    public string nodeId;
    public int nodeScore;
    public GameStateWin(string nodeId, int nodeScore){
        this.nodeId = nodeId;
        this.nodeScore = nodeScore;
    }
    public override void Enter()
    {
        base.Enter();
        NodeProcessOOP nodeProcessOOP = new NodeProcessOOP{
            userId = DataManager.Instance.UserData.userInformation.userID,
            nodeId = this.nodeId,
            isFinish = true,
            nodeScore = this.nodeScore
        };
        NetworkManager.Instance.PostUpdateUserProcess(nodeProcessOOP,(data)=>{
            Debug.Log("node update");
            NetworkManager.Instance.GetPlanetsFromServer();
        },(data)=>{
            Debug.LogWarning(data);
        });
    }
    public override void LogicUpdate(){

    }
}