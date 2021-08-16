using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XFProject.Entities;

namespace XFProject.DataAccess
{
    public class RepositoryPhotoUser : IRepositoryPhotoUser
    {
        public List<PhotoUserDto> GetPhotoUsers(Expression<Func<PhotoUser, bool>> predicate = null)
        {
            try
            {
                List<PhotoUserDto> PhotoUsers = new List<PhotoUserDto>();
                IEnumerable<PhotoUser> photos;
                using (cpdsEntities contex = new cpdsEntities())
                {
                    if (predicate != null)
                    {
                        photos = contex.PhotoUser.Where(predicate);
                    }
                    else
                        photos = contex.PhotoUser;

                    photos.ToList().ForEach(photo =>
                    {
                        PhotoUsers.Add(ConvertToEntitiesPhotoUser(photo));
                    }
                    );
                }

                return PhotoUsers;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public PhotoUserDto GetPhotoUserById(int photoUserID)
        {
            try
            {
                using (cpdsEntities context = new cpdsEntities())
                {
                    if (context.PhotoUser.Any(x => x.PhotoUserID == photoUserID))
                    {
                        return ConvertToEntitiesPhotoUser(context.PhotoUser.FirstOrDefault(x => x.PhotoUserID == photoUserID));
                    }
                    return new PhotoUserDto();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public PhotoUserDto InsertPhotoUser(PhotoUserDto photoUserDto)
        {

            try
            {
                using (cpdsEntities context = new cpdsEntities())
                {
                    PhotoUser newPhotoUser = ConvertFromEntitiesPhotoUser(photoUserDto);
                    context.PhotoUser.Add(newPhotoUser);
                    context.SaveChanges();

                    photoUserDto.PhotoUserId = newPhotoUser.PhotoUserID;
                    return photoUserDto;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public PhotoUserDto UpdatePhotoUser(PhotoUserDto photoUserDto)
        {
            try
            {
                using (cpdsEntities context = new cpdsEntities())
                {
                    if (context.PhotoUser.Any(x => x.PhotoUserID == photoUserDto.PhotoUserId))
                    {
                        PhotoUser modEmp = context.PhotoUser.FirstOrDefault(x => x.PhotoUserID == photoUserDto.PhotoUserId);
                        ConvertFromEntitiesPhotoUser(photoUserDto, ref modEmp);
                        context.SaveChanges();
                        return photoUserDto;

                    }

                    return new PhotoUserDto();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public PhotoUserDto DeletePhotoUser(int PhotoUserID)
        {
            try
            {
                using (cpdsEntities context = new cpdsEntities())
                {
                    if (context.PhotoUser.Any(x => x.PhotoUserID == PhotoUserID))
                    {
                        PhotoUser deletePhotoUser = context.PhotoUser.FirstOrDefault(x => x.PhotoUserID == PhotoUserID);

                        context.PhotoUser.Remove(deletePhotoUser);
                        context.SaveChanges();
                    }
                    return new PhotoUserDto();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private PhotoUserDto ConvertToEntitiesPhotoUser(PhotoUser photoUser)
        {
            return new PhotoUserDto()
            {
                PhotoUserId = photoUser.PhotoUserID,
                Latitude = photoUser.Latitude,
                Longitude = photoUser.Longitude,
                NickNameAutor = photoUser.NickNameAutor,
                PhotoDescription = photoUser.PhotoDescription,
                PhotoID = photoUser.PhotoID,
                PhotoName = photoUser.PhotoName,
                PhotoTitle = photoUser.PhotoTitle,
                PathUrl = photoUser.PathUrl,
                Email = photoUser.Email
            };

        }

        private PhotoUser ConvertFromEntitiesPhotoUser(PhotoUserDto photoUserDto)
        {
            return new PhotoUser()
            {

                PhotoUserID = photoUserDto.PhotoUserId,
                Latitude = photoUserDto.Latitude,
                Longitude = photoUserDto.Longitude,
                NickNameAutor = photoUserDto.NickNameAutor,
                PhotoDescription = photoUserDto.PhotoDescription,
                PhotoID = photoUserDto.PhotoID,
                PhotoName = photoUserDto.PhotoName,
                PhotoTitle = photoUserDto.PhotoTitle,
                PathUrl = photoUserDto.PathUrl,
                Email = photoUserDto.Email
            };
        }

        private void ConvertFromEntitiesPhotoUser(PhotoUserDto photoUserDto, ref PhotoUser photoUserBD)
        {

            photoUserBD.PhotoUserID = photoUserDto.PhotoUserId;
            photoUserBD.Latitude = photoUserDto.Latitude;
            photoUserBD.Longitude = photoUserDto.Longitude;
            photoUserBD.NickNameAutor = photoUserDto.NickNameAutor;
            photoUserBD.PhotoDescription = photoUserDto.PhotoDescription;
            photoUserBD.PhotoID = photoUserDto.PhotoID;
            photoUserBD.PhotoName = photoUserDto.PhotoName;
            photoUserBD.PhotoTitle = photoUserDto.PhotoTitle;
            photoUserBD.PathUrl = photoUserDto.PathUrl;
            photoUserBD.Email = photoUserDto.Email;

        }

        
    }
}
