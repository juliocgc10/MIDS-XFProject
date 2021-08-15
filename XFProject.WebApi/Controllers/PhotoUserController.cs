﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XFProject.DataAccess;
using XFProject.Entities;

namespace XFProject.WebApi.Controllers
{
    public class PhotoUserController : ApiController
    {
        private Repository repository;
        public PhotoUserController()
        {
            repository = new Repository();
        }
        // GET api/values
        public IEnumerable<PhotoUserDto> Get()
        {
            return repository.GetPhotoUsers();
        }

        // GET api/values/5
        public PhotoUserDto Get(int id)
        {
            return repository.GetPhotoUserById(id);
        }

        // POST api/values
        public PhotoUserDto Post([FromBody] PhotoUserDto value)
        {
            return repository.InsertPhotoUser(value);
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
    }
}
