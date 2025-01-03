using ntgroup.Data.Entities;

namespace ntgroup.APIs.Contracts;

public interface ITimepieceServer
{
    Task<bool> Create(List<TimepieceDTO> models);
}