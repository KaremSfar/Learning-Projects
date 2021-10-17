using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PersonalLogger.DTO;
using PersonalLogger.Models;
using PersonalLogger.Repository;
using PersonalLogger.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;

namespace PersonalLogger.Controllers.API
{
    public class MyLogsController : ApiController
    {

        private IUnitOfWork unitOfWork;

        public MyLogsController()
        {
            var context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
        }

        //GET /api/myLogs
        public IHttpActionResult GetLogs(int? categoryId = null, int? lastDays = null)
        {
            var userId = User.Identity.GetUserId();

            DateTime? lastDate = null;

            if (lastDays != null)
            {
                lastDate = DateTime.Now.AddDays(-(double)lastDays.Value);
            }

            IEnumerable<MyLog> logList = unitOfWork.MyLogs.GetWithFields(
                m => m.ApplicationUserId == userId
                && (categoryId != null ? (m.LogCategoryId == categoryId) : true)
                && (lastDays != null ? (m.LogDate >= lastDate) : true));


            return Ok(logList);
        }

        //POST /api/myLogs
        [HttpPost]
        public IHttpActionResult CreateLog(MyLogDTO myLogDTO)
        {
            if (!ModelState.IsValid){
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();

            var category = unitOfWork.LogCategories.GetByIDWithCategoryFields(myLogDTO.LogCategoryId);

            if (category == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var fields = new List<Field>();

            var fieldFactory = new FieldFactory();

            foreach(var field in myLogDTO.Fields)
            {
                var fieldType = category.CategoryFields.SingleOrDefault(cf => cf.Id == field.CategoryField.Id).FieldType.TypeName;
                Field lol = fieldFactory.CreateField(field.Value, fieldType);
                lol.CategoryFieldId = field.CategoryField.Id;
                fields.Add(lol);   
            }

            var myLog = new MyLog
            {
                ApplicationUserId = userId,
                Fields = fields,
                LogDate = DateTime.Now,
                LogCategoryId = myLogDTO.LogCategoryId
            };



            unitOfWork.MyLogs.Insert(myLog);

            unitOfWork.Commit();

            var rLog = Mapper.Map<MyLog, MyLogDTO>(myLog);

            return Ok(rLog);
        }


        [HttpDelete]
        public IHttpActionResult DeleteLog(int id)
        {
            var userId = User.Identity.GetUserId();

            var log = unitOfWork.MyLogs.GetWithFields(ml => ml.Id == id && ml.ApplicationUserId == userId).SingleOrDefault();

            if (log == null)
            {
                NotFound();
            }

            unitOfWork.MyLogs.Delete(log);
            unitOfWork.Commit();

            return Ok(Mapper.Map<MyLog,MyLogDTO>(log));
        }
    }
}
