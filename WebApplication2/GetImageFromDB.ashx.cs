using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    /// <summary>
    /// Summary description for GetImageFromDB
    /// </summary>
    public class GetImageFromDB : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //get the key of the image
//            string id = context.Request.QueryString["id"].ToString();
//            //get image byte[] from DB via ado.net
//            byte[] bs = getimagedata(id);
//            //change the type of response to image/jpg
//            context.Response.ContentType = "image/jpg";
//            //response image directly
//            context.Response.Write(bs);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}