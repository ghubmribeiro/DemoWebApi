using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWebApi.Controllers
{
    public class FilmesController : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: api/Filmes
        public List<Filme> Get()
        {
            var lista = from Filme in dc.Filmes orderby Filme.Titulo select Filme;

            return lista.ToList();
        }

        // GET: api/Filmes/5
        public IHttpActionResult Get(int id)
        {
            var filme = dc.Filmes.SingleOrDefault(f => f.Id == id);

            if (filme != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, filme));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Filme não existe."));
        }

        // POST: api/Filmes
        public IHttpActionResult Post([FromBody]Filme novoFilme)
        {
            Filme filme = dc.Filmes.FirstOrDefault(f => f.Id == novoFilme.Id);

            if(filme != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict, "Já existe um filme registado com esse Id."));
            }

            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == novoFilme.Categoria);

            if(categoria == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Não existe ainda essa categoria, vá a categorias primeiro."));
            }

            dc.Filmes.InsertOnSubmit(novoFilme);

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }

        // PUT: api/Filmes/5
        public IHttpActionResult Put([FromBody]Filme filmeAlterado)
        {
            Filme filme = dc.Filmes.FirstOrDefault(f => f.Id == filmeAlterado.Id);

            if (filme == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Não existe nenhum filme com esse Id para poder alterar."));
            }

            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == filmeAlterado.Categoria);

            if (categoria == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Não existe ainda essa categoria, vá a categorias primeiro."));
            }

            filme.Titulo = filmeAlterado.Titulo;
            filme.Categoria = filmeAlterado.Categoria;

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }

        // DELETE: api/Filmes/5
        public IHttpActionResult Delete(int id)
        {
            Filme filme = dc.Filmes.FirstOrDefault(f => f.Id == id);

            if(filme != null)
            {
                dc.Filmes.DeleteOnSubmit(filme);

                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Não existe um filme com esse Id para poder eliminar."));
        }
    }
}
