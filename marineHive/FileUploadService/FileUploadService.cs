using App.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;


namespace App.Home.FileUploadService
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImageDirector(TblDirector TblDirector)
        {
            var locationWithName = "images/directors/";
            locationWithName += Guid.NewGuid().ToString() + "_" + TblDirector.PhotoUpload.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await TblDirector.PhotoUpload.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }
        public async Task<string> UploadImageGallery(TblGalleryPhoto tblGalleryPhoto)
        {
            var locationWithName = "images/gallery/";
            locationWithName += Guid.NewGuid().ToString() + "_" + tblGalleryPhoto.PhotoUpload.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await tblGalleryPhoto.PhotoUpload.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }
        public async Task<string> UploadImageCrewManning(TblCrewManning tblCrewManning)
        {
            var locationWithName = "images/crewManning/";
            locationWithName += Guid.NewGuid().ToString() + "_" + tblCrewManning.PhotoUpload.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await tblCrewManning.PhotoUpload.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }
        public async Task<string> UploadImageCrewTraining(TblCrewTraining tblCrewTraining)
        {
            var locationWithName = "images/crewTraining/";
            locationWithName += Guid.NewGuid().ToString() + "_" + tblCrewTraining.PhotoUpload.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await tblCrewTraining.PhotoUpload.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }
        public async Task<string> UploadImageExecutive(TblExecutive tblExecutive)
        {
            var locationWithName = "images/executive/";
            locationWithName += Guid.NewGuid().ToString() + "_" + tblExecutive.PhotoUpload.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await tblExecutive.PhotoUpload.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }
        public async Task<string> UploadImageMissionVission(IFormFile tblMissionVission)
        {
            var locationWithName = "images/missionVission/";
            locationWithName += Guid.NewGuid().ToString() + "_" + tblMissionVission.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await tblMissionVission.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }
        public async Task<string> UploadImagePortAgency(TblPortAgency tblPortAgency)
        {
            var locationWithName = "images/portAgency/";
            locationWithName += Guid.NewGuid().ToString() + "_" + tblPortAgency.PhotoUpload.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await tblPortAgency.PhotoUpload.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }
        public async Task<string> UploadImageShipManagement(TblShipManagement tblShipManagement)
        {
            var locationWithName = "images/ShipManagement/";
            locationWithName += Guid.NewGuid().ToString() + "_" + tblShipManagement.PhotoUpload.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await tblShipManagement.PhotoUpload.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }
        public async Task<string> UploadImageWhyBdcrew(TblWhyBdcrew tblWhyBdcrew)
        {
            var locationWithName = "images/whyBdcrew/";
            locationWithName += Guid.NewGuid().ToString() + "_" + tblWhyBdcrew.PhotoUpload.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, locationWithName);
            await tblWhyBdcrew.PhotoUpload.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var imagePath = "/" + locationWithName;

            return imagePath;
        }

        public void DeleteImage(string ImagePath)
        {
            string root = _webHostEnvironment.WebRootPath;

            //string existingFile = Path.Combine(_webHostEnvironment.WebRootPath, tblCrewManning.Image);
            string existingFile = root + ImagePath;
            File.Delete(existingFile);
            return;
        }
    }
}
