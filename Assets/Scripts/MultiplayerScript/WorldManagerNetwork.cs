using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;

public class WorldManagerNetwork : NetworkBehaviour
{
    public bool isTheServer = false;
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
        }
        else
        {
            worldManager.allResponsesIn = allResponsesIn.Value;
            worldManager.responsesRemaining = responsesRemaining.Value;
        }
    }
}


//https://www.youtube.com/watch?v=fdkvm21Y0xE just a link for Cole's convienience;