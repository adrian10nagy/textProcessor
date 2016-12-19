using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BL.Business
{
    public interface IKit : IDisposable
    {
        WordBL WordBL { get; }
        UserBL UserBL { get; }
    }

    public class KitBL : IKit
    {
        private static KitBL _instance = new KitBL();
        public static KitBL Instance { get { return _instance ?? getInstance(); } }

        static KitBL getInstance()
        {
            return new KitBL();
        }

        public WordBL WordBL { get { return new WordBL(); } }
        public UserBL UserBL { get { return new UserBL(); } }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
