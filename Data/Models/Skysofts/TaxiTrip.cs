using ntgroup.Extensions;

namespace ntgroup.Data.Models.Skysofts;

public class TaxiTrip
{
    public int? _id { get; set; }
    public string vehicleID { get; set; } = string.Empty;
    public string state { get; set; } = string.Empty;
    public string distance { get; set; } = string.Empty;
    public string km { get; set; } = string.Empty;
    public string emptyKm { get; set; } = string.Empty;
    public decimal charge { get; set; }
    public decimal realCharge { get; set; }
    public decimal waitCharge { get; set; }
    public string waitTime { get; set; } = string.Empty;
    public decimal unitPrice { get; set; }
    public string irState { get; set; } = string.Empty;
    public string userName { get; set; } = string.Empty;
    public string typeID { get; set; } = string.Empty;
    public string tripID { get; set; } = string.Empty;
    public string token { get; set; } = string.Empty;
    public string pickupX { get; set; } = string.Empty;
    public string pickupY { get; set; } = string.Empty;
    public string dropOffX { get; set; } = string.Empty;
    public string dropOffY { get; set; } = string.Empty;
    public DateTime? pickupDate { get; set; }
    public DateTime? dropOffDate { get; set; }
    public DateTime? paidDate { get; set; }
    public string plateNo { get; set; } = string.Empty;
    public string fromPlaceName { get; set; } = string.Empty;
    public string toPlaceName { get; set; } = string.Empty;
    public List<string>? photos { get; set; }
    public List<string>? invoices { get; set; }
}
