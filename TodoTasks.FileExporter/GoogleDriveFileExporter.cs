namespace TodoTasks.FileExporter
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Drive.v2;
    using Google.Apis.Drive.v2.Data;
    using Google.Apis.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    public static class GoogleDriveFileExporter 
    {
        private const string SERVICE_ACCOUNT_PKCS12_FILE_PATH = @"../../My Project-69d8888d8990.p12";
        private const string SERVICE_ACCOUNT_EMAIL = @"699545407731-a12caj72eb9hj9mafgckndk2l5dpo1av@developer.gserviceaccount.com";
        
        static DriveService BuildService()
        {
            X509Certificate2 certificate = 
                new X509Certificate2(SERVICE_ACCOUNT_PKCS12_FILE_PATH, "notasecret", X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(SERVICE_ACCOUNT_EMAIL)
                {
                    User = "georgipyaramov@gmail.com",
                    Scopes = new[] { DriveService.Scope.DriveFile }
                }.FromCertificate(certificate));

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API",
            });

            return service;
        }

        public static void UploadFile()
        {
            var service = GoogleDriveFileExporter.BuildService();

            File body = new File();
            body.Title = "My document";
            body.Description = "A test document";
            body.MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] byteArray = System.IO.File.ReadAllBytes("../../15.xlsx");
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

            FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, "text/plain");
            request.Upload();
        }
    }
}
