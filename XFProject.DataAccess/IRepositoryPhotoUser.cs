using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XFProject.Entities;

namespace XFProject.DataAccess
{
    public interface IRepositoryPhotoUser
    {
        List<PhotoUserDto> GetPhotoUsers(Expression<Func<PhotoUser, bool>> predicate = null);
        PhotoUserDto GetPhotoUserById(int photoUserID);
        PhotoUserDto InsertPhotoUser(PhotoUserDto photoUserDto);
        PhotoUserDto UpdatePhotoUser(PhotoUserDto photoUserDto);
        PhotoUserDto DeletePhotoUser(int PhotoUserID);        
    }
}
