using API.Data.Interface;
using API.Models.Expenditure;
using API.Models.Partner;
using API.Models.Project;
using ErrorCodeLib;
using ErrorCodeLib.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NLog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Data.Repository
{

    public class ProjectRepository : IProjectRepository<ProjectModel>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ErpDbContext _context;

        private Iproject_partnerRepository<project_partnerModel> _repo_project_partner;
        private IGenericRepository<PartnerModel> _repo_partner;
        private IUnitOfWork _unitOfWork;
        public ProjectRepository(ErpDbContext context
            , Iproject_partnerRepository<project_partnerModel> repo_project_partner

            , IGenericRepository<PartnerModel> repo_partner
            , IUnitOfWork unitOfWork)
        {
            _context = context;
            _repo_project_partner = repo_project_partner;

            _repo_partner = repo_partner;
            _unitOfWork = unitOfWork;
        }

        public ResultModel Create(ProjectModel entity)
        {
            var result = new ResultModel();
            dynamic output;

            var project_partner_list = new List<project_partnerModel>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Add(entity);
                    var except_project_save = _unitOfWork.Save();
                    if (except_project_save != null)
                        throw except_project_save;
                    output = new
                    {
                        project_id = entity.id

                    };

                    if (!string.IsNullOrEmpty(entity.partner_id))
                    {

                        //新增合作廠商id 到project_partner table (註)
                        string[] partner_id_arr = entity.partner_id.Split(",");
                        foreach (var i in partner_id_arr)
                        {
                            var data = new project_partnerModel()
                            {
                                created = DateTime.Now,
                                project_id = entity.id,
                                partner_id = Convert.ToInt32(i)
                            };

                            _repo_project_partner.Create(data);
                            project_partner_list.Add(data);
                        }

                        var except_partner_save = _unitOfWork.Save();
                        if (except_partner_save != null)
                            throw except_partner_save;

                        output = new
                        {
                            project_id = entity.id,
                            project_partner_id = project_partner_list.Select(a => a.id)
                        };
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    result = ErrorCode.CUSTOM_ERROR(e.Message, "DB ERROR", 500, "cht");
                    return result;
                }
            }

            result = ErrorCode.SUCCESS(output, "", "cht");
            return result;
        }



        public ResultModel FindAll(Expression<Func<ProjectModel, bool>> expression)
        {
            var output = new object();
            var result = new ResultModel();
            var query = _context.project.Where(expression).AsNoTracking().ToList()
                   .GroupJoin(
                       _context.project_partner.ToList(),
                       o => o.id,
                       c => c.project_id,
                       (o, c) =>
                       new
                       {
                           o.id,
                           o.name,
                           o.budget,
                           o.date_start,
                           o.date_end,
                           o.internal_HR,
                           o.outsourcing_HR,
                           o.remarks,
                           partner_id = string.Join(',', c.Select(x => x.partner_id).OrderBy(x => x)),
                           o.status
                       });


            output = new
            {
                data = query,
                total = query.Count()
            };
            result = ErrorCode.SUCCESS(output, "", "cht");
            return result;

        }


        public IQueryable<ProjectModel> ListProtoType(Expression<Func<ProjectModel, bool>> expression)
        {
            var output = new object();
            var result = new ErrorCodeLib.Models.ResultModel();
            var query = _context.project
                .Where(expression)
                .AsNoTracking()
                .AsQueryable();

            return query;

        }

        public void Update(ProjectModel entity)
        {
            var result = new ErrorCodeLib.Models.ResultModel();
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(a => a.created).IsModified = false;
        }


        public void Delete(ProjectModel entity)
        {
            throw new NotImplementedException();
        }

        public ProjectModel FindById(int id)
        {
            var entity = _context.project.AsNoTracking().FirstOrDefault(a => a.id == id);
            return entity;
        }
    }
}
