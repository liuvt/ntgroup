using ntgroup.Data.Entities;

namespace ntgroup.Repositories.Interfaces;

public interface ITimepieceRepository
{
    Task<bool> Create(List<TimepieceDTO> models);
}