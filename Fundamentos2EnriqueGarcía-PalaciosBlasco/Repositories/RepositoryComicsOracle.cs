using Fundamentos2EnriqueGarcía_PalaciosBlasco.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using System.Data;

namespace Fundamentos2EnriqueGarcía_PalaciosBlasco.Repositories
{
    #region SP_INSERTORACLE_COMICS
    //    create or replace procedure SP_INSERTORACLE_COMICS(P_IDCOMIC COMICS.IDCOMIC%TYPE, P_NOMBRE COMICS.NOMBRE%TYPE, P_IMAGEN COMICS.IMAGEN%TYPE,
    //P_DESCRIPCION COMICS.DESCRIPCION%TYPE)
    //AS
    //BEGIN
    //  INSERT INTO COMICS VALUES(p_idcomic, p_nombre, p_imagen, p_descripcion);
    //    END;
    #endregion
    public class RepositoryComicsOracle : IRepositoryComics
    {
        private OracleConnection cn;
        private OracleCommand com;
        private OracleDataAdapter adapter;
        private DataTable tablaComcis;
        public RepositoryComicsOracle()
        {
            string sql = "Select * from COMICS";
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True;User Id=SYSTEM;Password=ORACLE";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = cn;
            this.adapter = new OracleDataAdapter(sql, connectionString);
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
                            select datos).Max(x => x.Field<int>("IDCOMIC")) +1;
            return maximo;
        }

        public void InsertComics(Comic comic)
        {
            int maximo = this.GetMaximoIdComic();
            
            OracleParameter pamid = new OracleParameter(":P_IDCOMIC",maximo);
            OracleParameter pamnombre = new OracleParameter(":P_NOMBRE", comic.Nombre);
            OracleParameter pamimagen = new OracleParameter(":P_DESCRIPCION", comic.Imagen);
            OracleParameter pamdescripcion = new OracleParameter(":P_IMAGEN", comic.Descripcion);
            

            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamimagen);
            this.com.Parameters.Add(pamdescripcion);
            

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERTORACLE_COMICS";

            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}

