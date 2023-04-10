using App.BLL.ServiceContracts;
using App.DAL.Models;
using App.DAL.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class DirectorsService : IDirectorsService
    {
        private readonly IDirectorsRepository _directorsRepository;
        public DirectorsService(IDirectorsRepository directorsRepository)
        {
            this._directorsRepository = directorsRepository;
        }

        public async Task<List<TblDirector>> GetAllDirectors()
        {
            return await _directorsRepository.GetAllDirectors();
        }

        public async Task<TblDirector> CreateDirectors(TblDirector TblDirector)
        {
            return await _directorsRepository.CreateDirectors( TblDirector);
        }
        
        public async Task<TblDirector> UpdateDirectors(TblDirector TblDirector)
        {
            return await _directorsRepository.UpdateDirectors( TblDirector);
        }
    }
}
