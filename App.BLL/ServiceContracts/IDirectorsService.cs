using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.ServiceContracts
{
    public interface IDirectorsService
    {
        Task<List<TblDirector>> GetAllDirectors();
        Task<TblDirector> CreateDirectors(TblDirector tblDirectors);
        Task<TblDirector> UpdateDirectors(TblDirector tblDirectors);
    }
}
