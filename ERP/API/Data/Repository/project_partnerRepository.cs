using API.Data.Interface;
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

namespace API.Data.Repository
{

    public class Project_partnerRepository : Iproject_partnerRepository<project_partnerModel>

    {
        private readonly ErpDbContext _context;

        public Project_partnerRepository(ErpDbContext context)
        {
            _context = context;
        }

        public void Create(project_partnerModel entity)
        {
            _context.project_partner.Add(entity);
        }



        public ResultModel FindAll(Expression<Func<project_partnerModel, bool>> expression)
        {
            var result = new ErrorCodeLib.Models.ResultModel();
            var data = _context.project_partner
                .Include(a => a.partner_)
                .Include(a => a.project_)
                .Where(expression)
                .AsNoTracking()
                //.Select(c => new
                //{
                //    id = c.project_id,
                //    c.project_.name,
                //    c.project_.budget,
                //    c.project_.date_start,
                //    c.project_.date_end,
                //    c.project_.internal_HR,
                //    c.project_.outsourcing_HR,
                //    c.project_.remarks,
                //    c.partner_id,
                //    c.project_.status

                //})
                .GroupBy(a=>a.id)
                //.OrderBy(a=>a.id)
                //.OrderBy(a=>a.partner_id)
                .ToList();
            result = ErrorCode.SUCCESS(data, "", "cht");

            return result;

        }

        public IQueryable<project_partnerModel> ListProtoType(Expression<Func<project_partnerModel, bool>> expression)
        {
            return _context.project_partner.Where(expression).AsQueryable();
              
        }

        public project_partnerModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel Update(project_partnerModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(project_partnerModel entity)
        {
            _context.Entry<project_partnerModel>(entity).State = EntityState.Deleted;
        }

       
    }
}
