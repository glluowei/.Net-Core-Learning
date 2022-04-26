using SwiftCode.BBS.IRepositories;
using SwiftCode.BBS.IServices;

namespace SwiftCode.BBS.Services
{
    public class CalculateService: ICalculateService
    {
        ICalculateRepository _calculateRepository = new CalculateRepository();

        public int Sum(int i, int j)
        {
            return _calculateRepository.Sum(i,j);
        }
    }
}
