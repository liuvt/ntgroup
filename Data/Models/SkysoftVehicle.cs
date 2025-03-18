
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocumentFormat.OpenXml.Presentation;

namespace ntgroup.Data.Models;

public class SkysoftVehicle
{
    public List<Vehicle>? vehicles { get; set; }
    public string actionResult { get; set; } = string.Empty;
}

public class Vehicle
{
    public int? vehicleID { get; set; }
    public int? customerID { get; set; }
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

public class TaxiTrip
{
    public int? _id { get; set; }
    public string vehicleID { get; set; } = string.Empty;
    public string state { get; set; } = string.Empty;
    public string distance { get; set; } = string.Empty;
    public string km { get; set; } = string.Empty;
    public string emptyKm { get; set; } = string.Empty;
    public string charge { get; set; } = string.Empty;
    public string realCharge { get; set; } = string.Empty;
    public string waitCharge { get; set; } = string.Empty;
    public string waitTime { get; set; } = string.Empty;
    public string unitPrice { get; set; } = string.Empty;
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

public class SkysoftTrip
{
    public List<Trip>? trips { get; set; }
    public string actionResult { get; set; } = string.Empty;
}

public class Trip
{
    public string _id { get; set; } = string.Empty;
    public string vehicleID { get; set; } = string.Empty;
    public string state { get; set; } = string.Empty;
    public string distance { get; set; } = string.Empty;
    public string km { get; set; } = string.Empty;
    public string emptyKm { get; set; } = string.Empty;
    public string charge { get; set; } = string.Empty;
    public string realCharge { get; set; } = string.Empty;
    public string waitCharge { get; set; } = string.Empty;
    public string waitTime { get; set; } = string.Empty;
    public string unitPrice { get; set; } = string.Empty;
    public string irState { get; set; } = string.Empty;
    public string userName { get; set; } = string.Empty;
    public string typeID { get; set; } = string.Empty;
    public string tripID { get; set; } = string.Empty;
    public string token { get; set; } = string.Empty;
    public string pickupX { get; set; } = string.Empty;
    public string pickupY { get; set; } = string.Empty;
    public string dropOffX { get; set; } = string.Empty;
    public string dropOffY { get; set; } = string.Empty;
    public string pickupDate { get; set; } = string.Empty;
    public string dropOffDate { get; set; } = string.Empty;
    public string paidDate { get; set; } = string.Empty;
    public string plateNo { get; set; } = string.Empty;
    public string fromPlaceName { get; set; } = string.Empty;
    public string toPlaceName { get; set; } = string.Empty;
}