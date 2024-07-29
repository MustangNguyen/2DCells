using UnityEngine;
public class GameStateWin : GameState{
    public MapNodeInformation nodeInformation;
    public int nodeScore;
    public GameStateWin(MapNodeInformation nodeInformation, int nodeScore){
        this.nodeInformation = nodeInformation;
        this.nodeScore = nodeScore;
    }
    public override void Enter()
    {
        base.Enter();
        EnemySpawner.Instance.isSpawning = false;
        UpdateManager.Instance.DestroyAllCell();
        PopupWin.Show();
        NodeProcessOOP nodeProcessOOP = new NodeProcessOOP{
            userId = DataManager.Instance.UserData.userInformation.userID,
            nodeId = this.nodeInformation.nodeId,
            isNodeFinish = true,
            nodeScore = this.nodeScore
        };

        NetworkManager.Instance.PostUpdateUserProcess(nodeProcessOOP,(data)=>{
            Debug.Log("node update");
            int nodeCounter = nodeInformation.nextNode.Count;
            if (nodeCounter > 0)
                foreach (var nextNode in nodeInformation.nextNode)
                {
                    nodeCounter--;
                    NodeProcessOOP nodeProcessOOPNextNode = new NodeProcessOOP
                    {
                        userId = DataManager.Instance.UserData.userInformation.userID,
                        nodeId = nextNode.nodeId,
                        isNodeFinish = false,
                        nodeScore = GameManager.Instance.score + GameManager.Instance.TotalCurrentXp() + 10000
                    };
                    Debug.Log("next nodeid: " + nextNode.nodeId);
                    NetworkManager.Instance.PostUpdateUserProcess(nodeProcessOOPNextNode, (data) =>
                    {
                        if (nodeCounter <= 0)
                            NetworkManager.Instance.GetUserInformationFromServer(DataManager.Instance.UserData.userInformation.email);
                        Debug.Log("Updated next nodes!");
                    }, (data) =>
                    {
                        Debug.LogError("Updated next nodes false!");
                    });
                }
            else
                NetworkManager.Instance.GetUserInformationFromServer(DataManager.Instance.UserData.userInformation.email);
        }, (data) =>
        {
            Debug.LogWarning(data);
        });
    }
    public override void LogicUpdate(){

    }
}