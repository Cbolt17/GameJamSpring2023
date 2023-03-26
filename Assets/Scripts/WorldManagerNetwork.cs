using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;
using System;

public class WorldManagerNetwork : NetworkBehaviour
{
    public bool isTheServer = false;
    public NetworkVariable<float> responseTimeRemaining = new NetworkVariable<float>(writePerm: NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> allResponsesIn = new NetworkVariable<bool>(writePerm: NetworkVariableWritePermission.Server);
    public NetworkVariable<int> responsesRemaining = new NetworkVariable<int>();

    private WorldManager worldManager;

    private void Start()
    {
        worldManager = WorldManager.instance;
        allResponsesIn.Value = true;
    }

    private void Update()
    {
        if (isTheServer)
        {
            allResponsesIn.Value = worldManager.allResponsesIn;
            responsesRemaining.Value = worldManager.responsesRemaining;
            responseTimeRemaining.Value = worldManager.responseTimeRemaining;
        }
        else
        {
            worldManager.allResponsesIn = allResponsesIn.Value;
            worldManager.responsesRemaining = responsesRemaining.Value;
            worldManager.responseTimeRemaining = responseTimeRemaining.Value;
        }
    }
}