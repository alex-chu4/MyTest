using EMIC2.Models;
using EMIC2.Models.Interface;
using EMIC2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Utility.Helper;
using Utility.Model;
using WebDav;

namespace Utility.EMIC2
{
    public enum FileNameTypeEnum
    {
        GUID_NAME,
        ORIGINAL_NAME
    }

    public class FileService : Controller
    {
        private IRepository<COM2_FILES_UPLOAD> repository = new GenericRepository<COM2_FILES_UPLOAD>();
        private IUnitOfWork unitOfWork = new UnitOfWork();

        [Obsolete]
        public ReturnModel SetFilebyByteArray(byte[] byteArray, string fileName, string sysCode, string uploadUser)
        {
            ReturnModel result = new ReturnModel();
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["fileServerPath"] == null)
                {
                    result.Data = null;
                    result.Message = "上傳失敗，fileServerPath不存在!!";
                    return result;
                }

                string filePath = System.Configuration.ConfigurationManager.AppSettings["fileServerPath"]?.ToString() + DateTime.Now.ToString("yyyyMMdd");

                Directory.CreateDirectory(filePath);

                // 副檔名
                string fileExtension = System.IO.Path.GetExtension(fileName);
                // 新檔名
                string fileId = Guid.NewGuid().ToString("N") + fileExtension;

                var path = System.IO.Path.Combine(filePath, fileId);

                using (var filestream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    filestream.Write(byteArray, 0, byteArray.Length);
                    return SaveDbChange(fileName, fileId, fileExtension, sysCode, uploadUser);
                }

                ReturnModel SaveDbChange(string _filename, string _fileId, string _fileExtension, string _sysCode, string _uploadUser)
                {
                    var localresult = new ReturnModel();
                    DateTime timeNow = DateTime.Now;
                    COM2_FILES_UPLOAD fileUpload = new COM2_FILES_UPLOAD();

                    fileUpload.FILE_ID = _sysCode + timeNow.ToString("yyyyMMddhhmmss");
                    fileUpload.SYS_CODE = _sysCode;
                    fileUpload.UPLOAD_FILE_NAME = _filename;
                    fileUpload.FILE_EXTENSION = _fileExtension;
                    fileUpload.NEW_FILE_NAME = _fileId;
                    fileUpload.FILE_PATH = path;
                    fileUpload.SERVER_ID = null;
                    fileUpload.UPLOAD_USER_ID = uploadUser;
                    fileUpload.UPLOAD_TIME = timeNow;

                    if (repository.Create(fileUpload))
                    {
                        localresult.Status = true;
                        localresult.Message = fileUpload.FILE_ID;
                        localresult.Data = fileUpload.FILE_ID;
                    }
                    else
                    {
                        localresult.Status = false;
                        localresult.Message = "資料庫新增失敗";
                        localresult.Data = null;
                    }

                    return localresult;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Message = ex.Message;
                return result;
            }
        }

