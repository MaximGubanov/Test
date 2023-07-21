using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace GasNetwork.Views;

public partial class ConsumptionView : UserControl
{
    public ConsumptionView()
    {
        InitializeComponent();
        
        YearsList.Items = new List<DateTime>(CreateYearsList());
        RangeSampling.Items = new List<Consumption>(CreateButtonList());
    }

    private List<DateTime> CreateYearsList()
    {
        List<DateTime> yearsList = new();
        int endYear = 2001;
        
        for (int year = DateTime.Now.Year; year >= endYear; year--)
        {
            yearsList.Add(new DateTime(year, 1, 1));
        }

        return yearsList;
    }

    private List<Consumption> CreateButtonList()
    {
        return new List<Consumption>
        {
            new Consumption(){ Title = "Годы"},
            new Consumption(){ Title = "Месяцы"},
            new Consumption(){ Title = "Сутки"},
            new Consumption(){ Title = "Часы"},
        };
    }

    //TODO: вынести этот класс в models
    class Consumption
    {
        public string Title { get; set; }
    }
}