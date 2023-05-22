using App.DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace App.Home.FileUploadService
{
    public interface IFileUploadService
    {
        Task<string> UploadImageDirector(TblDirector tblDirectors);
        Task<string> UploadImageGallery(TblGalleryPhoto tblGalleryPhoto);
        Task<string> UploadImageCrewManning(TblCrewManning tblCrewManning);
        Task<string> UploadImageCrewTraining(TblCrewTraining tblCrewTraining);
        Task<string> UploadImageMissionVission(IFormFile tblMissionVission);
        Task<string> UploadImagePortAgency(TblPortAgency tblPortAgency);
        Task<string> UploadImageShipManagement(TblShipManagement tblShipManagement);
        Task<string> UploadImageWhyBdcrew(TblWhyBdcrew tblWhyBdcrew);
        void DeleteImage(string ImagePath);

    }
}