        [Obsolete]
        public string SetFile(HttpPostedFileBase file, string sysCode, string uploadUser)
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["fileServerPath"] == null)
                {
                    return "上傳失敗，fileServerPath不存在!!";
                }

                string filePath = System.Configuration.ConfigurationManager.AppSettings["fileServerPath"]?.ToString() + DateTime.Now.ToString("yyyyMMdd");

                Directory.CreateDirectory(filePath);

                // 檔名
                string fileName = file.FileName;
                // 副檔名
                string fileExtension = System.IO.Path.GetExtension(fileName);
                // 新檔名
                string fileId = Guid.NewGuid().ToString("N") + fileExtension;

                var path = System.IO.Path.Combine(filePath, fileId);

                DateTime timeNow = DateTime.Now;

                COM2_FILES_UPLOAD fileUpload = new COM2_FILES_UPLOAD();

                fileUpload.FILE_ID = sysCode + timeNow.ToString("yyyyMMddhhmmss");
                fileUpload.SYS_CODE = sysCode;
                fileUpload.UPLOAD_FILE_NAME = fileName;
                fileUpload.FILE_EXTENSION = fileExtension;
                fileUpload.NEW_FILE_NAME = fileId;
                fileUpload.FILE_PATH = path;
                fileUpload.SERVER_ID = null;
                fileUpload.UPLOAD_USER_ID = uploadUser;
                fileUpload.UPLOAD_TIME = timeNow;

                if (repository.Create(fileUpload))
                {
                    file.SaveAs(path);
                    return fileUpload.FILE_ID;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }

            //return null;
            return "資料庫新增失敗";
        }

        [Obsolete]
        public FileStreamResult GetFile(string fileID)
        {
            try
            {
                var file = repository.Get(x => x.FILE_ID == fileID);

                if (file != null)
                {
                    //檔案位置
                    string filepath = file.FILE_PATH;
                    //檔案名稱
                    string filename = file.UPLOAD_FILE_NAME;
                    //讀成串流
                    Stream iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    //回傳出檔案
                    return File(iStream, "application/zip", filename);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 透過WebAPI轉檔
        /// </summary>
        /// <param name="url">WebAPI的URL</param>
        /// <param name="content">檔案內容</param>
        /// <returns>
        /// 檔案轉檔成功，則
        ///     Status = true
        ///     Data = 轉檔後的檔案內容
        /// 否則
        ///     Status = false
        ///     Message = 錯誤訊息
        /// </returns>
        public ReturnModel<byte[]> ConvertTo(string url, byte[] content)
        {
            ReturnModel<byte[]> returnModel = new ReturnModel<byte[]>();
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "post";
            webRequest.ContentType = "application/octet-stream";
            webRequest.GetRequestStream().Write(content, 0, content.Length);

            using (HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse)
            {
                int statusCode = (int)webResponse.StatusCode;
                if ((statusCode >= 200) && (statusCode < 300))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        webResponse.GetResponseStream().CopyTo(memoryStream);
                        returnModel.Data = memoryStream.ToArray();
                        returnModel.Status = true;
                    }
                }
                else
                {
                    using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        returnModel.Message = streamReader.ReadToEnd();
                    }
                }
            }

            return returnModel;
        }

        /// <summary>
        /// 透過WebAPI轉檔
        /// </summary>
        /// <param name="url">WebAPI的URL</param>
        /// <param name="inStream">檔案的Stream</param>
        /// <returns>
        /// 檔案轉檔成功，則
        ///     Status = true
        ///     Data = 轉檔後的檔案Stream
        /// 否則
        ///     Status = false
        ///     Message = 錯誤訊息
        /// </returns>

        public ReturnModel<Stream> ConvertTo(string url, Stream inStream)
        {
            ReturnModel<Stream> returnModel = new ReturnModel<Stream>();
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "post";
            webRequest.ContentType = "application/octet-stream";
            inStream.Seek(0, SeekOrigin.Begin);
            inStream.CopyTo(webRequest.GetRequestStream());

            using (HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse)
            {
                int statusCode = (int)webResponse.StatusCode;
                if ((statusCode >= 200) && (statusCode < 300))
                {
                    returnModel.Data = new MemoryStream();
                    webResponse.GetResponseStream().CopyTo(returnModel.Data);
                    returnModel.Data.Seek(0, SeekOrigin.Begin);
                    returnModel.Status = true;
                }
                else
                {
                    using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        returnModel.Message = streamReader.ReadToEnd();
                    }
                }
            }

            return returnModel;
        }

        /// <summary>
        /// 透過FILE_ID回傳WebDAV的URL
        /// </summary>
        /// <param name="fileId">FILE_ID</param>
        /// <returns>WebDAV的URL</returns>
        public string GetFileUrl(string fileId)
        {
            string fileUrl = string.Empty;
            COM2_FILES_UPLOAD filesUpload = repository.Get(p => p.FILE_ID.Equals(fileId));

            if (filesUpload != null)
            {
                fileUrl = filesUpload.FILE_PATH;
            }

            return fileUrl;
        }

        /// <summary>
        /// 透過FILE_ID回傳WebDAV檔案
        /// </summary>
        /// <param name="fileId">FILE_ID</param>
        /// <param name="fileName">原檔案檔名</param>
        /// <returns>檔案的Stream</returns>
        public Stream GetFile(string fileId, out string fileName)
        {
            Stream stream = null;
            fileName = string.Empty;
            COM2_FILES_UPLOAD filesUpload = repository.Get(p => p.FILE_ID.Equals(fileId));

            if (filesUpload != null)
            {
                fileName = filesUpload.UPLOAD_FILE_NAME;
                if (filesUpload.FILE_PATH.Contains("\\\\"))
                {
                    if (System.IO.File.Exists(filesUpload.FILE_PATH))
                    {
                        stream = new FileStream(filesUpload.FILE_PATH, FileMode.Open, FileAccess.Read);
                    }
                }
                else
                {
                    HttpWebRequest webRequest = WebRequest.Create(filesUpload.FILE_PATH) as HttpWebRequest;
                    webRequest.UseDefaultCredentials = true;
                    HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return webResponse.GetResponseStream();
                    }
                }
            }

            return stream;
        }

        /// <summary>
        /// 透過WebDAV上傳檔案
        /// </summary>
        /// <param name="bytes">上傳檔案的byte</param>
        /// <param name="fileName">上傳檔案的檔名</param>
        /// <param name="sysCode">功能模組代碼(如：SSO2, EMP2 ...)</param>
        /// <param name="subFolders">子資料夾結構，如為空值，則使用yyyyMM作為子資料夾名稱</param>
        /// <param name="uploadUser">上傳檔案人員</param>
        /// <param name="webDAVPath">WebDAV的根路徑</param>
        /// <param name="webDAVAccount">登入WebDAV的帳號</param>
        /// <param name="webDAVSecret">登入WebDAV的密碼</param>
        /// <param name="fileNameType">
        /// GUID_NAME: 表示使用Guid作為儲存的檔名 (預設)
        /// ORIGINAL_NAME: 表示使用原始檔名作為儲存的檔名
        /// </param>
        /// <returns>
        /// 上傳檔案及寫入資料庫成功，則
        ///     Status = true
        ///     Data = FILE_ID
        /// 否則
        ///     Status = false
        ///     Message = 錯誤訊息
        /// </returns>
        public ReturnModel WebDav_PutFile(byte[] bytes, string fileName, string sysCode, string subFolders, string uploadUser, string webDAVPath, string webDAVAccount, string webDAVSecret, FileNameTypeEnum fileNameType = FileNameTypeEnum.GUID_NAME)
        {
            return WebDav_PutFile(new MemoryStream(bytes), fileName, fileNameType, sysCode, subFolders, uploadUser, webDAVPath, webDAVAccount, webDAVSecret);
        }

        /// <summary>
        /// 透過WebDAV上傳檔案
        /// </summary>
        /// <param name="file">使用HTML上傳的檔案</param>
        /// <param name="sysCode">功能模組代碼(如：SSO2, EMP2 ...)</param>
        /// <param name="subFolders">子資料夾結構，如為空值，則使用yyyyMM作為子資料夾名稱</param>
        /// <param name="uploadUser">上傳檔案人員</param>
        /// <param name="webDAVPath">WebDAV的根路徑</param>
        /// <param name="webDAVAccount">登入WebDAV的帳號</param>
        /// <param name="webDAVSecret">登入WebDAV的密碼</param>
        /// <param name="fileNameType">
        /// GUID_NAME: 表示使用Guid作為儲存的檔名 (預設)
        /// ORIGINAL_NAME: 表示使用原始檔名作為儲存的檔名
        /// </param>
        /// <returns>
        /// 上傳檔案及寫入資料庫成功，則
        ///     Status = true
        ///     Data = FILE_ID
        /// 否則
        ///     Status = false
        ///     Message = 錯誤訊息
        /// </returns>
        public ReturnModel WebDav_PutFile(HttpPostedFileBase file, string sysCode, string subFolders, string uploadUser, string webDAVPath, string webDAVAccount, string webDAVSecret, FileNameTypeEnum fileNameType = FileNameTypeEnum.GUID_NAME)
        {
            return WebDav_PutFile(file.InputStream, System.IO.Path.GetFileName(file.FileName), fileNameType, sysCode, subFolders, uploadUser, webDAVPath, webDAVAccount, webDAVSecret);
        }

        /// <summary>
        /// 透過WebDAV刪除已上傳檔案
        /// </summary>
        /// <param name="fileId">FILE_ID</param>
        /// <param name="webDAVAccount">登入WebDAV的帳號</param>
        /// <param name="webDAVSecret">登入WebDAV的密碼</param>
        /// <returns>
        /// 刪除檔案及資料庫成功，則
        ///     Status = true
        /// 否則
        ///     Status = false
        ///     Message = 錯誤訊息
        /// </returns>
        public ReturnModel WebDav_Delete(string fileId, string webDAVAccount, string webDAVSecret)
        {
            ReturnModel returnModel = new ReturnModel();

            try
            {
                if (string.IsNullOrEmpty(fileId))
                {
                    returnModel.Message = "刪除失敗，fileId不能為空值！";
                    return returnModel;
                }

                COM2_FILES_UPLOAD fileUpload = repository.Get(f => f.FILE_ID.Equals(fileId));
                if (fileUpload != null)
                {
                    unitOfWork.UoWRepository<COM2_FILES_UPLOAD>().Delete(fileUpload);
                    using (WebDavClient client = new WebDavClient(new WebDavClientParams { Credentials = new NetworkCredential(webDAVAccount, webDAVSecret) }))
                    {
                        WebDavResponse webDavResponse = client.Delete(fileUpload.FILE_PATH).Result;
                        if (webDavResponse.IsSuccessful)
                        {
                            if (unitOfWork.Commit())
                            {
                                returnModel.Status = true;
                            }
                            else
                            {
                                returnModel.Message = string.Format("資料庫刪除\"{0}\"失敗", fileId);
                            }
                        }
                        else
                        {
                            returnModel.Message = string.Format("WebDAV刪除\"{0}\"失敗，{1}！", fileUpload.FILE_PATH, webDavResponse.Description);
                        }
                    }
                }
                else
                {
                    returnModel.Message = string.Format("查無FILE_ID:{0}", fileId);
                }
            }
            catch (Exception ex)
            {
                returnModel.Message = ex.Message;
            }

            return returnModel;
        }

        private ReturnModel WebDav_PutFile(Stream stream, string fileName, FileNameTypeEnum fileNameType, string sysCode, string subFolders, string uploadUser, string webDAVPath, string webDAVAccount, string webDAVSecret)
        {
            ReturnModel returnModel = new ReturnModel();

            try
            {
                if (stream == null)
                {
                    returnModel.Message = "上傳失敗，stream不能為NULL！";
                    return returnModel;
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    returnModel.Message = "上傳失敗，fileName不能為空值！";
                    return returnModel;
                }

                if (string.IsNullOrEmpty(webDAVPath))
                {
                    returnModel.Message = "上傳失敗，webDavPath不能為空值！";
                    return returnModel;
                }

                if (string.IsNullOrEmpty(webDAVAccount) || string.IsNullOrEmpty(webDAVSecret))
                {
                    returnModel.Message = "上傳失敗，webDavAccount與webDavSecret為驗證資訊，不能為空值！";
                    return returnModel;
                }

                if ((webDAVPath[webDAVPath.Length - 1] != '\\') && (webDAVPath[webDAVPath.Length - 1] != '/'))
                    webDAVPath += '/';
                string filePath = GetPath(sysCode, subFolders, webDAVPath, webDAVAccount, webDAVSecret);

                // 副檔名
                string fileExtension = System.IO.Path.GetExtension(fileName);

                // 新檔名
                string fileId = Guid.NewGuid().ToString("N");

                DateTime timeNow = DateTime.Now;

                COM2_FILES_UPLOAD fileUpload = new COM2_FILES_UPLOAD();

                fileUpload.FILE_ID = fileId;
                fileUpload.SYS_CODE = sysCode;
                fileUpload.UPLOAD_FILE_NAME = fileName;
                fileUpload.FILE_EXTENSION = fileExtension;
                fileUpload.NEW_FILE_NAME = fileNameType == FileNameTypeEnum.GUID_NAME ? fileId + fileExtension : fileName;
                fileUpload.FILE_PATH = filePath + fileUpload.NEW_FILE_NAME;
                fileUpload.SERVER_ID = null;
                fileUpload.UPLOAD_USER_ID = uploadUser;
                fileUpload.UPLOAD_TIME = timeNow;

                unitOfWork.UoWRepository<COM2_FILES_UPLOAD>().Create(fileUpload);
                using (WebDavClient client = new WebDavClient(new WebDavClientParams { Credentials = new NetworkCredential(webDAVAccount, webDAVSecret) }))
                {
                    WebDavResponse webDavResponse = client.PutFile(fileUpload.FILE_PATH, stream).Result;
                    if (webDavResponse.IsSuccessful)
                    {
                        if (unitOfWork.Commit())
                        {
                            returnModel.Status = true;
                            returnModel.Data = fileUpload.FILE_ID;
                        }
                        else
                        {
                            returnModel.Message = "資料庫新增失敗";
                        }
                    }
                    else
                    {
                        returnModel.Message = webDavResponse.Description;
                    }
                }
            }
            catch (Exception ex)
            {
                returnModel.Message = ex.Message;
            }

            return returnModel;
        }

        private string GetPath(string sysCode, string subFolders, string webDAVPath, string webDAVAccount, string webDAVSecret)
        {
            List<string> subFolderList = new List<string>();
            string path = webDAVPath;

            //if (!string.IsNullOrEmpty(sysCode))
            //{
            //    subFolderList.Add(sysCode);
            //}

            if (string.IsNullOrEmpty(subFolders))
            {
                subFolderList.Add(DateTime.Today.ToString("yyyyMM"));
            }
            else
            {
                subFolderList.AddRange(subFolders.Split(new char[] { '\\', '/' }));
            }

            if (subFolderList.Count > 0)
            {
                using (WebDavClient client = new WebDavClient(new WebDavClientParams { Credentials = new NetworkCredential(webDAVAccount, webDAVSecret) }))
                {
                    foreach (string subFolder in subFolderList)
                    {
                        path += subFolder + "/";
                        PropfindResponse propfindResponse = client.Propfind(path).Result;
                        if (!propfindResponse.IsSuccessful)
                        {
                            client.Mkcol(path);
                            propfindResponse = client.Propfind(path).Result;
                        }
                    }
                }
            }

            return path;
        }
    }
}
