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
    private List<float> weightList = new List<float>();
    public float topLeft;
    private List<float> topLeftList = new List<float>();
    public float topRight;
    private List<float> topRightList = new List<float>();
    public float bottomLeft;
    private List<float> bottomLeftList = new List<float>();
    public float bottomRight;
    private List<float> bottomRightList = new List<float>();


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

        AddList(float.Parse(datastr.Split('#')[0].Replace(',', '.')), ref weightList, 5);
        AddList(float.Parse(datastr.Split('#')[1].Replace(',', '.')), ref topLeftList, 5);
        AddList(float.Parse(datastr.Split('#')[2].Replace(',', '.')), ref topRightList,5);
        AddList(float.Parse(datastr.Split('#')[3].Replace(',', '.')), ref bottomLeftList,5);
        AddList(float.Parse(datastr.Split('#')[4].Replace(',', '.')), ref bottomRightList,5);

        weight = AverageList(ref weight, ref weightList);
        topLeft = AverageList(ref topLeft, ref topLeftList);
        topRight = AverageList(ref topRight, ref topRightList);
        bottomLeft = AverageList(ref bottomLeft, ref bottomLeftList);
        bottomRight = AverageList(ref bottomRight, ref bottomRightList);

        udpClient.BeginReceive(new System.AsyncCallback(OnMessageReiceived), null);
    }

    private void AddList(float var, ref List<float> varList, float sizeList)
    {
        if (varList.ToArray().Length == sizeList)
        {
            varList.RemoveAt(0);
        }
        varList.Add(var);
    }

    private float AverageList(ref float var, ref List<float> varList)
    {
        float sum = Sum(varList.ToArray());
        float result = sum / varList.ToArray().Length;
        return result;
    }

    public float Sum(float[] varArray)
    {
        float result = 0;

        for (int i = 0; i < varArray.Length; i++)
        {
            result += varArray[i];
        }

        return result;
    }

    private void OnDestroy()
    {
        udpClient.Close();
    }
}
