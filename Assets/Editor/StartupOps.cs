using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class StartupOps
{
    static StartupOps() {
        /*if (!SessionState.GetBool("StartUpDone",false)) {
            SessionState.SetBool("StartUpDone",true);
            Debug.Log("Now Starting Up...");
            AgentController.GetInstance().RegisterAgents();
        }*/
    }
}
