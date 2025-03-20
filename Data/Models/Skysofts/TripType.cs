using System.ComponentModel.DataAnnotations.Schema;
using ntgroup.Extensions;

namespace ntgroup.Data.Models.Skysofts;

public class TripType
{
    public int id { get; set; }
    public string typeName { get; set; }
    public string typeStatic { get; set; }
    public string createdAt { get; set; }
}
