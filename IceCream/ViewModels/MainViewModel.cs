using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IceCream.Models;
using Newtonsoft.Json;

namespace IceCream.ViewModels;

public class MainViewModel : BaseViewModel
{
    private string filePath;
    private string jsonData;

    private string listViewSelectedItem;
    private BackgroundMode _backgroundMode = 0;
    
    private string stationId;
    private DateTime date = DateTime.Now;
    private int target;
    private int actual;
    private int variance;
    private ObservableCollection<IceCreamModel> _iceCreams = new ObservableCollection<IceCreamModel>() { new IceCreamModel() {StationId = "123", Actual = 50, Date = DateTime.Now, Target = 34} };

    public MainViewModel()
    { 
        filePath = @"db.json"; 
        jsonData = System.IO.File.ReadAllText(filePath);
       IceCreams = JsonConvert.DeserializeObject<ObservableCollection<IceCreamModel>>(jsonData) 
            ?? new ObservableCollection<IceCreamModel>();
    }
    
    public BackgroundMode BackgroundMode
    {
        get => _backgroundMode;
        set => OnPropertyChanged(out _backgroundMode, value);
    }
    public string ListViewSelectedItem
    {
        get => listViewSelectedItem;
        set => OnPropertyChanged(out listViewSelectedItem, value);
    }
    public string StationId
    {
        get => stationId;
        set => OnPropertyChanged(out stationId, value);
    }
    public DateTime Date
    {
        get => date;
        set => OnPropertyChanged(out date, value);
    }
    public int Target
    {
        get => target;
        set => OnPropertyChanged(out target, value);
    }
    public int Actual
    {
        get => actual;
        set => OnPropertyChanged(out actual, value);
    }
    public int Variance
    {
        get => variance;
        set => OnPropertyChanged(out variance, value);
    }
    public ObservableCollection<IceCreamModel> IceCreams
    {
        get => _iceCreams;
        set => OnPropertyChanged(out _iceCreams, value);
    } 

    public void Clear()
    {
        StationId = string.Empty;
        Date = DateTime.Now;
        Target = 0;
        Actual = 0;
        Variance = 0;
    }

    public void SaveIceCreamStation()
    {
        var current = IceCreams.FirstOrDefault(x => x.StationId == ListViewSelectedItem);
        if (current == null)
        {
            if (StationId.Length == 0 && StationId == null)
                throw new Exception("Empty Station Id");
            IceCreams.Add(new IceCreamModel()
            {
                StationId = StationId,
                Date = Date,
                Target = Target,
                Actual = Actual
            });
        }
        else
        {
            IceCreams.Remove(current);
            current.StationId = StationId;
            current.Date = Date;
            current.Target = Target;
            current.Actual = Actual;
            IceCreams.Add(current);
        }
        
        jsonData = JsonConvert.SerializeObject(IceCreams);
        System.IO.File.WriteAllText(filePath, jsonData);
        Clear();
    }

    public void DeleteIceCreamStation()
    {
        if (ListViewSelectedItem == null)
            throw new Exception("Nothing chosen");
            
        IceCreams.Remove(IceCreams.FirstOrDefault(x => x.StationId == ListViewSelectedItem)!);
        jsonData = JsonConvert.SerializeObject(IceCreams);
        System.IO.File.WriteAllText(filePath, jsonData);
        Clear();
    }

    public void Select()
    {
        var current = IceCreams.FirstOrDefault(x => x.StationId == ListViewSelectedItem);
        if (current != null)
        {
            StationId = current.StationId;
            Date = current.Date;
            Target = current.Target;
            Actual = current.Actual;
        }
    }

    public int CalculateVariance()
    {
        Variance = Actual - Target;
        if (Variance >= (Target * 0.05)) return 1; // green
        else if (Variance <= -(Target * 0.1)) return 0; // red
        else return -1; // black
    }

    public int SwitchMode()
    {
        if ((int)_backgroundMode == 2)
        {
            _backgroundMode = 0;
            return (int)_backgroundMode;
        }
        else
        {
            return (int)++_backgroundMode;
        }
        
    }

    public void ListViewUnselection()
    {
        ListViewSelectedItem = null;
    }
    
}