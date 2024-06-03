using Microsoft.Data.SqlClient;
using Model;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Repositories
{

    public class PersistRepository
    {
        string Connection = "Server=127.0.0.1; Database=DBRadar; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True";
        SqlConnection conn;

        public PersistRepository()
        {
            conn = new SqlConnection(Connection);
            conn.Open();
        }

        public bool Insert(Radar radar)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(Radar.INSERT, conn);
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
                string DataDaInativacaoString = radar.data_Inativacao != null ? string.Join(",", radar.data_Inativacao) : null;
                command.Parameters.AddWithValue("@data_da_inativacao", radar.data_Inativacao);
                command.Parameters.AddWithValue("@latitude", radar.latitude);
                command.Parameters.AddWithValue("@longitude", radar.longitude);
                command.Parameters.AddWithValue("@velocidade_leve", radar.velocidade_Leve);
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        public bool Update(Radar radar)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(Radar.UPDATE, conn);
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
                result = true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(Radar.DELETE, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));
                if (command.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }


        public List<Radar> GetAll()
        {
            List<Radar> radares = new List<Radar>();
            StringBuilder sb = new StringBuilder();
            sb.Append(Radar.GETALL);
            try
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Radar radar = new Radar();

                    radar.Id = reader.GetInt32(0);
                    radar.concessionaria = reader.GetString(1);
                    radar.ano_PNV_SNV = reader.GetString(2);
                    radar.tipo_Radar = reader.GetString(3);
                    radar.rodovia = reader.GetString(4);
                    radar.uf = reader.GetString(5);
                    radar.km_m = reader.GetString(6);
                    radar.municipio = reader.GetString(7);
                    radar.tipo_Pista = reader.GetString(8);
                    radar.sentido = reader.GetString(9);
                    radar.situacao = reader.GetString(10);
                    radar.data_Inativacao = reader.GetString(11)?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    radar.latitude = reader.GetString(12);
                    radar.longitude = reader.GetString(13);
                    radar.velocidade_Leve = reader.GetString(14);
                    radares.Add(radar);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return radares;
        }
        public Radar Get(int id)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(Radar.GET);
            Radar radar = new();

            try
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conn);
                command.Parameters.Add(new SqlParameter("id", id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    radar.Id = reader.GetInt32(0);
                    radar.concessionaria = reader.GetString(1);
                    radar.ano_PNV_SNV = reader.GetString(2);
                    radar.tipo_Radar = reader.GetString(3);
                    radar.rodovia = reader.GetString(4);
                    radar.uf = reader.GetString(5);
                    radar.km_m = reader.GetString(6);
                    radar.municipio = reader.GetString(7);
                    radar.tipo_Pista = reader.GetString(8);
                    radar.sentido = reader.GetString(9);
                    radar.situacao = reader.GetString(10);
                    radar.data_Inativacao = reader.GetString(11)?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    radar.latitude = reader.GetString(12);
                    radar.longitude = reader.GetString(13);
                    radar.velocidade_Leve = reader.GetString(14);

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return radar;

        }

        public static void GenerateXML(List<Radar> lista)
        {
            if (lista.Count > 0)
            {
                var ListaRadar = new XElement("Root", from data in lista
                                                      select new XElement("radar",
                                                      new XElement("concessionaria", data.concessionaria),
                                                      new XElement("ano_do_pnv_snv", data.ano_PNV_SNV),
                                                      new XElement("tipo_de_radar", data.tipo_Radar),
                                                      new XElement("rodovia", data.rodovia),
                                                      new XElement("uf", data.uf),
                                                      new XElement("km_m", data.km_m),
                                                      new XElement("municipio", data.municipio),
                                                      new XElement("tipo_pista", data.tipo_Pista),
                                                      new XElement("sentido", data.sentido),
                                                      new XElement("situacao", data.situacao),
                                                      new XElement("data_da_inativacao", data.data_Inativacao),
                                                      new XElement("latitude", data.latitude),
                                                      new XElement("longitude", data.longitude),
                                                      new XElement("velocidade_leve", data.velocidade_Leve)
                                                      ));
                Console.WriteLine(ListaRadar);
            }
            else
            {
                Console.WriteLine("Não existe registro");
            }
        }
        static void GenerateJson(List<Radar> listaRadar, string JsonSalvo)
        {
            string json = JsonSerializer.Serialize(listaRadar, new JsonSerializerOptions { WriteIndented = true }); //WriteIndented: saída com identação, formatação.
            File.WriteAllText(JsonSalvo, json);
            Console.WriteLine($"Arquivo {JsonSalvo} criado com sucesso.");
        }
    }

}