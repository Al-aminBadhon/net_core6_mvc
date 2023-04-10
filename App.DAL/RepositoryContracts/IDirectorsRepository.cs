using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.RepositoryContracts
{
    public interface IDirectorsRepository/*<TModel> where TModel : class*/
    {
        Task<List<TblDirector>> GetAllDirectors();
        Task<TblDirector> CreateDirectors(TblDirector TblDirector);
        Task<TblDirector> UpdateDirectors(TblDirector TblDirector);

    }
}
