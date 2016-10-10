using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public interface IKit : IDisposable
    {
        Words Words { get; }
        Definitions Definitions { get; }
        Users Users { get; }
    }

    public class Kit : IKit
    {
        private static Kit _instance = new Kit();
        public static Kit Instance { get { return _instance ?? getInstance(); } }

        static Kit getInstance()
        {
            return new Kit();
        }

        public Words Words { get { return new Words(); } }
        public WordInflections WordInflections { get { return new WordInflections(); } }
        public Definitions Definitions { get { return new Definitions(); } }
        public Users Users { get { return new Users(); } }
        public WordSinonims WordSinonims { get { return new WordSinonims(); } }
        

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
