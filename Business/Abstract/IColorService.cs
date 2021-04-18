using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {
        IResult Add(Color color);
        IDataResult<List<Color>> GetAll();
        IDataResult<Color> GetById(int id);
        IDataResult<List<Color>> GetByColorNameLike(string colorName);
        IResult Update(Color color);
        IResult Delete(Color color);
    }
}
