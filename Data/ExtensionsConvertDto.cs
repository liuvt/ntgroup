using ntgroup.Data.Entities.Skysofts;
using ntgroup.Data.Models.Skysofts;
using ntgroup.Extensions;

namespace ntgroup.Data;

public static class ExtensionsConvertDto
{
    // Thêm "this" để không phải truyền tham số
    // Khi gọi hàm ExtensionsConvertDto chỉ truyền duy nhất tham số không có "this"
    public static IEnumerable<TripDTO> TripsDto(this IEnumerable<Trip> trips,
                                                                 IEnumerable<Vehicle> vehicles)
    {
        var _trips = (from t in trips
                      join v in vehicles on t.vehicleID equals v.vehicleID
                      select new TripDTO
                      {
                          _id = t._id,
                          vehicleID = v.vehicleID,
                          userName = t.userName,
                          vehicleNo = v.vehicleNo,
                          plateNo = v.plateNo,
                          staffName = v.staffName,
                          pickupDate = DatetimeExtensions.DateTimeFromStringDefault(t.pickupDate),
                          dropOffDate = DatetimeExtensions.DateTimeFromStringDefault(t.dropOffDate),
                          km = SumString.ReturnDouble(t.km),
                          emptyKm = SumString.ReturnDouble(t.emptyKm),
                          totalKm = SumString.ReturnDouble(t.km, t.emptyKm),
                          waitTime = t.waitTime,
                          waitCharge = t.waitCharge,
                          charge = t.charge,
                          realCharge = (t.realCharge == "-1") ? t.charge : t.realCharge,
                          type = (t.typeID == 0) ? "Đi theo đồng hồ" :
                                  (t.typeID == 1) ? "Đi theo hợp đồng" :
                                  (t.typeID == 4) ? "App to App" : Convert.ToString(t.typeID),
                          fromPlaceName = t.fromPlaceName,
                          toPlaceName = t.toPlaceName,
                      });
        Console.WriteLine("Tổng cuốc: " + _trips.Count());
        Console.WriteLine("Tiền: " + SumString.SumListString<TripDTO>(_trips.ToList(), "charge"));
        Console.WriteLine("Thành tiền (nhập thực thu): " + SumString.SumListString<TripDTO>(_trips.ToList(), "realCharge"));
        return _trips;
    }
}