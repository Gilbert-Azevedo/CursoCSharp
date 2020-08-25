using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web.Http;
using Modelo;

namespace LabAPI.Controllers
{
    public class MedicaoController : ApiController
    {
        // GET: api/Medicao
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Medicao/5
        public List<Medicao> Get(int id)
        {
            return Modelo.MedicaoDAL.Abrir(id);
        }

        // POST: api/Medicao
        public void Post([FromBody]string value)
        {
            Medicao m = JsonSerializer.Deserialize<Medicao>(value);
            Modelo.MedicaoDAL.Salvar(m);
        }

        // PUT: api/Medicao/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Medicao/5
        public void Delete(int id)
        {
        }
    }
}
