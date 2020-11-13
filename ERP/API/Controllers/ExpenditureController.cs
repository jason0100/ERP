using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models.Expenditure;
using ErrorCodeLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using API.Data.Interface;
using API.Models;
using NLog;
using NLog.LayoutRenderers.Wrappers;
using API.Models.Project;
using API.Models.Partner;
using API.Models.Item;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpenditureController : ControllerBase
    {
        private IExpenditureRepository<ExpenditureModel> _repo_expenditure;
        private IProjectRepository<ProjectModel> _repo_project;
        private IGenericRepository<PartnerModel> _repo_partner;
        private IGenericRepository<ItemModel> _repo_item;

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IUnitOfWork _unitOfWork;
        public ExpenditureController(
            IExpenditureRepository<ExpenditureModel> repo_expenditure
            , IProjectRepository<ProjectModel> repo_project
            , IGenericRepository<PartnerModel> repo_partner
            , IGenericRepository<ItemModel> repo_item
            , IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo_expenditure = repo_expenditure;
            _repo_project = repo_project;
            _repo_partner = repo_partner;
            _repo_item = repo_item;
        }

        /// <summary>
        /// 查詢專案支出資料
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Expenditure?date_start = 2020-05-16&amp; date_end = 2020-05-18
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<ErrorCodeLib.Models.ResultModel> GET([FromQuery] ExpenditureModelForQuery model)
        {
            var result = new ErrorCodeLib.Models.ResultModel();

            var predicate = PredicateBuilder.True<ExpenditureModel>();

            if (model.partner_id != null)
                predicate = predicate.And(a => a.partner_id == model.partner_id);
            if (model.project_id != null)
                predicate = predicate.And(a => a.project_id == model.project_id);
            if (model.date_start != null)
                predicate = predicate.And(a => a.project_.date_start >= model.date_start);
            if (model.date_end != null)
                predicate = predicate.And(a => a.project_.date_end <= model.date_end);

            var query_result = _repo_expenditure.FindAll(predicate);
            result = ErrorCode.SUCCESS(query_result, "", "cht");
            return result;
        }


        /// <summary>
        /// 新增專案支出資料
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Expenditure
        ///     {
        ///	        "project_id":2,
        ///	        "partner_id":1,
        ///	        "item_id":1,
        ///	        "due_date":"2020-05-12",
        ///     	"payment_date":"2020-05-12",
        ///	        "amount_payable":500000,
        ///	        "status":1
        ///     }
        /// </remarks>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost]
        public async Task<ErrorCodeLib.Models.ResultModel> POST([FromBody] ExpenditureModel model)
        {
            var result = new ErrorCodeLib.Models.ResultModel();

            //檢查project
            var exist_project = _repo_project.FindById(Convert.ToInt32( model.project_id));
            if (exist_project == null)
            {
                result = ErrorCode.DB_ERROR_NOT_FOUND(null, "project_id", "cht");
                return result;
            }

            //檢查partner
            var exist_partner = _repo_partner.FindById(Convert.ToInt32(model.partner_id));
            if (exist_partner == null)
            {
                result = ErrorCode.DB_ERROR_NOT_FOUND(null, "partner_id", "cht");
                return result;
            }
           
            //檢查item
            var exist_item = _repo_item.FindById(Convert.ToInt32(model.item_id));
            if (exist_item == null)
            {
                result = ErrorCode.DB_ERROR_NOT_FOUND(null, "item_id", "cht");
                return result;
            }
            model.created = DateTime.Now;

            _repo_expenditure.Create(model);
            var exception_save = _unitOfWork.Save();
            if (exception_save != null)
                result = ErrorCode.CUSTOM_ERROR(exception_save.Message, "DB ERROR", 500, "cht");
            var output = new
            {
                model.id
            };
            result = ErrorCode.SUCCESS(null, "新增成功", "cht");
            return result;
        }



        /// <summary>
        /// 修改專案支出資料
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Expenditure
        ///     {
        ///	        "project_id":2,
        ///	        "partner_id":1,
        ///	        "item_id":1,
        ///	        "due_date":"2020-05-12",
        ///     	"payment_date":"2020-05-12",
        ///	        "amount_payable":500000,
        ///	        "status":1
        ///     }
        /// </remarks>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPut]
        public async Task<ErrorCodeLib.Models.ResultModel> PUT([FromBody] ExpenditureModel model)
        {
            var result = new ErrorCodeLib.Models.ResultModel();
            if (model.id == null)
            {
                result = ErrorCode.PARAMETER_REQUIRED(new string[] { "id" }, "cht");
                return result;
            }

            //檢查expenditure
            var exist_expenditure = _repo_expenditure.FindById(Convert.ToInt32( model.id));
            if (exist_expenditure == null)
            {
                result = ErrorCode.DB_ERROR_NOT_FOUND(null, "id", "cht");
                return result;
            }

            //檢查project
            var exist_project = _repo_project.FindById(Convert.ToInt32(model.project_id));
            if (exist_project == null)
            {
                result = ErrorCode.DB_ERROR_NOT_FOUND(null, "project_id", "cht");
                return result;
            }

            //檢查partner
            var exist_partner = _repo_partner.FindById(Convert.ToInt32(model.partner_id));
            if (exist_partner == null)
            {
                result = ErrorCode.DB_ERROR_NOT_FOUND(null, "partner_id", "cht");
                return result;
            }

            //檢查item
            var exist_item = _repo_item.FindById(Convert.ToInt32(model.item_id));
            if (exist_item == null)
            {
                result = ErrorCode.DB_ERROR_NOT_FOUND(null, "item_id", "cht");
                return result;
            }

            model.updated = DateTime.Now; 
            
            _repo_expenditure.Update(model);

            var exception_save = _unitOfWork.Save();
            if (exception_save != null)
                result = ErrorCode.CUSTOM_ERROR(exception_save.Message, "DB ERROR", 500, "cht");
          
            result = ErrorCode.SUCCESS(null, "修改成功", "cht");
            return result;
        }
    }
}