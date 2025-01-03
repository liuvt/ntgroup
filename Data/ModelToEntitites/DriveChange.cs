
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.ModelToEntities;

public static class DriveChange
{
    // Thêm "this" để không phải truyền tham số
    public static DriveDTO DriveToDo(this Drive drive)
    {
        return new DriveDTO
        {

        };
    }

    // Thêm "this" để không phải truyền tham số
    public static IEnumerable<DriveDTO> DriveToDo(this IEnumerable<Drive> _dives)
    {
        return(from d in _dives
                    select new DriveDTO
                    {

                    });
    }
}