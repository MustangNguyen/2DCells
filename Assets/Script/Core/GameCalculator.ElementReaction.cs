using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameCalculator : MonoBehaviour
{
    public static StatusState ElementReact(StatusState currentStatus, StatusState incomeStatus){
        StatusState resultStatus = new();
        Debug.Log("element react system still work");
        switch(currentStatus.primaryElement){
            case PrimaryElement.Fire:
                Debug.Log("Curren status is burn");
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Fire:
                        Debug.Log("Why only Fire?");
                        resultStatus = incomeStatus;
                    break;
                    case PrimaryElement.Toxin:
                        Debug.Log("Fire + Toxin");
                        resultStatus = new StatusStateHellBurn(currentStatus.enemyCell,currentStatus.damagePerTick,currentStatus.stack);
                    break;
                }
            break;
            case PrimaryElement.Ice:
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Ice:
                        resultStatus = incomeStatus;
                    break;
                }
            break;
            case PrimaryElement.Electric:
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Electric:
                        resultStatus = incomeStatus;
                    break;
                }
            break;
            case PrimaryElement.Toxin:
                Debug.Log("Curren status is poisonous");
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Toxin:
                        Debug.Log("Why only Toxin?");
                        resultStatus = incomeStatus;
                    break;
                    case PrimaryElement.Fire:
                        Debug.Log("Toxin + Fire");
                        resultStatus = new StatusStateBlast(currentStatus.enemyCell,currentStatus.damagePerTick,currentStatus.stack);
                    break;
                }
            break;
            case PrimaryElement.None:
                Debug.Log("Have not had status yet");
                resultStatus = incomeStatus;
            break;
            default:
                resultStatus = incomeStatus;
                Debug.Log("WTF why?");
            break;
        }
        return resultStatus;
    }
}
