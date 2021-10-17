using AutoMapper;
using Microsoft.Ajax.Utilities;
using PersonalLogger.DTO;
using PersonalLogger.Models;
using PersonalLogger.Repository;
using System.Linq;
using System.Web.Http;

namespace PersonalLogger.Controllers.API
{
    public class FieldTypesController : ApiController
    {


        private IUnitOfWork unitOfWork;

        public FieldTypesController()
        {
            var context = new ApplicationDbContext();

            unitOfWork = new UnitOfWork(context);

        }


        //GET /api/FieldTypes
        [HttpGet]
        public IHttpActionResult GetFieldTypes()
        {
            var fieldTypes = unitOfWork.FieldTypes.Get().Select(Mapper.Map<Models.FieldType,FieldTypeDTO>);

            return Ok(fieldTypes);
        }

    }
}
