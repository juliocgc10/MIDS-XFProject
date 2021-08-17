using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using XFProject.DataAccess;
using XFProject.Entities;

namespace XFProject.WebApi.Controllers
{
    public class PhotoUserController : ApiController
    {
        private IRepositoryPhotoUser repository;
        public PhotoUserController()
        {
            repository = new RepositoryPhotoUser();
        }
        // GET api/values
        public IEnumerable<PhotoUserDto> Get(bool isMyPhotos, string autor = null)
        {
            if (string.IsNullOrWhiteSpace(autor))
            {
                return repository.GetPhotoUsers();
            }
            else
            {
                if (isMyPhotos)
                    return repository.GetPhotoUsers(x => x.NickNameAutor.Trim().ToUpper() == autor.Trim().ToUpper());
                else
                    return repository.GetPhotoUsers(x => x.NickNameAutor.Trim().ToUpper() != autor.Trim().ToUpper());
            }
        }

        // GET api/values/5
        public PhotoUserDto Get(int id)
        {
            return repository.GetPhotoUserById(id);
        }

        // POST api/values
        public PhotoUserDto Post([FromBody] PhotoUserDto value)
        {
            var obj = repository.InsertPhotoUser(value);

            SaveImage(value.PhotoBase64, value.PhotoID.ToString().ToLower());
            return obj;
        }

        // PUT api/values/5
        public PhotoUserDto Put([FromBody] PhotoUserDto value)
        {
            return repository.UpdatePhotoUser(value);
        }

        // DELETE api/values/5
        public PhotoUserDto Delete(int id)
        {
            return repository.DeletePhotoUser(id);
        }

        public bool SaveImage(string ImgStr, string ImgName)
        {
            String path = HttpContext.Current.Server.MapPath("~/Files"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = ImgName + ".jpg";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(imgPath, imageBytes);

            return true;
        }
    }
}
