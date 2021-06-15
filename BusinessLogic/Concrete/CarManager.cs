using BusinessLogic.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using System.Linq.Expressions;
using System.Linq;
using Entities.DTOs;
using Core.Utilities.Results;
using BusinessLogic.Constants;
using DataAccess.Concrete.EntityFramework;
using FluentValidation;
using BusinessLogic.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Aspects.Autofac.Validation;
using BusinessLogic.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;

namespace BusinessLogic.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("Car.Admin")]
        [CacheRemoveAspect("ICarService.Get")]
        [PerformanceAspect(5)]
        [TransactionScopeAspect]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult("Kayıt başarılı!!");

        }
        [PerformanceAspect(5)]
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new Result(true, "Silme Başarılı");

        }
        [PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }
        [PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.GetById(p => p.Id == id));
        }
        [PerformanceAspect(5)]
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Car car)
        {
            if (!(car.DailyPrice > 0))
            {
                return new Result(false, "Günlük ücret belirtilmedi!!");
            }
            return new Result(true, "Araç Eklendi");
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetCarsByBrandId(int Id)

        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().Where(p => p.BrandId == Id).ToList());
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetCarsByColorId(int Id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().Where(p => p.ColorId == Id).ToList());
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<RentCarDetailsDto>> GetRentCarDetails()
        {
            return new SuccessDataResult<List<RentCarDetailsDto>>(CheckIfCarImageNull());
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<RentCarDetailsDto>> GetRentCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<RentCarDetailsDto>>(CheckIfCarImageNull(p=>p.BrandId==brandId));
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<RentCarDetailsDto>> GetRentCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<RentCarDetailsDto>>(CheckIfCarImageNull(p => p.ColorId == colorId));
        }

        private List<RentCarDetailsDto> CheckIfCarImageNull(Expression<Func<RentCarDetailsDto, bool>> filter = null)
        {

            string path = @"/images/default.jpg";
            List<RentCarDetailsDto> result = _carDal.GetRentCarDetails(filter);
            foreach (var item in result)
            {
                if (item.CarImages.Count == 0)
                {
                    item.CarImages = new List<CarImage>{
                        new CarImage{CarId = item.Id,ImagePath = path,DateTime = DateTime.Now }
                    };
                }
            }
            return result;
        }
    }
}
