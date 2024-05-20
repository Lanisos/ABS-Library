using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItems
{
    
    [MenuItem("Assets/Create/New Agent",false,2)]
    private static void createAgentMenuItem() {
        AgentCreationWindow.ShowWindow();
    }

    [MenuItem("Assets/Create/New Environment",false,2)]
    private static void createEnvironmentMenuItem() {
        EnvironmentCreationWindow.ShowWindow();
    }

    [MenuItem("Assets/Create/New Item",false,2)]
    private static void createItemMenuItem() {
        ItemCreationWindow.ShowWindow();
    }

    [MenuItem("Assets/Delete/Delete Agent",false,7)]
    private static void deleteAgentMenuItem() {
        AgentDestructionWindow.ShowWindow();
    }

    [MenuItem("Assets/Delete/Delete Environment",false,7)]
    private static void deleteEnvironmentMenuItem() {
        EnvironmentDestructionWindow.ShowWindow();
    }

    [MenuItem("Assets/Delete/Delete Item",false,7)]
    private static void deleteItemMenuItem() {
        ItemDestructionWindow.ShowWindow();
    }

    [MenuItem("Help/ABS Library Manual",false,7)]
    private static void openManual() {
        
    }
}
