using Microsoft.Data.SqlClient;
using Models;
using MongoDB.Driver;


namespace Repositories
{
    public class MongoDB
    {
        static string sqlConnectionString = "Server=127.0.0.1; Database=DBRadar; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True";
        static string mongoConnectionString = "mongodb://root:Mongo%402024%23@localhost:27017";
        static string mongoDatabaseName = "DBRadar";
        static string mongoCollectionName = "Radar";
        static SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);

        private IMongoCollection<Radar> colecao;

        public static void TransferirDadosParaMongo()
        {
            try
            {
                var mongoClient = new MongoClient(mongoConnectionString);
                var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);
                var mongoCollection = mongoDatabase.GetCollection<Radar>(mongoCollectionName);

                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    sqlConnection.Open();
                    string sqlQuery = "SELECT concessionaria, ano_do_pnv_snv, tipo_de_radar, rodovia, uf, km_m, municipio, tipo_pista, sentido, situacao, data_da_inativacao, latitude, longitude, velocidade_leve FROM Radar";

                    using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var radar = new Radar
                            {
                                concessionaria = reader["concessionaria"].ToString(),
                                ano_PNV_SNV = reader["ano_do_pnv_snv"].ToString(),
                                tipo_Radar = reader["tipo_de_radar"].ToString(),
                                rodovia = reader["rodovia"].ToString(),
                                uf = reader["uf"].ToString(),
                                km_m = reader["km_m"].ToString(),
                                municipio = reader["municipio"].ToString(),
                                tipo_Pista = reader["tipo_pista"].ToString(),
                                sentido = reader["sentido"].ToString(),
                                situacao = reader["situacao"].ToString(),
                                data_Inativacao = reader["data_da_inativacao"].ToString().Split(',').ToList(),
                                latitude = reader["latitude"].ToString(),
                                longitude = reader["longitude"].ToString(),
                                velocidade_Leve = reader["velocidade_leve"].ToString()
                            };


                            mongoCollection.InsertOne(radar);
                        }
                    }
                }

                Console.WriteLine("Dados transferidos para o DBMongo com sucesso.");
            }
            catch (Exception e)
            {

                Console.WriteLine("Dados não tranferidos, erro: " + e.Message);
            }
        }
        public void ManipularMongo(IMongoDatabase BD)
        {
            colecao = BD.GetCollection<Radar>("Radar");

        }
        public List<Radar> RecuperarMongo()
        {
            return colecao.Find(radar => true).ToList();
        }

    }
}