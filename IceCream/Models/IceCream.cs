using System;

namespace IceCream.Models;

public class IceCreamModel
{
    public string StationId { get; set; }
    public DateTime Date { get; set; }
    public int Target { get; set; }
    public int Actual { get; set; }

    public override string ToString()
    {
        return StationId;
    }
}