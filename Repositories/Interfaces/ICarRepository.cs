using ntgroup.Data.Entities;

namespace ntgroup.Repositories.Interfaces;

public interface ICarRepository
{
    Task<IEnumerable<CarDTO>> Gets();
    Task<CarDTO> Get(string car_Id);
}