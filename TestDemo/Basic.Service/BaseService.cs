using Basic.IRepository;

namespace Basic.Service
{
    public class BaseService:IBaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _unitOfWork.BeginTran();
        }
    }

    public interface IBaseService
    {

    }
}