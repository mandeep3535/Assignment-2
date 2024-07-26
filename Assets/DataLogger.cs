using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    public string filename = "";
    public string participant_id = "";
    public enum Device { mouse , touchpad};
    public Device device;

    public enum Width { W_halfPoint, W_onePointtwofive , W_two };
    public Width width;

    public enum Distance { D_three, D_six, D_ten };
    public Distance distance;

    public float time = 0.0f;
    public float Finaltime = 0.0f;
    public int wrong = 0;
    string updatedWidth = "";
    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/" + participant_id + ".csv";
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void WriteCSV()
    {
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine("" + participant_id + ","+ device + ","+ width + ","+ distance +"," + Finaltime+","+ wrong);
        tw.Close();
        time = 0;
        Finaltime = 0;
        wrong = 0;
    }
}
