using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

using Pluck.SiteLife.SDK;
using Pluck.SiteLife.SDK.Interfaces;
using Pluck.SiteLife.SDK.Models.Media;
using Pluck.SiteLife.SDK.Requests;
using Pluck.SiteLife.SDK.Requests.Discovery;
using Pluck.SiteLife.SDK.Requests.Media;
using Pluck.SiteLife.SDK.Responses;
using Pluck.SiteLife.SDK.Responses.Media;
using Pluck.SiteLife.SDK.Security;
using Groundfloor.Pluck.Config;
using System.Collections.Generic;

namespace Groundfloor.Pluck
{
    public class PluckHelper
    {
        const int PLUCK_BATCH_SIZE = 20;

        public static void UploadUserPhotoToPluck(string appName, Pluck.Config.PluckConfigElement pluckConfig, string photoKey, string fileName, string contentType, byte[] fileBytes)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(pluckConfig.uploadUrl);
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" + boundary;
            httpWebRequest2.Method = "POST";
            httpWebRequest2.KeepAlive = true;
            httpWebRequest2.Credentials = CredentialCache.DefaultCredentials;
 
            using (Stream memStream = new System.IO.MemoryStream())
            {
                //add Pluck gallery key to form data
                string formitem = string.Format("\r\n--{0}\r\nContent-Disposition: form-data; name=\"galleryKey\";\r\n\r\n{1}", boundary, pluckConfig.galleryKey);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                memStream.Write(formitembytes, 0, formitembytes.Length);

                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                string header = string.Format("Content-Disposition: form-data; name=\"Filedata\"; filename=\"{0}\"\r\n Content-Type: {1}\r\n\r\n"
                                               , fileName
                                               , contentType);

                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                memStream.Write(headerbytes, 0, headerbytes.Length);

                memStream.Write(fileBytes, 0, fileBytes.Length);                    
                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                httpWebRequest2.ContentLength = memStream.Length;

                Stream requestStream = null;
                try
                {
                    requestStream = httpWebRequest2.GetRequestStream();
                    memStream.Position = 0;
                    byte[] tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();
                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                }
                finally
                {
                    requestStream.Close();
                    requestStream.Dispose();
                }

                var results = new Dictionary<string, object>();

                WebResponse webResponse2 = httpWebRequest2.GetResponse();
                using (Stream stream2 = webResponse2.GetResponseStream())
                {
                    string response = null;
                    using (StreamReader reader2 = new StreamReader(stream2))
                    {
                        try
                        {
                            response = reader2.ReadToEnd();
                            //strip off the script text and CRLF
                            string guidStr = response.Substring(response.LastIndexOf('>') + 3);
                            results.Add("photokey", Guid.Parse(guidStr));
                        }
                        catch
                        {
                            results.Add("lasterror", new ApplicationException(response));
                        }
                        finally{
                            reader2.Close();
                        }
                    }
                }
                webResponse2.Close();
                httpWebRequest2 = null;
                webResponse2 = null;
            }
        }

        public static Dictionary<string, string> UpdatePluckPhotoDetails(string appName, Pluck.Config.PluckConfigElement pluckConfig, string description, string title, string tags, string photoKey)
        {
            var requests = new RequestBatch();

            #region Add UpdatePhotoActionRequest to batch
            var updateRequest = new UpdatePhotoActionRequest
            {
                Description = description,
                Title = title,
                Tags = tags,
                PhotoKey = new PhotoKey { Key = photoKey }
            };

            requests.AddRequest(updateRequest);
            #endregion

            #region Add UpdateDiscoveryFilterFlagActionRequest to batch
            ////Make the photo not discoverable
            UpdateDiscoveryFilterFlagActionRequest discAction = new UpdateDiscoveryFilterFlagActionRequest();
            discAction.BaseKey = updateRequest.PhotoKey;
            discAction.ExcludeFromDiscovery = true;
            requests.AddRequest(discAction);
            #endregion

            #region Add PhotoRequest to batch
            requests.AddRequest(new PhotoRequest { PhotoKey = updateRequest.PhotoKey });
            #endregion

            var pluckService = new PluckService(pluckConfig.apiUrl);
            var authToken = new UserAuthenticationToken(pluckConfig.userKey, pluckConfig.userNickname, pluckConfig.userEmail, pluckConfig.sharedSecret);

            ResponseBatch responseBatch = pluckService.SendRequest(requests, authToken);

            Dictionary<string, string> resultURI = new Dictionary<string, string>();
            if (responseBatch != null)
            {
                if (responseBatch.Envelopes[0].Payload.ResponseStatus.Exceptions != null && responseBatch.Envelopes[0].Payload.ResponseStatus.Exceptions.Length > 0)
                {
                    throw new ApplicationException(responseBatch.Envelopes[0].Payload.ResponseStatus.Exceptions[0].ExceptionMessage);
                }

                IResponse photoObject = responseBatch.Envelopes[2].GetResponse();
                PhotoResponse photoDetails = (PhotoResponse)photoObject;

                resultURI.Add("tiny", photoDetails.Photo.Image.Tiny);
                resultURI.Add("full", photoDetails.Photo.Image.Full);
                resultURI.Add("small", photoDetails.Photo.Image.Small);//60x60
                resultURI.Add("medium", photoDetails.Photo.Image.Medium);//h=200
            }
            return resultURI;
        }

