namespace EMIC2.Models.Interface.NDS2
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EMIC2.Models.Dao.Dto.NDS2;

    public interface IEocProjectDao
    {
        IEnumerable<EocProjectDto> SearchAvailable(string prjType, DateTime prjSDate, DateTime prjEDate, string prjName);

        Task<IEnumerable<EocProjectDto>> SearchAvailableAsync(string prjType, DateTime prjSDate, DateTime prjEDate, string prjName);

        IEnumerable<EocProjectDto> SearchAvailable20301(string prjType, DateTime prjSDate, DateTime prjEDate, string prjName);

        Task<IEnumerable<EocProjectDto>> SearchAvailableAsync20301(string prjType, DateTime prjSDate, DateTime prjEDate, string prjName);
    }
}