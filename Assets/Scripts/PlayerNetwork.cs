using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using Enums;

public class PlayerNetwork : NetworkBehaviour
{
    public NetworkVariable<bool> isReady = new NetworkVariable<bool>(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<Type> itemType = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Owner);
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
            if(player.isReady)
            {
                player.itemID = (Tuple<Type, int>) { itemType, itemNum };
            }
        }
        else
        {
            isReady.Value = player.isReady;
            if(player.isReady)
            {
                itemType.Value = player.item.type;
                itemNum.Value = player.item.num;
                itemTarget.Value = player.item.target;
            }
        }
    }
}
