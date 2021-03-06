using BusinessLogic.Abstract;
using BusinessLogic.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BusinessLogic.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }


        public IResult Add(Brand brand)
        {
            if(brand.Name.Length>=2)
            {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.BrandAdded);
            }
            return new ErrorResult(Messages.BrandNameInvalid);
        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
           
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.BrandListed);
        }

        public IDataResult<Brand> GetById(Expression<Func<Brand, bool>> filter)
        {
           
            return new SuccessDataResult<Brand>(_brandDal.GetById(filter),Messages.BrandListed);
        }

        public IResult Update(Brand brand)
        {
            if (brand.Name.Length >= 2)
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.BrandUpdated);
            }
            return new ErrorResult(Messages.BrandNameInvalid);
          

        }
    }
}