        public static void GetPhotoModerationStatus(PluckConfigElement pluckConfig, List<string> photoKeys)
        {

            if (photoKeys.Count == 0)
                return;

            var pluckService = new PluckService(pluckConfig.apiUrl);
            var authToken = new UserAuthenticationToken(pluckConfig.userKey, pluckConfig.userNickname, pluckConfig.userEmail, pluckConfig.sharedSecret);
            RequestBatch requests = null;

            int numBatches = photoKeys.Count / PLUCK_BATCH_SIZE;
            if (photoKeys.Count % PLUCK_BATCH_SIZE > 0)
                numBatches++;

            int photoKeyIndex = 0;

            for (int batchNumber = 1; batchNumber <= numBatches; batchNumber++)
            {
                requests = new RequestBatch();
                #region build requests batch
                for (int i = 0; i < PLUCK_BATCH_SIZE; i++)
                {
                    photoKeyIndex = (batchNumber * PLUCK_BATCH_SIZE) - PLUCK_BATCH_SIZE + i;

                    if (photoKeyIndex == photoKeys.Count)
                        break;

                    var photorequest = new PhotoRequest()
                    {
                        PhotoKey = new PhotoKey { Key = photoKeys[photoKeyIndex] }
                    };

                    requests.AddRequest(photorequest);
                }
                #endregion

                ResponseBatch responseBatch = pluckService.SendRequest(requests, authToken);
                Dictionary<string, bool> photoStatus = new Dictionary<string, bool>();

                #region build responses batch
                if (responseBatch != null)
                {
                    foreach (var envelope in responseBatch.Envelopes)
                    {
                        var photo = (PhotoResponse)envelope.GetResponse();
                        var status = DetermineModerationStatus(photo);

                        if (status == PhotoModerationStatus.Pending || status == PhotoModerationStatus.Unknown)
                            continue;

                        try
                        {
                            //var photokey = photoKeys[photoKeyIndex];
                            //UserSubmitDB.SetModerationStatus(photo.Photo.PhotoKey.Key, (status == PhotoModerationStatus.Approved));
                            if (status == PhotoModerationStatus.Approved)
                            {
                                photoStatus.Add(photo.Photo.PhotoKey.Key, true);
                            }
                            else
                            {
                                string photokey = photo.ResponseStatus.Exceptions.Where(e => e.Name == "PhotoKey").Select(e => e.Value).FirstOrDefault();
                                photoStatus.Add(photokey, false);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                #endregion
            }
        }

        public static PhotoModerationStatus GetPhotoModerationStatus(PluckConfigElement pluckConfig, string photoKey)
        {
            var pluckService = new PluckService(pluckConfig.apiUrl);
            var authToken = new UserAuthenticationToken(pluckConfig.userKey, pluckConfig.userNickname, pluckConfig.userEmail, pluckConfig.sharedSecret);

            var requests = new RequestBatch();
            var photorequest = new PhotoRequest{PhotoKey = new PhotoKey { Key = photoKey }};
            requests.AddRequest(photorequest);

            ResponseBatch responseBatch = pluckService.SendRequest(requests, authToken);
            var photo = (PhotoResponse)responseBatch.Envelopes[0].GetResponse();

            return DetermineModerationStatus(photo);
        }

        public static PhotoModerationStatus DetermineModerationStatus(PhotoResponse photo)
        {
            //deleted
            if (photo.ResponseStatus.StatusCode == global::Pluck.SiteLife.SDK.Models.System.ResponseStatusCode.ProcessingException)
                return PhotoModerationStatus.Deleted;

            if (photo.ResponseStatus.StatusCode == global::Pluck.SiteLife.SDK.Models.System.ResponseStatusCode.SecurityException)
                return PhotoModerationStatus.Unknown;

            if (photo.Photo.IsPendingApproval)
                return PhotoModerationStatus.Pending;
            else
                return PhotoModerationStatus.Approved;

            //if (photo.Photo.ContentBlockingState = Pluck.SiteLife.SDK.Models.Common.ContentBlockingEnum.BlockedByAdmin)
        }

        public enum PhotoModerationStatus
        {
            Unknown,
            Deleted,
            Approved,
            Pending
        }
    }
}