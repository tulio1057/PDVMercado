using Google.Cloud.Firestore;
using PDVMercado;
using PDVMercado;
using Microsoft.Extensions.Configuration;

namespace PDVMercado.Data
{
    public class FirebaseContext
    {
        private FirestoreDb _firestoreDb;
        private static FirebaseContext _instance;

        public FirestoreDb FirestoreDb => _firestoreDb;

        private FirebaseContext()
        {
            Initialize();
        }

        public static FirebaseContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FirebaseContext();
                return _instance;
            }
        }

        private void Initialize()
        {
            try
            {
                // Usando appsettings.json
                var config = Program.Configuration;
                string path = config["Firebase:CredentialPath"];
                string projectId = config["Firebase:ProjectId"];

                // Configurar variável de ambiente
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                _firestoreDb = FirestoreDb.Create(projectId);

                Console.WriteLine($"Firebase conectado! Projeto: {projectId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro Firebase: {ex.Message}");
                throw;
            }
        }
    }
}