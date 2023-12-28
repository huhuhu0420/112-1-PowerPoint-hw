using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;

namespace PowerPoint
{
    public class GoogleDriveService
    {
        public GoogleDriveService(string applicationName, string clientSecretFileName)
        {
            _applicationName = applicationName;
            _clientSecretFileName = clientSecretFileName;
            this.CreateNewService(applicationName, clientSecretFileName);
        }

        private void CreateNewService(string applicationName, string clientSecretFileName)
        {
            const string USER = "user";
            const string CREDENTIAL_FOLDER = ".credential/";
            UserCredential credential;

            using (FileStream stream = new FileStream(clientSecretFileName, FileMode.Open, FileAccess.Read))
            {
                string credentialPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credentialPath = Path.Combine(credentialPath, CREDENTIAL_FOLDER + applicationName);
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                    SCOPES, USER, CancellationToken.None, new FileDataStore(credentialPath, true)).Result;
            }

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            _credential = credential;
            DateTime now = DateTime.Now;
            _timeStamp = UNIXNowTimeStamp;
            _service = service;
        }

        private int UNIXNowTimeStamp
        {
            get
            {
                const int UNIX_START_YEAR = 1970;
                DateTime unixStartTime = new DateTime(UNIX_START_YEAR, 1, 1);
                return Convert.ToInt32((DateTime.Now.Subtract(unixStartTime).TotalSeconds));
            }
        }

        public Google.Apis.Drive.v2.Data.File UploadFile(string uploadFileName, string contentType, Action<IUploadProgress> uploadProgressEventHandeler = null, Action<Google.Apis.Drive.v2.Data.File> responseReceivedEventHandler = null)
        {
            FileStream uploadStream = new FileStream(uploadFileName, FileMode.Open, FileAccess.Read);
            const char SPLASH = '\\';
            string title = "";
            Debug.Print(uploadFileName);

            this.CheckCredentialTimeStamp();
            if (uploadFileName.LastIndexOf(SPLASH) != -1)
                title = uploadFileName.Substring(uploadFileName.LastIndexOf(SPLASH) + 1);
            else
                title = uploadFileName;

            Google.Apis.Drive.v2.Data.File fileToInsert = new Google.Apis.Drive.v2.Data.File { Title = title };
            FilesResource.InsertMediaUpload insertRequest = _service.Files.Insert(fileToInsert, uploadStream, contentType);
            insertRequest.ChunkSize = FilesResource.InsertMediaUpload.MinimumChunkSize * 2;

            if (uploadProgressEventHandeler != null)
                insertRequest.ProgressChanged += uploadProgressEventHandeler;


            if (responseReceivedEventHandler != null)
                insertRequest.ResponseReceived += responseReceivedEventHandler;

            try
            {
                insertRequest.Upload();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                uploadStream.Close();
            }

            return insertRequest.ResponseBody;
        }
        private void CheckCredentialTimeStamp()
        {
            const int ONE_HOUR_SECOND = 3600;
            int nowTimeStamp = UNIXNowTimeStamp;

            if ((nowTimeStamp - _timeStamp) > ONE_HOUR_SECOND)
                this.CreateNewService(_applicationName, _clientSecretFileName);
        }
 
        private static readonly string[] SCOPES = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        private DriveService _service;
        private const int KB = 0x400;
        private const int DOWNLOAD_CHUNK_SIZE = 256 * KB;
        private int _timeStamp;
        private string _applicationName;
        private string _clientSecretFileName;
        private UserCredential _credential;

    }
}