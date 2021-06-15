using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BusinessLogic.Abstract
{
   public  interface ICarService
    {
        
        IDataResult<List<RentCarDetailsDto>> GetRentCarDetails();
        IDataResult<List<RentCarDetailsDto>> GetRentCarDetailsByBrandId(int brandId);
        IDataResult<List<RentCarDetailsDto>> GetRentCarDetailsByColorId(int colorId);
        IDataResult<List<Car>> GetCarsByBrandId(int Id);
        IDataResult<List<Car>> GetCarsByColorId(int Id);
        IDataResult<List<Car>> GetAll();
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<Car> GetById(int id);
    }
}
