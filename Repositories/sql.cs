
using Microsoft.Data.SqlClient;
using Models;
using MongoDB.Driver;
using System.Text;
using static Models.Radar;


namespace Repositories
{
    public class SQL
    {
        static string connectionString = "Server=127.0.0.1; Database=DBRadar; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True";
        public static void InsertDBRadar(List<Radar> radares)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Radar (concessionaria, ano_do_pnv_snv, tipo_de_radar, rodovia, uf, km_m, municipio, tipo_pista, sentido, situacao, data_da_inativacao, latitude, longitude, velocidade_leve)";
                foreach (Radar radar in radares)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.Parameters.AddWithValue("@concessionaria", radar.concessionaria);
                        command.Parameters.AddWithValue("@ano_do_pnv_snv", radar.ano_PNV_SNV);
                        command.Parameters.AddWithValue("@tipo_de_radar", radar.tipo_Radar);
                        command.Parameters.AddWithValue("@rodovia", radar.rodovia);
                        command.Parameters.AddWithValue("@uf", radar.uf);
                        command.Parameters.AddWithValue("@km_m", radar.km_m);
                        command.Parameters.AddWithValue("@municipio", radar.municipio);
                        command.Parameters.AddWithValue("@tipo_pista", radar.tipo_Pista);
                        command.Parameters.AddWithValue("@sentido", radar.sentido);
                        command.Parameters.AddWithValue("@situacao", radar.situacao);
                        command.Parameters.AddWithValue("@data_da_inativacao", radar.data_Inativacao);
                        command.Parameters.AddWithValue("@latitude", radar.latitude);
                        command.Parameters.AddWithValue("@longitude", radar.longitude);
                        command.Parameters.AddWithValue("@velocidade_leve", radar.velocidade_Leve);
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }
        public List<Radar> RecuperarSQL()
        {
            List<Radar> radares = new List<Radar>();
            string consultaSQL = "SELECT * FROM Radar";

            using (SqlCommand comando = new SqlCommand(consultaSQL))
            {
                using (SqlDataReader leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        Radar radar = new Radar();
                        radar.Id = leitor.GetInt32(0);
                        radar.concessionaria = leitor.GetString(1);
                        radar.ano_PNV_SNV = leitor.GetString(2);
                        radar.tipo_Radar = leitor.GetString(3);
                        radar.rodovia = leitor.GetString(4);
                        radar.uf = leitor.GetString(5);
                        radar.km_m = leitor.GetString(6);
                        radar.municipio = leitor.GetString(7);
                        radar.tipo_Pista = leitor.GetString(8);
                        radar.sentido = leitor.GetString(9);
                        radar.situacao = leitor.GetString(10);
                        radar.data_Inativacao = leitor.GetString(11)?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        radar.latitude = leitor.GetString(12);
                        radar.longitude = leitor.GetString(13);
                        radar.velocidade_Leve = leitor.GetString(14);
                        radares.Add(radar);
                    }
                }
            }
            return radares;
        }
    }
}
