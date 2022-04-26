using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.IRepositories
{
    public class CalculateRepository : ICalculateRepository
    {
        public int Sum(int i, int j)
        {
            return i + j;
        }
    }
}
