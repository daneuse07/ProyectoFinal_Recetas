using System;
using System.Collections.Generic;
using ChefRecetasCS.Models;
using MySql.Data.MySqlClient;

namespace ChefRecetasCS.DAL
{
    public class RecetaIngredienteDAL
    {
        public List<RecetaIngrediente> ObtenerTodos()
        {
            var lista =
                new List<RecetaIngrediente>();

            using var cn =
                Conexion.GetConexion();

            using var cmd =
                new MySqlCommand(
                    "SELECT * FROM vw_receta_ingredientes",
                    cn);

            cn.Open();

            using var dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(Mapear(dr));
            }

            return lista;
        }

        public List<RecetaIngrediente>
            ObtenerPorReceta(string criterio)
        {
            var lista =
                new List<RecetaIngrediente>();

            using var cn =
                Conexion.GetConexion();

            using var cmd =
                new MySqlCommand(
                    @"SELECT *
                      FROM vw_receta_ingredientes
                      WHERE receta LIKE @c",
                    cn);

            cmd.Parameters.AddWithValue(
                "@c",
                "%" + criterio + "%");

            cn.Open();

            using var dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(Mapear(dr));
            }

            return lista;
        }

        public Dictionary<string, int>
            ObtenerEstadisticas()
        {
            var datos =
                new Dictionary<string, int>();

            using var cn =
                Conexion.GetConexion();

            using var cmd =
                new MySqlCommand(
                    @"SELECT receta,
                             COUNT(*) total
                      FROM vw_receta_ingredientes
                      GROUP BY receta",
                    cn);

            cn.Open();

            using var dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                datos.Add(
                    dr["receta"].ToString(),
                    dr.GetInt32("total"));
            }

            return datos;
        }

        public RecetaIngrediente
            ObtenerDetalle(int id)
        {
            using var cn =
                Conexion.GetConexion();

            using var cmd =
                new MySqlCommand(
                    @"SELECT *
                      FROM vw_receta_ingredientes
                      WHERE id_ri=@id",
                    cn);

            cmd.Parameters.AddWithValue(
                "@id",
                id);

            cn.Open();

            using var dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                return Mapear(dr);
            }

            return null;
        }

        public void Insertar(
    RecetaIngrediente ri)
        {
            using var cn =
                Conexion.GetConexion();

            using var cmd =
                new MySqlCommand(
                @"INSERT INTO receta_ingrediente
        (
            id_receta,
            id_ingrediente,
            cantidad,
            unidad
        )
        VALUES
        (
            @r,
            @i,
            @c,
            @u
        )", cn);

            cmd.Parameters.AddWithValue(
                "@r",
                ri.IdReceta);

            cmd.Parameters.AddWithValue(
                "@i",
                ri.IdIngrediente);

            cmd.Parameters.AddWithValue(
                "@c",
                ri.Cantidad);

            cmd.Parameters.AddWithValue(
                "@u",
                ri.Unidad);

            cn.Open();

            cmd.ExecuteNonQuery();
        }

        public void EliminarPorReceta(
    int idReceta)
        {
            using var cn =
                Conexion.GetConexion();

            using var cmd =
                new MySqlCommand(
                @"DELETE FROM receta_ingrediente
          WHERE id_receta=@id", cn);

            cmd.Parameters.AddWithValue(
                "@id", idReceta);

            cn.Open();

            cmd.ExecuteNonQuery();
        }
        private RecetaIngrediente Mapear(
            MySqlDataReader dr)
        {
            return new RecetaIngrediente
            {
                IdRI =
                    dr.GetInt32("id_ri"),

                IdReceta =
                    dr.GetInt32("id_receta"),

                IdIngrediente =
                    dr.GetInt32("id_ingrediente"),

                Cantidad =
                    dr.GetDecimal("cantidad"),

                Unidad =
                    dr["unidad"].ToString(),

                Receta =
                    dr["receta"].ToString(),

                Ingrediente =
                    dr["ingrediente"].ToString()
            };
        }
    }
}