using UnityEngine;

/// <summary>
/// Test script: logs "OKKK" when all rooms are generated.
/// </summary>
public class AllRoomsGeneratedTester : MonoBehaviour
{
    private void OnEnable()
    {
        AllRoomsGeneratedChecker.OnAllRoomsGenerated += HandleAllRoomsGenerated;
    }

    private void OnDisable()
    {
        AllRoomsGeneratedChecker.OnAllRoomsGenerated -= HandleAllRoomsGenerated;
    }

    private void HandleAllRoomsGenerated()
    {
        Debug.Log("OKKK");
    }
}
