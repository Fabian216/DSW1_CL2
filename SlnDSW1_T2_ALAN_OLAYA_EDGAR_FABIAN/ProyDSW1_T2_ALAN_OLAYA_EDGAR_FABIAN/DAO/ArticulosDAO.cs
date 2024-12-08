using ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.Models;

namespace ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.DAO
{
    public class ArticulosDAO
    {

        private string cad_cn;

        public ArticulosDAO(IConfiguration cfg)
        {
            cad_cn = cfg.GetConnectionString("cn1")!;
        }

        public List<Articulos> GetArticulos()
        {
            var lista = new List<Articulos>();
            //
            var dr = SqlHelper.ExecuteReader(cad_cn, "PA_LISTAR_ARTICULOS");
            //
            while (dr.Read())
            {
                lista.Add(new Articulos()
                {
                    nom_art = dr.GetString(0),
                    pre_art = dr.GetDecimal(1),
                    cod_art = dr.GetString(2),            
                    stk_art = dr.GetInt32(3)                  
                });
            }
            //
            return lista;
        }

        public Articulos BuscarArticulo(string codigo)
        {
            var buscado = GetArticulos().Find(p =>
                          p.cod_art.Equals(codigo));
            //
            return buscado;
        }


        public List<Articulos> GetArticulosPorFiltro(string filtro)
        {
            var lista = new List<Articulos>();
            //
            var dr = SqlHelper.ExecuteReader(cad_cn, "PA_LISTAR_ARTICULOS_POR_NOMBRE", filtro);
            //
            while (dr.Read())
            {
                lista.Add(new Articulos()
                {
                    nom_art = dr.GetString(0),
                    pre_art = dr.GetDecimal(1),
                    cod_art = dr.GetString(2),
                    stk_art = dr.GetInt32(3)
                });
            }
            //
            return lista;
        }



    }
}
