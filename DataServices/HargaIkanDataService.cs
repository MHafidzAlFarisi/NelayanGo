using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NelayanGo.Models;
using Npgsql;
using System.Collections.Generic;

namespace NelayanGo.DataServices
{
    public class HargaIkanDataService
    {
        private const string ConnectionString =
            "Host=localhost;Username=postgres_user;Password=postgres_password;Database=nelayan_go_db";

        public List<HargalkanModel> GetAll()
        {
            var result = new List<HargalkanModel>();
            // TODO: SELECT * FROM TBL_HARGA_IKAN + mapping ke model
            return result;
        }

        public void Insert(HargalkanModel model) { /* TODO: INSERT */ }

        public void Update(HargalkanModel model) { /* TODO: UPDATE */ }

        public void Delete(string id) { /* TODO: DELETE */ }
    }
}


