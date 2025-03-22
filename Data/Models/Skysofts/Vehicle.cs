using ntgroup.Extensions;

namespace ntgroup.Data.Models.Skysofts;

//Schema detail
public class Vehicle
{
    public int vehicleID { get; set; }
    public int customerID { get; set; }
    public string x { get; set; } = string.Empty;
    public string y { get; set; } = string.Empty;
    public string plateNo { get; set; } = string.Empty;
    public string vehicleCode { get; set; } = string.Empty;
    public string deviceType { get; set; } = string.Empty;
    public string currentSpeed { get; set; } = string.Empty;
    public string pulseSpeed { get; set; } = string.Empty;
    public string direction { get; set; } = string.Empty;
    public string updateDate { get; set; } = string.Empty;
    public string gpsDate { get; set; } = string.Empty;
    public string engineState { get; set; } = string.Empty;
    public string gpsState { get; set; } = string.Empty;
    public string staffID { get; set; } = string.Empty;
    public string staffName { get; set; } = string.Empty;
    public string licenseNo { get; set; } = string.Empty;
    public string vehicleNo { get; set; } = string.Empty;
    public DateTime? stopDate { get; set; }
    public int? stopDuration { get; set; }
    public string dailyDrivingDuration { get; set; } = string.Empty;
    public string drivingDuration { get; set; } = string.Empty;
    public string numOfOverSpeed { get; set; } = string.Empty;
    public string dailyEstFuelUsage { get; set; } = string.Empty;
    public string dailyDistance { get; set; } = string.Empty;
    public bool locked { get; set; }
    public bool sendToMT { get; set; }
    public TaxiTrip? taxiTrip { get; set; }
}

//Schema Total
public class VehicleTotal
{
    public List<Vehicle>? vehicles { get; set; }
    public string actionResult { get; set; } = string.Empty;
}