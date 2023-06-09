using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using Enums;
using System;

public class PlayerNetwork : NetworkBehaviour
{
    public NetworkVariable<bool> isReady = new NetworkVariable<bool>(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> itemType = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> itemNum = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Owner);

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!IsOwner)
        {
            player.isReady = isReady.Value;
            if (player.isReady)
            {

                player.itemID = new((Enums.Type)itemType.Value, itemNum.Value);
            }
        }
        else
        {
            isReady.Value = player.isReady;
            if (player.isReady)
            {
                itemType.Value = (int)player.itemID.Item1;
                itemNum.Value = player.itemID.Item2;
                //itemTarget.Value = player.item.target;
            }
        }
    }
}