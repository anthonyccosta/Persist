using Microsoft.Data.SqlClient;
using MongoDB.Driver;

namespace Repositories
{
    public class ControladordeConexaoDB
    {
        private static ControladordeConexaoDB instancia;
        private static readonly object syncOBJ = new object();

        private MongoClient mongoClient;
        private SqlConnection sqlConnection;

        private ControladordeConexaoDB()
        {
            mongoClient = new MongoClient("mongodb://root:Mongo%402024%23@localhost:27017");
            sqlConnection = new SqlConnection("Server=127.0.0.1; Database=DBRadar; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True");
        }
        public static ControladordeConexaoDB GetInstancia
        {
            get
            {
                lock (syncOBJ) //objeto garante que abra a instância apenas 1 vez
                {
                    if (instancia == null)
                    {
                        instancia = new ControladordeConexaoDB();
                    }
                    return instancia;
                }
            }
        }

        public MongoClient GetClientMongo()
        {
            if (mongoClient == null)
            {
                mongoClient = new MongoClient("mongodb://root:Mongo%402024%23@localhost:27017");
            }
            return mongoClient;
        }

        public SqlConnection GetConexaoSQL()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection("Server=127.0.0.1; Database=DBRadar; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True");
            }
            return sqlConnection;
        }
        public void CloseConexao()
        {
            mongoClient.Cluster.Dispose(); //Cluster.Dispose encerra a conexão, liberar recursos
            sqlConnection.Close();
        }

    }
}