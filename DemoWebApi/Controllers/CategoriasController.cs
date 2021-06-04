using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWebApi.Controllers
{
    public class CategoriasController : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: api/Categorias
        public List<Categoria> Get()
        {
            var lista = from Categoria in dc.Categorias select Categoria;

            return lista.ToList();
        }

        // GET: api/Categorias/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Categorias
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Categorias/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Categorias/5
        public void Delete(int id)
        {
        }
    }
}
