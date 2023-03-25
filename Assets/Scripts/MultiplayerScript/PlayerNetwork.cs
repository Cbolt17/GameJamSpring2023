using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    public NetworkVariable<bool> isReady = new NetworkVariable<bool>(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> itemType = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> itemNum = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> itemTarget = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Owner);

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
                player.item = new item
                {
                    type = itemType.Value,
                    num = itemNum.Value,
                    target = itemTarget.Value
                };
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
