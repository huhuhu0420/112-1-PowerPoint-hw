using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    [TestClass]
    public class GoogleDriveServiceTests
    {
        private GoogleDriveService _service;
        private Mock<DriveService> _mockDriveService;
        private Mock<UserCredential> _mockCredential;
        private Mock<FilesResource> _mockFilesResource;
        private Mock<FilesResource.InsertMediaUpload> _mockInsertRequest;
        private Mock<FilesResource.GetRequest> _mockGetRequest;
        private Mock<FilesResource.DeleteRequest> _mockDeleteRequest;
        private Mock<FilesResource.UpdateMediaUpload> _mockUpdateRequest;
        private const string TEST_FILE_NAME = "IHATEUNITTEST.txt";

        // test
        [TestInitialize]
        public void Setup()
        {
            // _mockDriveService = new Mock<DriveService>(new BaseClientService.Initializer());
            // _mockCredential = new Mock<UserCredential>();
            // _mockFilesResource = new Mock<FilesResource>(_mockDriveService.Object);
            // _mockInsertRequest = new Mock<FilesResource.InsertMediaUpload>(_mockDriveService.Object, new Google.Apis.Drive.v2.Data.File(), new MemoryStream());
            // _mockGetRequest = new Mock<FilesResource.GetRequest>(_mockDriveService.Object, "fileId");
            // _mockDeleteRequest = new Mock<FilesResource.DeleteRequest>(_mockDriveService.Object, "fileId");
            // _mockUpdateRequest = new Mock<FilesResource.UpdateMediaUpload>(_mockDriveService.Object, new Google.Apis.Drive.v2.Data.File(), "fileId", new MemoryStream(), "contentType");

            const string APPLICATION_NAME = "DrawAnyWhere";
            const string CLIENT_SECRET_FILE_NAME = "clientSecret.json";
            _service = new GoogleDriveService(APPLICATION_NAME, CLIENT_SECRET_FILE_NAME);
        }

        // test
        [TestMethod]
        public async Task UploadFile_CallsInsertAsync()
        {
            Task<string> task = _service.UploadFile(TEST_FILE_NAME, Constant.TEXT_PLAIN);
            await task;
            Assert.IsNotNull(task.Result);
            _service.DeleteFile(task.Result);
        }

        
        // test
        [TestMethod]
        public async Task DownloadFile_CallsDownload()
        {
            Task<string> task = _service.UploadFile(TEST_FILE_NAME, Constant.TEXT_PLAIN);
            await task;
            _service.DownloadFile(task.Result, TEST_FILE_NAME);
            _service.DeleteFile(task.Result);
            Assert.IsTrue(File.Exists(TEST_FILE_NAME));
        }

        // test
        [TestMethod]
        public async Task DeleteFile_CallsExecute()
        {
            Task<string> task = _service.UploadFile(TEST_FILE_NAME, Constant.TEXT_PLAIN);
            await task;
            _service.DeleteFile(task.Result);
        }

        // test
        [TestMethod]
        public async Task UpdateFile_CallsUpload()
        {
            Task<string> task = _service.UploadFile(TEST_FILE_NAME, Constant.TEXT_PLAIN);
            await task;
            _service.UpdateFile(TEST_FILE_NAME, task.Result, Constant.TEXT_PLAIN);
            Assert.IsNotNull(task.Result);
            _service.DeleteFile(task.Result);
        }
    }
}