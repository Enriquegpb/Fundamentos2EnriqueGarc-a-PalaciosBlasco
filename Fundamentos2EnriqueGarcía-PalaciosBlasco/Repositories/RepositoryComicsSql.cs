using Fundamentos2EnriqueGarcía_PalaciosBlasco.Models;
using System.Data;
using System.Data.SqlClient;

namespace Fundamentos2EnriqueGarcía_PalaciosBlasco.Repositories
{
    #region SP_INSERTARSQL_COMIC
    //    CREATE PROCEDURE SP_INSERTARSQL_COMIC(@IDCOMIC INT, @NOMBRE NVARCHAR(50), @IMAGEN NVARCHAR(50), @DESCRIPCION NVARCHAR(50))
    //AS
    //INSERT INTO COMICS VALUES(@IDCOMIC, @NOMBRE, @IMAGEN, @DESCRIPCION)
    //GO
    #endregion
    public class RepositoryComicsSql: IRepositoryComics
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataAdapter adapter;
        private DataTable tablaComcis;
        public RepositoryComicsSql()
        {
            string sql = "Select * from COMICS";
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
            this.adapter = new SqlDataAdapter(sql, connectionString);
            this.tablaComcis = new DataTable();
            this.adapter.Fill(tablaComcis);
        }

        public List<Comic> GetComics()
        {
            var consulta = from datos in this.tablaComcis.AsEnumerable()
                           select new Comic
                           {
                               IdComic = datos.Field<int>("IDCOMIC"),
                               Nombre = datos.Field<string>("NOMBRE"),
                               Imagen = datos.Field<string>("IMAGEN"),
                               Descripcion = datos.Field<string>("DESCRIPCION")
                           };

            return consulta.ToList();
        }

        public int GetMaximoIdComic()
        {
            int maximo = (from datos in this.tablaComcis.AsEnumerable()
                          select datos).Max(x => x.Field<int>("IDCOMIC")) + 1;
            return maximo;
        }

        public void InsertComics(Comic comic)
        {
            int maximo = this.GetMaximoIdComic();

            SqlParameter pamid = new SqlParameter("@IDCOMIC", maximo);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", comic.Nombre);
            SqlParameter pamimagen = new SqlParameter("@DESCRIPCION", comic.Imagen);
            SqlParameter pamdescripcion = new SqlParameter("@IMAGEN", comic.Descripcion);


            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamimagen);
            this.com.Parameters.Add(pamdescripcion);


            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERTARSQL_COMIC";

            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
