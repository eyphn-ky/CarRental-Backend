using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<RentCarContext, Car>, ICarDal
    {
        public List<RentCarDetailsDto> GetRentCarDetails(Expression<Func<RentCarDetailsDto, bool>> filter = null)
        {
            using (RentCarContext context = new RentCarContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             select new RentCarDetailsDto
                             {
                                 BrandName = b.Name,
                                 ColorName = co.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 BrandId=b.Id,
                                 ColorId=co.Id,
                                 Id = c.Id,
                                 Name = c.Name,
                                 CarImages = (from ca in context.CarImages 
                                              where ca.CarId == c.Id
                                              select new CarImage {
                                              Id = ca.Id,
                                              CarId = ca.CarId,
                                              DateTime = ca.DateTime,
                                              ImagePath = ca.ImagePath }).ToList()
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
   
      
    }
}
