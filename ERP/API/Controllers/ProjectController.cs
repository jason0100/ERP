using System;
using System.Threading.Tasks;
using API.Data;
using API.Data.Interface;
using API.Models;
using API.Models.Expenditure;
using API.Models.Partner;
using API.Models.Project;
using ErrorCodeLib;
using ErrorCodeLib.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private Iproject_partnerRepository<project_partnerModel> _repo_project_partner;
        private IProjectRepository<ProjectModel> _repo_project;
        private IGenericRepository<PartnerModel> _repo_partner;
        private IUnitOfWork _unitOfWork;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ProjectController(

             IProjectRepository<ProjectModel> repo_project
            , Iproject_partnerRepository<project_partnerModel> repo_project_partner
            , IGenericRepository<PartnerModel> repo_partner
             , IUnitOfWork unitOfWork
            )
        {

            _repo_project = repo_project;
            _repo_project_partner = repo_project_partner;
            _repo_partner = repo_partner;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 查詢專案資料
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     /Project?id=2&amp;date_start=2020-05-12   
        /// 
        /// </remarks>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet]
        public async Task<ErrorCodeLib.Models.ResultModel> Get([FromQuery] ProjectModelForQuery model)
        {

            var result = new ErrorCodeLib.Models.ResultModel();
            if (model.partner_id != null)
            {
                var predicate = PredicateBuilder.True<project_partnerModel>();
                predicate = predicate.And(a => a.partner_id == model.partner_id);
                if (model.partner_id != null)
                    predicate = predicate.And(a => a.partner_id == model.partner_id);
                if (model.id != null)
                    predicate = predicate.And(a => a.project_id == model.id);
                if (model.date_start != null)
                    predicate = predicate.And(a => a.project_.date_start >= model.date_start);
                if (model.date_end != null)
                    predicate = predicate.And(a => a.project_.date_end <= model.date_end);
                var query_result = _repo_project_partner.FindAll(predicate);


                if (query_result.Status != 200)
                    return query_result;

                result = query_result;

            }
            else
            {
                var predicate = PredicateBuilder.True<ProjectModel>();
                if (model.id != null)
                    predicate = predicate.And(a => a.id == model.id);
                if (model.date_start != null)
                    predicate = predicate.And(a => a.date_start >= model.date_start);
                if (model.date_end != null)
                    predicate = predicate.And(a => a.date_end <= model.date_end);
                var query_result = _repo_project.FindAll(predicate);

                if (query_result.Status != 200)
                    return query_result;
                result = query_result;
            }



            return result;
        }


        /// <summary>
        /// 新增專案資料
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /project
        ///     {
        ///         "name":"project1",
        ///         "budget":1000000,
        ///         "date_start":"2020-05-12",
        ///         "date_end":"2020-05-12",
        ///         "status":1
        ///     }
        /// </remarks>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost]
        public async Task<ErrorCodeLib.Models.ResultModel> Create([FromBody] ProjectModel model)
        {
            var result = new ErrorCodeLib.Models.ResultModel();
            model.created = DateTime.Now;
            //檢查partner
            var exist_partner = new PartnerModel();
            if (!string.IsNullOrEmpty(model.partner_id))
            {
                string[] partner_id_arr = model.partner_id.Split(",");
                foreach (var i in partner_id_arr)
                {
                    try
                    {
                        exist_partner = _repo_partner.FindById(Convert.ToInt32(i));
                    }
                    catch (Exception e)
                    {
                        result = ErrorCode.PARAMETER_INVALID(null, "partner_id", "cht");
                        return result;
                    }
                    if (exist_partner == null)
                    {
                        result = ErrorCode.DB_ERROR_NOT_FOUND(null, "partner_id", "cht");
                        return result;
                    }

                }
            }
            result = _repo_project.Create(model);

            return result;
        }


        /// <summary>
        /// 修改專案資料
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /project
        ///     {
        ///         "id":,
        ///         "name":"project1",
        ///         "budget":2000000,
        ///         "date_start":"2020-05-12",
        ///         "date_end":"2020-05-19",
        ///         "partner_id":1,
        ///         "status":1
        ///     }
        /// </remarks>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPut]
        public async Task<ErrorCodeLib.Models.ResultModel> Put([FromBody] ProjectModel model)
        {
            var result = new ErrorCodeLib.Models.ResultModel();
            //檢查project
            var existData =_repo_project.FindById(model.id);
            if (existData == null)
            {
                result = ErrorCode.DB_ERROR_NOT_FOUND(null, "id", "cht");
                return result;
            }


            //檢查partner
            var exist_partner = new PartnerModel();
            if (!string.IsNullOrEmpty(model.partner_id))
            {
                string[] partner_id_arr = model.partner_id.Split(",");
                foreach (var i in partner_id_arr)
                {
                    try
                    {
                        exist_partner = _repo_partner.FindById(Convert.ToInt32(i));
                    }
                    catch (Exception e)
                    {
                        result = ErrorCode.PARAMETER_INVALID(null, "partner_id", "cht");
                        return result;
                    }
                    if (exist_partner == null)
                    {
                        result = ErrorCode.DB_ERROR_NOT_FOUND(null, "partner_id", "cht");
                        return result;
                    }

                }
            }
            model.updated = DateTime.Now;
             _repo_project.Update(model);

            if (!string.IsNullOrEmpty(model.partner_id))
            {
                var exist_project_partner_list = _repo_project_partner.ListProtoType(a => a.project_id == model.id);

                string[] partner_id_arr = model.partner_id.Split(",");
                ////刪除不在partner_id array裡的
                foreach (var k in exist_project_partner_list)
                {
                    if (!Array.Exists(partner_id_arr, a => a == k.partner_id.ToString()))
                        _repo_project_partner.Delete(k);
                }
                //增加exist_project_partner_list缺少的
                foreach (var i in partner_id_arr)
                {

                    bool isExist = false;
                    foreach (var j in exist_project_partner_list)
                    {
                        if (j.partner_id.ToString() == i)
                            isExist = true;

                    }
                    if (!isExist)
                    {
                        //新增合作廠商id (註)
                        var project_partner_data = new project_partnerModel();
                        project_partner_data.created = DateTime.Now;
                        project_partner_data.project_id = model.id;
                        project_partner_data.partner_id = Convert.ToInt32(i);
                        _repo_project_partner.Create(project_partner_data);
                    }
                }
            }

            //最後存檔
            var except = _unitOfWork.Save();
            if (except != null)
            {
                result = ErrorCode.CUSTOM_ERROR(except.Message, "DB ERROR", 500, "cht");
                return result;
            }

            result = ErrorCode.SUCCESS(null, "修改成功", "cht");
            return result;
        }
    }
}