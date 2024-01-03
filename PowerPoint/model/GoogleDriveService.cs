using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
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

        // google
        private void CreateNewService(string applicationName, string clientSecretFileName)
        {
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

        // google
        private int UNIXNowTimeStamp
        {
            get
            {
                const int UNIX_START_YEAR = 1970;
                DateTime unixStartTime = new DateTime(UNIX_START_YEAR, 1, 1);
                return Convert.ToInt32((DateTime.Now.Subtract(unixStartTime).TotalSeconds));
            }
        }

        // google
        public async Task<string> UploadFile(string uploadFileName, string contentType, Action<IUploadProgress> uploadProgressEventHandeler = null, Action<Google.Apis.Drive.v2.Data.File> responseReceivedEventHandler = null)
        {
            FileStream uploadStream = new FileStream(uploadFileName, FileMode.Open, FileAccess.Read);
            this.CheckCredentialTimeStamp();
            string title = uploadFileName.Substring(uploadFileName.LastIndexOf(Constant.SPLASH) + 1);
            Google.Apis.Drive.v2.Data.File fileToInsert = new Google.Apis.Drive.v2.Data.File { Title = title };
            FilesResource.InsertMediaUpload insertRequest = _service.Files.Insert(fileToInsert, uploadStream, contentType);
            try
            {
                await insertRequest.UploadAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            uploadStream.Close();
            return insertRequest.ResponseBody.Id;
        }

        //public async Task<string> UploadFileMore(FilesResource.InsertMediaUpload insertRequest, FileStream uploadStream)
        //{
        //    try
        //    {
        //        await insertRequest.UploadAsync();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //    finally
        //    {
        //        uploadStream.Close();
        //    }
        //    return insertRequest.ResponseBody.Id;
        //}

        // google
        public void DownloadFile(string fileId, string fileName, Action<IDownloadProgress> downloadProgressChangedEventHandeler = null)
        {
            CheckCredentialTimeStamp();
            if (!String.IsNullOrEmpty(fileId))
            {
                var request = _service.Files.Get(fileId);
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    request.Download(stream);
                }
            }
        }

        // google
        private void CheckCredentialTimeStamp()
        {
            const int ONE_HOUR_SECOND = 3600;
            int nowTimeStamp = UNIXNowTimeStamp;

            if ((nowTimeStamp - _timeStamp) > ONE_HOUR_SECOND)
                this.CreateNewService(_applicationName, _clientSecretFileName);
        }

        // google
        public Google.Apis.Drive.v2.Data.File UpdateFile(string fileName, string fileId, string contentType)
        {
            CheckCredentialTimeStamp();
            try
            {
                Google.Apis.Drive.v2.Data.File file = _service.Files.Get(fileId).Execute();
                byte[] byteArray = System.IO.File.ReadAllBytes(fileName);
                MemoryStream stream = new MemoryStream(byteArray);
                FilesResource.UpdateMediaUpload request = _service.Files.Update(file, fileId, stream, contentType);
                request.NewRevision = true;
                request.Upload();

                Google.Apis.Drive.v2.Data.File updatedFile = request.ResponseBody;
                return updatedFile;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // google
        public void DeleteFile(string fileId)
        {
            CheckCredentialTimeStamp();
            try
            {
                _service.Files.Delete(fileId).Execute();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        
        private static readonly string[] SCOPES = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        private DriveService _service;
        private const int KB = 0x400;
        private const int DOWNLOAD_CHUNK_SIZE = 256 * KB;
        private int _timeStamp;
        private string _applicationName;
        private string _clientSecretFileName;
        private UserCredential _credential;
        const string USER = "user";
        const string CREDENTIAL_FOLDER = ".credential/";
    }
}