using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameCalculator : MonoBehaviour
{
    public static StatusState ElementReact(StatusState currentStatus, StatusState incomeStatus){
        StatusState resultStatus = new(incomeStatus);
        switch(currentStatus.primaryElement){
            case PrimaryElement.Fire:
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Fire:
                        resultStatus = incomeStatus;
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
                    case PrimaryElement.Ice:
                        resultStatus = incomeStatus;
                    break;
                }
            break;
            case PrimaryElement.Toxin:
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Ice:
                        resultStatus = incomeStatus;
                    break;
                }
            break;
            case PrimaryElement.None:
                resultStatus = incomeStatus;
            break;
        }
        return resultStatus;
    }
}
