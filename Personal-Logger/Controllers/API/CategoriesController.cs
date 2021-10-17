using PersonalLogger.DTO;
using PersonalLogger.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using AutoMapper;
using System.Data.Entity.Infrastructure;
using System;
using PersonalLogger.Repository;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace PersonalLogger.Controllers.API
{
    public class CategoriesController : ApiController
    {

        private IUnitOfWork unitOfWork;

        public CategoriesController()
        {
            var context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
        }

        //GET /api/categories
        [HttpGet]
        public IHttpActionResult GetCategories(string query = null)
        {
            var userId = User.Identity.GetUserId();

            //var list = context.LogCategories.Include(c => c.CategoryFields)
            //  .Where(c => c.ApplicationUserId == userId);

            IEnumerable<LogCategory> list;


            if (!string.IsNullOrWhiteSpace(query))
            {
                list = unitOfWork.LogCategories.GetWithCategoryFields(lc => lc.ApplicationUserId == userId && lc.CategoryName.Contains(query));
            }
            else
            {
                list = unitOfWork.LogCategories.GetWithCategoryFields(lc => lc.ApplicationUserId == userId);
            }
            
            var categories = list.Select(Mapper.Map<LogCategory, LogCategoryDTO>);


            return Ok(categories);
        }

        //GET /api/categories/1
        public IHttpActionResult GetCategory(int id)
        {
            var category = unitOfWork.LogCategories.GetByIDWithCategoryFields(id);
            if (category == null)
            {
                NotFound();
            }
            //TODO MAP
            return Ok(category);
        }

        //POST /api/cetegories
        [HttpPost]
        public IHttpActionResult CreateCategory(LogCategoryDTO logCategoryDTO)
        {

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var category = Mapper.Map<LogCategoryDTO, LogCategory>(logCategoryDTO);

            category.ApplicationUserId = User.Identity.GetUserId();



            //category = context.LogCategories.Add(category);

            unitOfWork.LogCategories.Insert(category);

            unitOfWork.Commit();

            return Ok(Mapper.Map<LogCategory,LogCategoryDTO>(category));

        }

        //PUT /api/categories/1
        [HttpPut]
        public IHttpActionResult UpdateCategory(LogCategoryDTO logCategoryDTO)
        {
            return NotFound();
        }

        //DELETE /api/categories/1
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = unitOfWork.LogCategories.GetByIDWithCategoryFields(id);

            if (category == null)
            {
                NotFound();
            }
            unitOfWork.LogCategories.Delete(category);
            unitOfWork.Commit();

            return Ok(category);
        }


    }
}
