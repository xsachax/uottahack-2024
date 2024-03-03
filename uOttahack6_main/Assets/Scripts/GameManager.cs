using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using Mirror.Discovery;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    public VRNetworkDiscovery networkDiscovery;

    private void Start()
    {
        //NetworkManager.singleton.networkAddress = "192.168.117.44";
        if (networkDiscovery == null)
        { networkDiscovery = GameObject.FindObjectOfType<VRNetworkDiscovery>(); }
    }
    void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        NetworkManager.singleton.StartClient(info.uri);
    }
    
    public void OnDiscoveredServer(ServerResponse info)
    {
        discoveredServers[info.serverId] = info;
        Connect(info);
    }
    public void StartAsHost(string name, string ip)
    {
        VRStaticVariables.playerName = name;
        NetworkManager.singleton.networkAddress = ip;
        discoveredServers.Clear();
        NetworkManager.singleton.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    public void JoinAsClient(string name, string ip)
    {
        VRStaticVariables.playerName = name;
        NetworkManager.singleton.networkAddress = ip;
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
    }

    public void StopServer()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }
        // stop server if server-only
        else if (NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
        }
        networkDiscovery.StopDiscovery();

    }
    
    public void ResetGame()
    {
        StopServer();
        // turn off scene stuff
    }
}

