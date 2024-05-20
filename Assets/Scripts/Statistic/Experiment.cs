using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

public class Experiment {
    private string id;
    private bool export_process;

    private static string sheet_id = "158sbnl45Urnkxmr6bnmALnQX4lMMGXIuJDNRGucT-E8";
    //private static string user_credentials = "/Scripts/abs-library-1363b8e6c9de.json";
    private static SheetsService service;

    public Experiment() {
        export_process = false;
        /*string full_path = Application.dataPath + user_credentials;
        Stream json = (Stream)File.Open(full_path,FileMode.Open);
        ServiceAccountCredential credentials = ServiceAccountCredential.FromServiceAccountData(json);
        service = new SheetsService(new BaseClientService.Initializer(){
            HttpClientInitializer = credentials,
        });*/
    }

    public void ExportStatisticsCSV<T>(string model_name, List<Statistic<T>> stats) {
        Debug.Log("I start Exporting");
        TextWriter tw;
        if (!export_process) {
            id = DateTime.Now.ToString("MMddyyyyHHmmss");
            export_process = true;
            tw = new StreamWriter(Application.dataPath+"/"+model_name+id+".csv",false);
        }
        else tw = new StreamWriter(Application.dataPath+"/"+model_name+id+".csv",true);
        string names = "";
        foreach (Statistic<T> stat in stats) names += "Time stamp"+", "+stat.GetName()+", ";
        tw.WriteLine(names);
        tw.Close();
        tw = new StreamWriter(Application.dataPath+"/"+model_name+id+".csv",true);
        for (int i = 0; i < stats[0].GetLenght(); ++i) {
            string values = "";
            foreach (Statistic<T> stat in stats) {
                values += stat.GetStampAt(i).ToString().Replace(",",".")+", "+stat.GetValueAt(i).ToString().Replace(",",".")+", ";
            }
            tw.WriteLine(values);
        }
        tw.Close();
    }

    public void FinishExporting() {
        export_process = false;
        /*UserCredential credential;
        using (var stream = new FileStream("../abs-library-1363b8e6c9de.json", FileMode.Open, FileAccess.Read)) {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                    new[] {  },
                    "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));
            )
        }*/
        /*Sheet sheet = new Sheet();
        sheet.Properties = new SheetProperties();
        sheet.Properties.Title = id;
        Spreadsheet req1 = service.Spreadsheets.Get(sheet_id).Execute();*/
        //SpreadsheetsResource.CreateRequest req2 = service.Spreadsheets.BatchUpdate();
    }
}