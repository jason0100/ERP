using API.Data.Interface;
using API.Models.Expenditure;
using ErrorCodeLib;
using ErrorCodeLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Data.Repository
{

    public class ExpenditureRepository : IExpenditureRepository<ExpenditureModel>
    {
        private readonly ErpDbContext _context;

        public ExpenditureRepository(ErpDbContext context)
        {
            _context = context;
        }

        public void Create(ExpenditureModel entity)
        {
            _context.expenditure.Add(entity);
        }



        public ResultModel FindAll(Expression<Func<ExpenditureModel, bool>> expression)
        {
            var result = new ErrorCodeLib.Models.ResultModel();

            var query = _context.expenditure
                .Include(a => a.project_)
                .Include(a => a.partner_)
                .Where(expression)
                .AsNoTracking()
                .Select(a => new
                {
                    a.id,
                    a.project_id,
                    a.partner_id,
                    a.partner_.organization_id,
                    a.item_id,
                    a.due_date,
                    a.payment_date,
                    overdue = new TimeSpan(Convert.ToDateTime(a.payment_date).Ticks - Convert.ToDateTime(a.due_date).Ticks).Days < 0 ? 0 : new TimeSpan(Convert.ToDateTime(a.payment_date).Ticks - Convert.ToDateTime(a.due_date).Ticks).Days,
                    a.amount_payable,
                    a.amount_paid,
                    a.status
                })
                .ToList();

            var output = new
            {
                data = query,
                total = query.Count()
            };
            result = ErrorCode.SUCCESS(output, "", "cht");
            return result;

        }

        public void Update(ExpenditureModel entity)
        {
            entity.updated = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(a => a.created).IsModified = false;

        }


        public void Delete(ExpenditureModel entity)
        {
            throw new NotImplementedException();
        }

        public ExpenditureModel FindById(int id)
        {
            return _context.Set<ExpenditureModel>().AsNoTracking().FirstOrDefault(a => a.id == id);
        }
    }
}
