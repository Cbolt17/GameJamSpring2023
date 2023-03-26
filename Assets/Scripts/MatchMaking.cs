using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class MatchMaking : MonoBehaviour
{
    public string joinCode;
    public int maxPlayers = 5;

    private WorldManager worldManager;
    private UnityTransport transport;

    private async void Awake()
    {
        transport = FindObjectOfType<UnityTransport>();

        await Authenticate();
    }

    private void Start()
    {
        worldManager = WorldManager.instance;
    }

    private static async Task Authenticate()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateGame()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
        joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
        NetworkManager.Singleton.StartHost();
        GiveWorldManagerJoinCode(joinCode);
    }

    public async void JoinGame(string joinCode)
    {
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);

        NetworkManager.Singleton.StartClient();
        GiveWorldManagerJoinCode(joinCode);
    }

    private void GiveWorldManagerJoinCode(string joinCode)
    {
        worldManager.joinCode.text = joinCode;
    }
}
