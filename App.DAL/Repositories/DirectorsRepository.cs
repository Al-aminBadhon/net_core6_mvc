
using App.DAL.Models;
using App.DAL.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using App.DAL.Data;

namespace App.DAL.Repositories
{
    public class DirectorsRepository/*<TModel>*/ : IDirectorsRepository/*<TModel> where TModel : class*/
    {
        private readonly MHDBContext _mHDBContext;


        public DirectorsRepository(MHDBContext mHDBContext)
        {
            _mHDBContext = mHDBContext;

        }

        public async Task<List<TblDirector>> GetAllDirectors()
        {
            try
            {
                List<TblDirector> directors = new List<TblDirector>();
                //directors = _mHDBContext.TblDirectors.FromSql("select a.* from tblDirectors a  where a.IsDeleted = 0").ToList();
                directors = await _mHDBContext.TblDirectors.Where(x => x.IsDeleted == false).ToListAsync();

                //const String sql = "select a.* from tblDirectors a  where a.IsDeleted = 0";
                //directors = _mHDBContext.Database.ExecuteSqlCommand(sql);
                return directors;
            }
            catch
            {
                throw;
            }

        }
         public async Task<TblDirector> CreateDirectors(TblDirector model)
        {
            try
            {
               
                return model;
            }
            catch
            {
                throw;
            }

        }
          public async Task<TblDirector> UpdateDirectors(TblDirector tblDirectors)
        {
            try
            {

                

                _mHDBContext.Update(tblDirectors);
                await _mHDBContext.SaveChangesAsync();
                
            }
            catch
            {
                throw;
            }
            return tblDirectors;
        }

    }
}
