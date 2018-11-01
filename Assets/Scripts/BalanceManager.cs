using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WiimoteLib;
using System.Text.RegularExpressions;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Globalization;

public class BalanceManager : MonoBehaviour {

    UdpClient udpClient;
    private string datastr;

    public float weight;
    public float topLeft;
    public float topRight;
    public float bottomLeft;
    public float bottomRight;


    private void Start()
    {
        udpClient = new UdpClient(4000);
        try
        {
            print("listening to port 4000");
            udpClient.BeginReceive(new System.AsyncCallback(OnMessageReiceived), null);
        }
        catch (System.Exception e)
        {
            print("error " + e.Message);
        }
    }

    

    void OnMessageReiceived(System.IAsyncResult res)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 4000);
        byte[] bytedata = udpClient.EndReceive(res, ref endPoint);
        datastr = Encoding.ASCII.GetString(bytedata, 0, bytedata.Length);

        weight = float.Parse(datastr.Split('#')[0].Replace(',', '.'));
        topLeft = float.Parse(datastr.Split('#')[1].Replace(',', '.'));
        topRight = float.Parse(datastr.Split('#')[2].Replace(',', '.'));
        bottomLeft = float.Parse(datastr.Split('#')[3].Replace(',', '.'));
        bottomRight = float.Parse(datastr.Split('#')[4].Replace(',', '.'));

        udpClient.BeginReceive(new System.AsyncCallback(OnMessageReiceived), null);
    }

    private void OnDestroy()
    {
        udpClient.Close();
    }
}
